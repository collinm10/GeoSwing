using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    private GameObject[] power_ups;
    // Start is called before the first frame update
    void Start()
    {
        power_ups = GameObject.FindGameObjectsWithTag("Power Up");
    }

    public void reset_power_ups()
    {
        foreach(GameObject power_up in power_ups)
        {
            power_up.GetComponent<SpriteRenderer>().enabled = true;
            power_up.GetComponent<CircleCollider2D>().enabled = true;
        }
    }
}
