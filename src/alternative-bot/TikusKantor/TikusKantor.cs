using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Robocode.TankRoyale.BotApi;
using Robocode.TankRoyale.BotApi.Events;

public class TikusKantor : Bot
{
    static void Main(string[] args)
    {
        new TikusKantor().Start();
    }

    TikusKantor() : base(BotInfo.FromFile("TikusKantor.json")) { }

    static Hashtable enemies = new Hashtable();
    static Enemy target;
    static Point2D nextDestination;
    static Point2D lastPosition;
    static Point2D myPos;
    static double myEnergy;

    public override void Run()
    {

        AdjustGunForBodyTurn = true;
        AdjustRadarForGunTurn = true;

        SetTurnRadarRight(360);

        nextDestination = lastPosition = myPos = new Point2D(X, Y);
        target = new Enemy();
        
        // Inisialisasi posisi awal
        lastPosition = new Point2D(X, Y);
        
        while (IsRunning)
        {
            myPos = new Point2D(X, Y);
            myEnergy = Energy;
            
            // Console.WriteLine("Jumlah musuh: " + enemies.Count);
            // Console.WriteLine("J musuh: " + target.live);
            
            if(target.live){
                moveAndGun();
            }

            Go();

        }
    }
    
    public void moveAndGun(){
        double distanceToTarget = myPos.Distance(target.pos);
    

        if (GunTurnRemaining == 0 && myEnergy > 1){
            Fire(1);
        }

        double gunHeadingRadians = GunDirection * (Math.PI / 180);

        SetTurnGunRight(NormalizeRelativeAngle(CalcAngle(target.pos, myPos)) - GunDirection);
        WaitFor(new TurnCompleteCondition(this));

        double distanceToNextDestination = myPos.Distance(nextDestination);

        if(distanceToNextDestination < 15) 
        {	
			
			double addLast = 1 - Math.Round(Math.Pow(new Random().NextDouble(), EnemyCount));

            Point2D testPoint;
            int i = 0;

            do
            {
                testPoint = CalcPoint(myPos,Math.Min(distanceToTarget*0.8, 100 + 200 * new Random().NextDouble()),2*Math.PI*new Random().NextDouble());

                if (testPoint.X > 30 && testPoint.Y > 30 && testPoint.X < ArenaWidth - 60 && testPoint.Y < ArenaHeight - 60 
                && evaluate(testPoint,addLast) < evaluate(nextDestination, addLast))
                {
                    nextDestination = testPoint;
                }

            }while(i++ < 200);
            Console.WriteLine("k");

        }else
        {
            double angle = CalcAngle(nextDestination, myPos) - GunDirection;
            double direction = 1;

            if(Math.Cos(angle * (Math.PI/180) ) < 0)
            {
                angle+=180;
                direction = -1;
            }
            
            
            angle = NormalizeRelativeAngle(angle);

            SetForward(distanceToNextDestination * direction);
            WaitFor(new TurnCompleteCondition(this));
            SetTurnRight(angle);
            WaitFor(new TurnCompleteCondition(this));
            
            

        }


    }


    public double evaluate(Point2D p, double addLast)
    {
        double eval = addLast * 0.08/p.DistanceSq(lastPosition);
        
        foreach (Enemy en in enemies.Values)
        {
            if (en.live)
            {
                eval += Math.Min(en.energy / myEnergy, 2) *
                        (1 + Math.Abs(Math.Cos(CalcAngle(myPos, p) - CalcAngle(en.pos, p)))) / p.DistanceSq(en.pos);
            }
        }
        return eval;

    }


    public override void OnScannedBot(ScannedBotEvent e)
    {
        Enemy en = (Enemy)enemies[e.ScannedBotId];

        if (en == null)
        {
            en = new Enemy();
            enemies.Add(e.ScannedBotId,en);
        }

        en.energy = e.Energy;
        en.live = true;
        Point2D enPos = new Point2D(e.X,e.Y);

        en.pos = CalcPoint(myPos, enPos.Distance(myPos), GunDirection + BearingTo(e.X,e.Y));

        if(!target.live || DistanceTo(e.X,e.Y) < myPos.Distance(target.pos)) {
			target = en;
		}

        if (EnemyCount == 1)
        {
            SetTurnRadarLeft(RadarTurnRemaining);
        }
    }

    public override void OnBotDeath(BotDeathEvent e)
    {
        ((Enemy)enemies[e.VictimId]).live = false;
    }

    private double GetDistanceTo(Point2D target)
    {
        return Math.Sqrt(Math.Pow(target.X - X, 2) + Math.Pow(target.Y - Y, 2));
    }

    private double CalcAngle(Point2D target,Point2D myPos)
    {
        return Math.Atan2(target.Y - myPos.Y,target.X - myPos.X) * (180.0 / Math.PI);
    }
    

    private static Point2D CalcPoint(Point2D p, double dist, double ang)
    {
        return new Point2D(p.X + dist * Math.Sin(ang), p.Y + dist * Math.Cos(ang));
    }
}

public class Point2D
{
    public double X { get; set; }
    public double Y { get; set; }

    public Point2D(double x, double y)
    {
        X = x;
        Y = y;
    }

    public double Distance(Point2D other)
    {
        return Math.Sqrt(Math.Pow(other.X - X, 2) + Math.Pow(other.Y - Y, 2));
    }

    public double DistanceSq(Point2D other)
    {
        return Math.Pow(other.X - X, 2) + Math.Pow(other.Y - Y, 2);
    }

    public double AngleTo(Point2D other)
    {
        return Math.Atan2(other.Y - Y, other.X - X);
    }

    public Point2D Translate(double angle, double distance)
    {
        return new Point2D(X + distance * Math.Cos(angle), Y + distance * Math.Sin(angle));
    }
}

public class Enemy
{
    public Point2D pos;
    public double energy;
    public bool live;
}

public class TurnCompleteCondition : Condition
{
    private readonly Bot bot;

    public TurnCompleteCondition(Bot bot)
    {
        this.bot = bot;
    }

    public override bool Test()
    {

        return bot.TurnRemaining == 0;
    }
}