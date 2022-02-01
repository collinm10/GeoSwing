using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePhasor : MonoBehaviour
{
    float time = 0;
    public Vector2 dir;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        time = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - time > .6)
            Destroy(gameObject);
    }

    public void ChangeDir(Vector2 _dir, Vector3 lookHere)
    {
        dir = _dir;
        float ang = Vector2.Angle(_dir, new Vector2(1, 0));
        if (dir.y < 0)
            ang *= -1f;
        transform.Rotate(0, 0, ang);
        gameObject.GetComponent<Rigidbody2D>().velocity =  -1f * dir * speed;
    }
}
