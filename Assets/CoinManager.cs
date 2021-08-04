using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    public int current_level;
    public Image[] UICoins;
    public Sprite coin_image;

    private CoinProgress cp;
    void Start()
    {
        //cp = new CoinProgress();
        //SaveSystem.SaveCoinProgress(cp);

        cp = SaveSystem.LoadCoinProgress();

        if (cp == null)
        {
            cp = new CoinProgress();
            SaveSystem.SaveCoinProgress(cp);
        }

        if (cp.levelCoinProg[current_level-1][2])
        {
            try { GameObject.Find("Coin").SetActive(false); }
            catch {}
        }

        int i = 0;
        for (int j = 0; j < 3; j++)
        {
            if (cp.levelCoinProg[current_level-1][j])
            {
                UICoins[i].sprite = coin_image;
                i++;
            }

        }
    }

    public void UpdateGemUI()
    {
        int i = 0;
        cp = SaveSystem.LoadCoinProgress();
        bool[] level_prog = cp.GetNthLevelProg(current_level - 1);
        foreach (bool b in level_prog)
        {
            if (b)
            {
                UICoins[i].sprite = coin_image;
                i++;
            }

        }
    }
}
