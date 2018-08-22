using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{

    /* Notes
        fish group in cloud, following single game object.
            fish swarm player at a distance

        slow moving, slow turn speed

        fish disappears and reappears in character's hands. 
            Single fish moves to charcter's hands

        List of fish, add and remove gameobjects to prevent errors

        Fish, once activated, cannot be further than a certain distance from the player.


     */


    #region Movement Variables
    [Header("Movement Variable")]
    [Space(5)]
    public float curSpeed;
    public float followSpeed;
    public float catchupSpeed;
    public float rotationSpeed = 10f;
    public float distConstrain = 5f;

    public bool targetPlayer = false;

    #endregion

    #region Object References
    [Header("Object References")]
    [Space(5)]
    public Rigidbody rigid;
    public GameObject target;
    public GameObject player;
    public GameObject sacTarget;
    public GameObject switchTarget;
    #endregion

    #region Player Script Reference
    [Header("Player Script Reference")]
    [Space(5)]
    public Player playerScript;
    #endregion







    // Use this for initialization
    void Start()
    {
        followSpeed = Random.Range(0.5f, 5f);
        catchupSpeed = followSpeed * 2.5f;
        target = this.gameObject;
        playerScript = player.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target == player)
        {
            targetPlayer = true;
        }

        else
        {
            targetPlayer = false;
        }

        if (target != null && target != this.gameObject)
        {
            rigid.velocity = transform.forward * curSpeed;

            FishDistConstrain();

            var targetRotation = Quaternion.LookRotation(target.transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        //if (Input.GetKeyDown(KeyCode.J))
        //{
        //    if (target == player)
        //    {
        //        if (playerScript.nearSacrifice)
        //        {
        //            target = playerScript.nearSacObj;
        //        }

        //        if (playerScript.nearSwitch)
        //        {
        //            target = playerScript.nearSwitchObj;
        //        }
        //    }
        //    if (target == switchTarget && playerScript.nearSwitch)
        //    {
        //        target = player;
        //    }
        //}

        //else if (Input.GetKeyUp(KeyCode.J) && target == sacTarget && playerScript.nearSacrifice)
        //{
        //    target = player;
        //}

    }

    private void FishDistConstrain()
    {
        float dist = Vector3.Distance(player.transform.position, this.transform.position);
        if (dist <= distConstrain)
        {
            curSpeed = followSpeed;
        }
        else if (dist > distConstrain)
        {
            curSpeed = catchupSpeed;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            if (!other.gameObject.GetComponent<Player>().fishList.Contains(this.gameObject))
            {
                target = other.gameObject;
                other.gameObject.GetComponent<Player>().fishList.Add(this.gameObject);
                //this.gameObject.GetComponent<Collider>().enabled = false;
            }

        }
    }


    private void OnDestroy()
    {
        //playerScript.
    }
}