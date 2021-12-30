using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGeneration : MonoBehaviour
{
    public GameObject ObstaclePrefab;

    public float x_obstacle_offset = 10f;
    public float y_obstacle_offset = 10f;

    private HazardGeneration hg;

    //Variables to store obstacles
    private GameObject[] obstacles;
    private int obstacle_pointer;

    // Start is called before the first frame update
    void Start()
    {
        obstacles = new GameObject[20];

        hg = this.gameObject.GetComponent("HazardGeneration") as HazardGeneration;

        reset();
    }

    public void reset()
    {
        for (int i = 0; i < 20; i++)
        {
            if (obstacles[i] != null)
                Destroy(obstacles[i]);
        }

        if (obstacle_pointer >= 20)
            obstacle_pointer = 0;

        obstacles[obstacle_pointer] = Instantiate(ObstaclePrefab, new Vector3(36f, 14f, 0), Quaternion.identity);
        obstacle_pointer++;
    }

    // Update is called once per frame
    void Update()
    {
        if (!hg.generated_obstacle)
        {
            hg.generated_obstacle = true;

            //If obstacle pointer is greated than the array length then wrap around
            if (obstacle_pointer >= 20)
                obstacle_pointer = 0;

            if (obstacles[obstacle_pointer] != null)
                Destroy(obstacles[obstacle_pointer]);

            float bottom_of_top_hazard = hg.hazards[hg.hazard_pointer - 2].transform.position.y - hg.hazards[hg.hazard_pointer - 2].transform.localScale.y / 2;

            obstacles[obstacle_pointer] = Instantiate(ObstaclePrefab, new Vector3(hg.hazards[hg.hazard_pointer - 2].transform.position.x - x_obstacle_offset, bottom_of_top_hazard + y_obstacle_offset, 0), Quaternion.identity);
            obstacle_pointer++;

            if (obstacle_pointer >= 20)
                obstacle_pointer = 0;

            if (obstacles[obstacle_pointer] != null)
                Destroy(obstacles[obstacle_pointer]);

            obstacles[obstacle_pointer] = Instantiate(ObstaclePrefab, new Vector3(hg.hazards[hg.hazard_pointer - 2].transform.position.x + x_obstacle_offset, bottom_of_top_hazard + y_obstacle_offset, 0), Quaternion.identity);
            obstacle_pointer++;
        }
    }
}
