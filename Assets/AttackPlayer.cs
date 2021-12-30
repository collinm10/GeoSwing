using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPlayer : MonoBehaviour
{
    private float speed = 15f;
    private GameObject player;

    void Start()
    {
        player = GameObject.Find("plaer");
    }
    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime;
        this.gameObject.transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);
    }
}
