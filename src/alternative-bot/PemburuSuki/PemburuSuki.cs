using System;
using System.Drawing;
using Robocode.TankRoyale.BotApi;
using Robocode.TankRoyale.BotApi.Events;

public class PemburuSuki : Bot
{   
    double radius, deathTime, switchTime, move;
    bool isMoveForward;
    
    /* A bot that will find you, chase you, and backshot you till you cant stand on your legs */
    static void Main(string[] args)
    {
        new PemburuSuki().Start();
    }

    PemburuSuki() : base(BotInfo.FromFile("PemburuSuki.json")) { }

    public override void Run()
    {
        /* Customize bot colors, read the documentation for more information */
        BodyColor = Color.FromArgb(0x0, 0x0, 0x0);
        TurretColor = Color.FromArgb(0x0, 0x0, 0x0);
        RadarColor = Color.FromArgb(0xff, 0xff, 0xff);
        BulletColor = Color.FromArgb(0x0, 0x0, 0x0);
        ScanColor = Color.FromArgb(0x0, 0x0, 0x0);
        TracksColor = Color.FromArgb(0x0, 0x0, 0x0);
        GunColor = Color.FromArgb(0x0, 0x0, 0x0);

        deathTime = 0;
        switchTime = 0;
        radius = 360;
        isMoveForward = true;
        move = 2;

        
        while (IsRunning)
        {

            // if (TurnNumber % 100 == 0) {
            //     Console.WriteLine(TurnNumber + ":" + isMoveForward);
            // }

            SetTurnLeft(radius);

            Go();
        }
    }

    public override void OnScannedBot(ScannedBotEvent e)
    {
        if (TurnNumber - deathTime > 10) {


            radius = BearingTo(e.X, e.Y);
            double d = DistanceTo(e.X, e.Y);

            if (isMoveForward) {
                SetForward(d);
            } else {
                SetForward(-50);
            }



            if (d < 300) {
                Fire(3);
            } else {
                Fire(0.5);
                isMoveForward = true;
            }
        }
    }

    public override void OnHitBot(HitBotEvent e)
    {
        var bearing = BearingTo(e.X, e.Y);
        if (bearing > -10 && bearing < 10)
        {
            Fire(3);
        }    
        isMoveForward = false;  
    }

    public override void OnHitWall(HitWallEvent e)
    {

    }

    public override void OnBotDeath(BotDeathEvent e) {
        // reset attributes
        deathTime = TurnNumber;
        radius = 360;
        Console.WriteLine("Enemy killed");
        isMoveForward = true;
    }
}
