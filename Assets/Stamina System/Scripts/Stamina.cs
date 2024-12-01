using System;

public struct Stamina
{
    public Stamina(int amount, DateTime nextUpdateTime, DateTime lastUpdateTime)
    {
        Amount = amount;

        NextUpdateTime = nextUpdateTime;

        LastUpdateTime = lastUpdateTime;
    }

    public int Amount { get; set; }

    public DateTime NextUpdateTime { get; set; }

    public DateTime LastUpdateTime { get; set; }
}