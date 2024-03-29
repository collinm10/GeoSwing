using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelProgress
{
    public bool[] levelUnlockProg;

    public LevelProgress(int numLevels)
    {
        levelUnlockProg = new bool[numLevels];
        for(int i = 0; i < numLevels; i++)
        {
            if(i < 3)
            {
                levelUnlockProg[i] = true;
            }
            else
            {
                levelUnlockProg[i] = false;
            }
        }
    }

    public void unlock_level(int i)
    {
        levelUnlockProg[i] = true;
    }
    
    public void UpdateLevelAmount()
    {
        bool[] temp = new bool[levelUnlockProg.Length + 10];
        
        for(int i = 0; i < temp.Length; i++)
        {
            if(i < levelUnlockProg.Length)
            {
                temp[i] = levelUnlockProg[i];
            }
            else
            {
                if(i < 11)
                {
                    temp[i] = true;
                }
                else
                {
                    temp[i] = false;
                }
            }
        }
        levelUnlockProg = temp;
    }
}
