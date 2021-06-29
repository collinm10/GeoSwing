using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D playerRB;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject launcher;
    [SerializeField] private Transform end_of_launcher;
    [SerializeField] private Transform start_of_launcher;
    [SerializeField] private LineRenderer l;
    [SerializeField] private float grapple_strength;
    [SerializeField] private LevelComplete lc;

    public float total_launch_force = 1000f;
    private float launcher_move_increment = .5f;
    private bool launch;
    private float angle_to_launch;
    private bool draw_line = false;
    private GameObject grappled_to;
    private float sphereRadius = 2f;
    private SpringJoint2D joint;
    public float start_time;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Jump"))
        {
            if(launcher.transform.position != end_of_launcher.position)
            {
                launch = true;
                start_time = Time.time;
            }
        }

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0.0f;

        if (Input.GetButtonDown("Fire1"))
        {

            if(Physics2D.OverlapCircle(mousePos, sphereRadius))
            {
                grappled_to = Physics2D.OverlapCircle(mousePos, sphereRadius).gameObject;
                if(grappled_to.layer != 7) //7 is the obstacle layer
                {
                    grappled_to = null;
                }
            }

            if(grappled_to != null)
            {
                draw_line = true;
                joint = player.gameObject.AddComponent<SpringJoint2D>();
                joint.autoConfigureConnectedAnchor = false;
                joint.connectedAnchor = grappled_to.transform.position;

                joint.distance = Vector2.Distance(player.transform.position, grappled_to.transform.position);

                joint.enableCollision = true;

                joint.dampingRatio = 0f;
                joint.frequency = 10;
            }

        }
        else if (Input.GetButtonUp("Fire1"))
        {
            draw_line = false;
            l.positionCount = 0;
            grappled_to = null;
            Destroy(joint);
        }

        if (draw_line)
        {
            l.positionCount = 2;
            List<Vector3> pos = new List<Vector3>();
            pos.Add(player.transform.position);
            pos.Add(grappled_to.transform.position);
            l.SetPositions(pos.ToArray());
        }
    }

    void FixedUpdate()
    {
        if (launch)
        {
            launcher.transform.position = Vector3.MoveTowards(launcher.transform.position, end_of_launcher.position, launcher_move_increment);
            Vector3 launcherPosVec = end_of_launcher.position - start_of_launcher.position;
            angle_to_launch = Vector3.Angle(new Vector3(1, 0, 0), launcherPosVec);
        }
        if(launch && launcher.transform.position == end_of_launcher.position)
        {
            launch = false;
            Vector2 launchForce = new Vector2(total_launch_force * ((90f - angle_to_launch) / 90f), angle_to_launch * (total_launch_force/90));
            playerRB.AddForce(launchForce);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Finish")
            lc.level_complete();
        else if (col.gameObject.tag == "Hazard")
            lc.level_failed();
    }

    public void reset()
    {
        grappled_to = null;
        draw_line = false;
    }
}
