using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private PlayerMovement pm;
    [SerializeField] GameObject pu;

    void Start()
    {
        pm = GameObject.FindGameObjectWithTag("Player").GetComponent("PlayerMovement") as PlayerMovement;
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        pm.AddBoost();
        Destroy(pu);
    }
}
