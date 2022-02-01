using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpPhasor : MonoBehaviour
{
    [SerializeField] private PlayerMovement pm;
    void Start()
    {
        pm = GameObject.FindGameObjectWithTag("Player").GetComponent("PlayerMovement") as PlayerMovement;
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        pm.AddPhasor();
        ParticleSystem ps = gameObject.GetComponentInChildren<ParticleSystem>();
        ps.Play();
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
    }
}
