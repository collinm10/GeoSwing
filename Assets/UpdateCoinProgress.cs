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

        ParticleSystem ps = gameObject.GetComponent<ParticleSystem>();
        ps.Play();

        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;

        int coins = PlayerPrefs.GetInt("Coins", 0);
        coins = coins + 1;
        PlayerPrefs.SetInt("Coins", coins);
    }
}
