using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltMove : MonoBehaviour
{

    public GameObject waypointParent;
    public Transform[] waypoints;
    public float speed = 2;

    public bool pingPong;
    public bool loopBack;

    public int currentPoint = 0;

    public int size;

    private void Start()
    {
        waypoints = new Transform[size];
        for (int i = 0; i < size; i++)
        {
            waypoints[i] = waypointParent.transform.Find("Checkpoint (" + i + ")").transform;
        }
    }

    void Update()
    {




        if (transform.position != waypoints[currentPoint].transform.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, waypoints[currentPoint].transform.position, speed * Time.deltaTime);
        }

        if (!loopBack)
        {
            Debug.Log("Moving forward");
            if (transform.position == waypoints[currentPoint].transform.position)
            {
                currentPoint++;
            }

            if (currentPoint >= waypoints.Length)
            {
                currentPoint = waypoints.Length-1;
                loopBack = true;
            }
        }

        if (loopBack)
        {
            Debug.Log("Looping back");
            if (transform.position == waypoints[currentPoint].transform.position)
            {
                currentPoint--;
            }

            if (currentPoint <= 0)
            {
                currentPoint = 0;
                loopBack = false;
            }
        }

    }



}
