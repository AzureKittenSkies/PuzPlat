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
    public float curRotSpeed;
    public float rotationSpeed = 10f;
    public float orbitRotationSpeed;
    public float distConstrain = 5f;


    public float minDist = 5f;

    public bool targetPlayer = false;

    #endregion

    #region Object References
    [Header("Object References")]
    [Space(5)]
    public Rigidbody rigid;
    public GameObject target;
    public GameObject sacTarget;
    public GameObject switchTarget;
    #endregion

    #region Player References
    [Header("Player Script Reference")]
    [Space(5)]
    public Player playerScript;
    public GameObject player;
    public bool waitingForPlayer = true;

    public LayerMask layerMask;

    #endregion







    // Use this for initialization
    void Start()
    {
        followSpeed = Random.Range(2.5f, 7.5f);
        catchupSpeed = followSpeed * 2.5f;
        target = this.gameObject;
        playerScript = player.GetComponent<Player>();
        orbitRotationSpeed = followSpeed / minDist;

    }

    // Update is called once per frame
    void Update()
    {
        if (target.CompareTag("Player"))
        {

            rigid.velocity = transform.forward * curSpeed;

            FishDistConstrain();

            var targetRotation = Quaternion.LookRotation((target.transform.position)- transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, curRotSpeed * Time.deltaTime);
        }

        if (target.CompareTag("Switch"))
        {
            return;
        }

    }

    private void FishDistConstrain()
    {
        float dist = Vector3.Distance(player.transform.position, this.transform.position);
        if (dist <= distConstrain)
        {
            curRotSpeed = rotationSpeed;
            curSpeed = followSpeed;
        }
        if (dist > distConstrain)
        {
            curRotSpeed = rotationSpeed;
            curSpeed = catchupSpeed;
        }
        else if (dist < minDist)
        {
            curRotSpeed = orbitRotationSpeed;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player" && waitingForPlayer)
        {
            if (!other.gameObject.GetComponent<Player>().fishList.Contains(this.gameObject))
            {
                target = other.gameObject;
                player = other.gameObject;
                other.gameObject.GetComponent<Player>().fishList.Add(this.gameObject);
                //this.gameObject.GetComponent<Collider>().enabled = false;
                waitingForPlayer = false;
            }

        }
    }
    

    private void OnDestroy()
    {
        //playerScript.
    }
}