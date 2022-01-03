using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddGem : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        ParticleSystem ps = gameObject.GetComponent<ParticleSystem>();
        ps.Play();

        int coins = PlayerPrefs.GetInt("Coins", 0);
        coins = coins + 1;
        PlayerPrefs.SetInt("Coins", coins);

        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }
}
