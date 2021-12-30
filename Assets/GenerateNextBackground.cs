using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateNextBackground : MonoBehaviour
{
    private GameObject player;
    [SerializeField] GameObject background_prefab;
    void Start()
    {
        player = GameObject.Find("plaer");
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Instantiate(background_prefab, new Vector3(transform.position.x + 108, -3.4f, 0), Quaternion.identity);
        this.gameObject.SetActive(false);
    }

}
