using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuAnimation : MonoBehaviour
{
    public int z_offset;
    public GameObject player;
    public LineRenderer l;
    public GameObject obstacle;
    private float radius = 0f;
    private SpringJoint2D joint;

    void Start()
    {
        joint = player.gameObject.AddComponent<SpringJoint2D>();
        joint.autoConfigureConnectedAnchor = false;
        joint.connectedAnchor = obstacle.transform.position;

        joint.distance = Vector2.Distance(player.transform.position, obstacle.transform.position);

        joint.enableCollision = true;

        joint.dampingRatio = 0f;
        joint.frequency = 10;
    }

    void Update()
    {
        l.positionCount = 2;
        List<Vector3> pos = new List<Vector3>();
        Vector3 edge = player.transform.position - obstacle.transform.position;
        edge.Normalize();
        Vector3 hold = player.transform.position + edge * radius;
        hold.z = hold.z - z_offset;
        pos.Add(hold);
        pos.Add(obstacle.transform.position);
        l.SetPositions(pos.ToArray());
    }

    public void update_skins(int index, int type)
    {
        switch (type)
        {
            case 0:
                break;
            case 1:
                break;
            case 2:
                gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("ForPlayer/PlayerSkin" + index);
                break;
        }
    }
}
