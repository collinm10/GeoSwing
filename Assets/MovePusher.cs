using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePusher : MonoBehaviour
{
    public float speed;
    public bool go;

    // Update is called once per frame
    void Update()
    {
        if (go)
        {
            transform.position += new Vector3(1,0,0) * speed * Time.deltaTime;
        }
    }
}
