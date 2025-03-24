using System;
using System.Collections.Generic;
using System.Drawing;
using Robocode.TankRoyale.BotApi;
using Robocode.TankRoyale.BotApi.Events;

public class AntiRodok : Bot
{
    private double targetEnergyThreshold = 20;
    private int roundCheckInterval = 10;

    static void Main(string[] args)
    {
        new AntiRodok().Start();
    }

    AntiRodok() : base(BotInfo.FromFile("AntiRodok.json")) { }

    public override void Run()
    {
        while (IsRunning)
        {
            TurnRadarLeft(360); 
        }
    }

    public override void OnScannedBot(ScannedBotEvent e)
    {
        if (e.Energy < targetEnergyThreshold)
        {
            // Kalau musuh mempunyai energi yang kurang dari 20, maka bot akan menembakkan peluru dengan power yang lebih besar berdasarkan energi musuh
            double firePower = Math.Min(10, Math.Max(5, 20 - e.Energy));
            TurnGunToTarget(e.X, e.Y);
            Fire(firePower);
            GoTo(e.X, e.Y);
        }
        else
        {
            // Menembakkan musuh yang dilihat dengan power 2
            TurnGunToTarget(e.X, e.Y);
            Fire(2);
        }
    }

    private void TurnGunToTarget(double targetX, double targetY)
    {
        double angleToTarget = Math.Atan2(targetX - X, targetY - Y) * (180 / Math.PI);
        double gunTurnAngle = NormalizeBearing(angleToTarget - GunHeading);

        if (gunTurnAngle > 0)
        {
            TurnGunRight(gunTurnAngle);
        }
        else
        {
            TurnGunLeft(-gunTurnAngle);
        }
    }

    public override void OnRoundStarted(RoundStartedEvent e)
    {
        // Jika baru mulai cari posisi yang aman
        MoveToSaferLocation();
    }

    private void MoveToSaferLocation()
    {
        double safeX = BattlefieldWidth / 2 + (Random.NextDouble() - 0.5) * BattlefieldWidth / 2;
        double safeY = BattlefieldHeight / 2 + (Random.NextDouble() - 0.5) * BattlefieldHeight / 2;
        GoTo(safeX, safeY);
    }

    private void GoTo(double x, double y)
    {
        double angle = Math.Atan2(x - X, y - Y);
        TurnLeft(NormalizeBearing(angle - Heading));
        Forward(Math.Sqrt(Math.Pow(x - X, 2) + Math.Pow(y - Y, 2)));
    }

    private double NormalizeBearing(double angle)
    {
        angle %= 360;
        if (angle > 180) angle -= 360;
        if (angle < -180) angle += 360;
        return angle;
    }
}