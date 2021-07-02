using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] private GameObject bb1;


    
    public float total_launch_force = 1000f;
    private float launcher_move_increment = .5f;
    private bool launch;
    private float angle_to_launch;
    private bool draw_line = false;
    private GameObject grappled_to;
    private float sphereRadius = 30f;
    private SpringJoint2D joint;
    public float start_time;

    //Boost variabes
    private float boost;
    private float boost_force = 15f;
    private bool boosting;
    private BoostBar bb;

    //Launch variables
    private bool launched;
    
    //UI Variables
    private bool click_button;
    private GameObject launch_button;
    private GameObject swing_button;
    private float press_radius = 5f;
    private Touch button_touch;
    private Touch grapple_touch;

    // Start is called before the first frame update
    void Start()
    {
        boost = 0f;
        bb = bb1.GetComponent("BoostBar") as BoostBar;
        launch_button = GameObject.Find("Launch Boost Button");
        swing_button = GameObject.Find("Grapple");
        button_touch.phase = TouchPhase.Ended;
        grapple_touch.phase = TouchPhase.Ended;
    }

    // Update is called once per frame
    void Update()
    {
        if (!launched)
        {
            launch_button.SetActive(true);
        }
        if (Input.touchCount > 0)
        {
            grapple_touch = Input.GetTouch(0);

            Vector3 g_pos = Camera.main.ScreenToWorldPoint(grapple_touch.position);
            g_pos.z = swing_button.transform.position.z;

            if (grapple_touch.phase == TouchPhase.Began && Vector3.Distance(g_pos, swing_button.transform.position) < press_radius)
            {
                setup_grapple(grapple_touch);
            }
            else if (grapple_touch.phase == TouchPhase.Ended)
            {
                draw_line = false;
                l.positionCount = 0;
                grappled_to = null;
                Destroy(joint);
                launch_button.SetActive(false);
            }
            
            if(Input.touchCount > 1 || !launched)
            {
                if (launched)
                    button_touch = Input.GetTouch(1);
                else
                    button_touch = Input.GetTouch(0);
                Vector3 t_pos = Camera.main.ScreenToWorldPoint(button_touch.position);
                t_pos.z = launch_button.transform.position.z;

                if (Vector3.Distance(t_pos, launch_button.transform.position) < press_radius && (button_touch.phase == TouchPhase.Began || button_touch.phase == TouchPhase.Stationary))
                {
                    if (!launched)
                        launch = true;
                    else
                        boosting = true;
                }
                else if(button_touch.phase == TouchPhase.Ended)
                {
                    boosting = false;
                }
            }
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
            launch_button.SetActive(false);
            launcher.transform.position = Vector3.MoveTowards(launcher.transform.position, end_of_launcher.position, launcher_move_increment);
            Vector3 launcherPosVec = end_of_launcher.position - start_of_launcher.position;
            angle_to_launch = Vector3.Angle(new Vector3(1, 0, 0), launcherPosVec);
        }

        if(launch && launcher.transform.position == end_of_launcher.position)
        {
            launch = false;
            Vector2 launchForce = new Vector2(total_launch_force * ((90f - angle_to_launch) / 90f), angle_to_launch * (total_launch_force/90));
            playerRB.AddForce(launchForce);
            launched = true;
            launch_button.GetComponentInChildren<Text>().text = "Boost";
            start_time = Time.time;
        }

        if (boosting)
        {
            if (boost > 0)
            {
                Vector2 curr_dir = playerRB.velocity;
                curr_dir.Normalize();
                playerRB.AddForce(curr_dir * boost_force);
                boost = boost - 1;
                bb.boost = boost;
            }
            else
            {
                boosting = false;
                if(launched)
                    launch_button.SetActive(false);
            }
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
        Destroy(joint);
        start_time = Time.time;
        boost = 0;
        bb.boost = 0;
        bb1.SetActive(false);
        launched = false;
        start_time = 0f;
        launch_button.GetComponentInChildren<Text>().text = "Launch";
        launch_button.SetActive(true);
    }

    public void AddBoost()
    {
        bb1.SetActive(true);
        boost = 100;
        bb.boost = 100;
    }

    private void setup_grapple(Touch t)
    {
        if (Physics2D.OverlapCircle(player.transform.position, sphereRadius))
        {
            float closest = sphereRadius;
            Collider2D[] hold = Physics2D.OverlapCircleAll(player.transform.position, sphereRadius);
            foreach(Collider2D col in hold)
            {
                if(grappled_to == null)
                {
                    grappled_to = col.gameObject;
                    closest = Vector2.Distance(player.transform.position, col.transform.position);
                }
                else if(Vector2.Distance(player.transform.position, col.transform.position) < closest)
                {
                    closest = Vector2.Distance(player.transform.position, col.transform.position);
                    grappled_to = col.gameObject;
                }
            }
            if (grappled_to.layer != 7) //7 is the obstacle layer
            {
                grappled_to = null;
            }
            if(boost > 0)
            {
                launch_button.SetActive(true);
            }
        }

        if (grappled_to != null)
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


}
