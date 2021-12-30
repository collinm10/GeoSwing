using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyLastBackground : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        GameObject[] d = GameObject.FindGameObjectsWithTag("BackgroundTag");
        if(d.Length > 2)
        {
            Destroy(d[1]);
        }
    }
}
