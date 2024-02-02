using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class KillScore : IComparable<KillScore>
{
    public string playerName;
    public int playerKills;
    public KillScore(string playerName, int playerKills)
    {
        this.playerName = playerName;
        this.playerKills = playerKills;
    }

    public int CompareTo(KillScore other)
    {
        return other.playerKills - playerKills; 
    }

}
