using System.Drawing;
using Robocode.TankRoyale.BotApi;
using Robocode.TankRoyale.BotApi.Events;

// ------------------------------------------------------------------
// RusdiJoging
// ------------------------------------------------------------------
// joging 
// ------------------------------------------------------------------
public class RusdiJoging : Bot
{
    bool movingForward;
    double radius, lastRammingTime;

    static void Main()
    {
        new RusdiJoging().Start();
    }

    RusdiJoging() : base(BotInfo.FromFile("RusdiJoging.json")) { }

    public override void Run()
    {
        BodyColor = Color.FromArgb(0x00, 0xC8, 0x00);   // lime
        TurretColor = Color.FromArgb(0x00, 0x96, 0x32); // green
        RadarColor = Color.FromArgb(0x00, 0x64, 0x64);  // dark cyan
        BulletColor = Color.FromArgb(0xFF, 0xFF, 0x64); // yellow
        ScanColor = Color.FromArgb(0xFF, 0xC8, 0xC8);   // light red

        movingForward = true;
        radius = 90;
        lastRammingTime = 0;

        while (IsRunning)
        {
            SetForward(40000);
            SetTurnLeft(radius);

            if (TurnNumber % 50 == 0) {
                radius *= -1;
            }

            Go();

        }
    }

    public override void OnHitWall(HitWallEvent e)
    {
        SetBack(50);
    }

    public override void OnScannedBot(ScannedBotEvent e)
    {
        if (TurnNumber - lastRammingTime > 200) {
            radius = BearingTo(e.X, e.Y);
        } 
    }

    public override void OnHitBot(HitBotEvent e)
    {
        Fire(10);
    }

    public override void OnBotDeath(BotDeathEvent e) {
        lastRammingTime = TurnNumber;
        radius = 90;
    }
}
