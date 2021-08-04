using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateCoinProgress : MonoBehaviour
{
    public int current_level;
    void OnTriggerEnter2D(Collider2D col)
    {
        CoinProgress cp = SaveSystem.LoadCoinProgress();
        cp.levelCoinProg[current_level-1][2] = true;
        SaveSystem.SaveCoinProgress(cp);
        gameObject.SetActive(false);
    }
}
