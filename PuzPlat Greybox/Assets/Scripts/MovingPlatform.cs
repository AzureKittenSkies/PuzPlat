using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public GameObject movePlat;

    public int checkpointIndex;
    public Transform[] checkpoint;
    public Transform target;
    public GameObject checkpointParent;

    public Vector3 platDestination;

    public Rigidbody rigid;

    public float speed = 2.5f;

    public bool vertMove, horzMove;
    public bool pingPong = false;
    public bool loopBack = false;

    void Start()
    {
        rigid = movePlat.GetComponent<Rigidbody>();

        if (this.gameObject.name.Contains("Vertical"))
        {
            vertMove = true;
        }

        else if (this.gameObject.name.Contains("Horizontal"))
        {
            horzMove = true;
        }

        for (int i = 0; i < checkpoint.Length; i++)
        {
            //checkpoint[i] = checkpointParent.transform.Find("Checkpoint " + i).transform;
        }


    }

    private void Update()
    {

        movePlat.transform.position = Vector3.Lerp(checkpoint[checkpointIndex].position, checkpoint[checkpointIndex + 1].position, speed);

        if (pingPong)
        {
            loopBack = true;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Waypoint"))
        {
            if (pingPong)
            {
                if (!loopBack)
                {
                    if (checkpoint.Length > checkpointIndex + 1)
                    {
                        checkpointIndex++;
                    }

                    if (checkpoint.Length == checkpointIndex + 1)
                    {
                        loopBack = true;
                    }
                }

                else if (loopBack)
                {
                    if (checkpointIndex + 1 > 0)
                    {
                        checkpointIndex--;
                    }

                    if (checkpointIndex == 0)
                    {
                        loopBack = true;
                    }
                }
            }

            else if (!pingPong)
            {
                if (checkpoint.Length > checkpointIndex + 1)
                {
                    checkpointIndex++;
                }

                if (checkpointIndex == checkpointIndex + 1)
                {
                    checkpointIndex = 0;
                }
            }
        }
    }

}
