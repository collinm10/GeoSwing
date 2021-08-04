using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CoinProgress
{
    public bool[][] levelCoinProg = new bool[10][];

    public CoinProgress()
    {
        int i = 0;
        for(i = 0; i < 10; i++)
        {
            levelCoinProg[i] = new bool[3];
        }
    }

    public bool[] GetNthLevelProg(int i)
    {
        return levelCoinProg[i];
    }

    public bool SetNthLevelProg(int i, bool[] b)
    {
        try { levelCoinProg[i] = b; return true; }
        catch{return false;}
    }

    public void AddNewLevel()
    {
        bool[][] b = new bool[levelCoinProg.Length + 1][];
        for (int i = 0; i < levelCoinProg.Length; i++)
        {
            b[i] = levelCoinProg[i];
        }
        b[levelCoinProg.Length] = new bool[3];
        levelCoinProg = b;
    }
}
