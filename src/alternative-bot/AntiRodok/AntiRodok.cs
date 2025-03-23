using System;
using System.Collections.Generic;
using System.Drawing;
using Robocode.TankRoyale.BotApi;
using Robocode.TankRoyale.BotApi.Events;

public class AntiRodok : Bot
{
    private List<ScannedBotEvent> detectedBots = new List<ScannedBotEvent>();
    private ScannedBotEvent target = null;
    private const double SAFE_DISTANCE = 200;
    private const double LOW_STAMINA = 30;
    private bool radarSweepComplete = false;
    static void Main(string[] args)
    {
        new AntiRodok().Start();
    }

    AntiRodok() : base(BotInfo.FromFile("AntiRodok.json")) { }

    public override void Run()
    {
        BodyColor = Color.Black;
        TurretColor = Color.Black;
        GunColor = Color.Gray;
        RadarColor = Color.Green;

        while (IsRunning)
        {
            if (radarSweepComplete)
            {
                detectedBots.Clear(); 
                radarSweepComplete = false;
            }
            
            Scan();
            
            if (detectedBots.Count > 0)
            {
                target = GetFarthestBot();
                if (target != null)
                {
                    MaintainDistanceAndCircle(target);
                    double angle = Math.Atan2(target.Y - Y, target.X - X) * 180 / Math.PI;
                    TurnGunRight(angle - this.GunDirection);
                    Fire(2); 
                }
            }
            else
            {
                SetForward(50);
                SetTurnLeft(30);
                Go();
            }
        }
    }

    public void Scan()
    {
        TurnRadarRight(360);
    }

    public override void OnScannedBot(ScannedBotEvent e)
    {
        detectedBots.Add(e);
    }
    
    public override void OnRoundEnded(RoundEndedEvent e)
    {
        radarSweepComplete = true;
    }

    private ScannedBotEvent GetFarthestBot()
    {
        ScannedBotEvent farthest = null;
        double maxDistance = 0;

        foreach (var bot in detectedBots)
        {
            double distance = Math.Sqrt(Math.Pow(bot.X - X, 2) + Math.Pow(bot.Y - Y, 2));
            if (distance > maxDistance)
            {
                maxDistance = distance;
                farthest = bot;
            }
        }
        return farthest;
    }

    private void MaintainDistanceAndCircle(ScannedBotEvent target)
    {
        double distance = Math.Sqrt(Math.Pow(target.X - X, 2) + Math.Pow(target.Y - Y, 2));

        if (distance > SAFE_DISTANCE)
        {
            Forward(50);
            TurnRight(20);
        }
        else
        {
            Back(50);
            TurnLeft(20);
        }
    }

    public override void OnHitWall(HitWallEvent e)
    {
        TurnRight(125);
        Forward(100);
    }

    public override void OnHitBot(HitBotEvent e)
    {
        if (e.Energy < LOW_STAMINA)
        {
            Forward(100);
        }
        else
        {
            Back(50);
        }
    }
}
