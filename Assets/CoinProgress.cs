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
        for(i = 0; i < 20; i++)
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
    
    public void AddNewLevelSet()
    {
        bool[][] b = new bool[levelCoinProg.Length + 10][];
        for (int i = 0; i < levelCoinProg.Length; i++)
        {
            b[i] = levelCoinProg[i];
        }
        
        for(int i = 0; i < 10; i++)
        {
            b[levelCoinProg.Length + i] = new bool[3];
        }

        levelCoinProg = b;
    }
}
