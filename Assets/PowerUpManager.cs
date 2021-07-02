using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    private GameObject[] power_ups;
    private Transform[] power_ups_transform;
    public GameObject power_up_prefab;
    public GameObject empty_go;
    // Start is called before the first frame update
    void Start()
    {
        power_ups = GameObject.FindGameObjectsWithTag("Power Up");
        power_ups_transform = new Transform[power_ups.Length];
        
        int i = 0;
        foreach (GameObject power_up in power_ups)
        {
            power_ups_transform[i] = Instantiate(empty_go, power_up.transform).transform;
            power_ups_transform[i].parent = null;
            i++;
        }

    }

    public void reset_power_ups()
    {
        int i = 0;
        foreach(GameObject power_up in power_ups)
        {
            if(power_up == null)
            {
                Instantiate(power_up_prefab, power_ups_transform[i]);
            }
            i++;
        }
    }
}
