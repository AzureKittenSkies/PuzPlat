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

    #region General Variables
    [Header("Movement Variable")]
    [Space(5)]
    public float curSpeed;
    public float followSpeed;
    public float catchupSpeed;
    public float tempCatchupSpeed;
    public float curRotSpeed;
    public float rotationSpeed = 10f;
    public float orbitRotationSpeed;
    public float distConstrain = 5f;
    public float minDist = 1.5f;
    public bool targetPlayer = false;
    public float switchVel = 1.5f;
    public float switchRot = 5f;

    public bool sacrificed = false;
    #endregion

    #region Animation
    [Header("Animation")]
    [Space(5)]
    public Animator anim;
    public Animation sacrificeAnim;
    public float sacAnimDuration = 2.5f;
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
    public GameObject playerFollow;
    public bool waitingForPlayer = true;

    public LayerMask layerMask;

    #endregion


    // Use this for initialization
    void Start()
    {
        followSpeed = Random.Range(1.5f, 2.5f);
        catchupSpeed = followSpeed * 2.5f;
        target = this.gameObject;
        playerScript = playerFollow.GetComponent<Player>();
        orbitRotationSpeed = followSpeed / minDist;
        tempCatchupSpeed = catchupSpeed;
        
    }

    private void Move()
    {
        rigid.velocity = transform.forward * curSpeed;


        var targetRotation = Quaternion.LookRotation((target.transform.position) - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, curRotSpeed * Time.deltaTime);
    }
    

    // Update is called once per frame
    void Update()
    {
        if (target.CompareTag("Player"))
        {
            Move();
            FishDistConstrain();

        }

        if (target.CompareTag("Switch") && !waitingForPlayer )
        {
            //anim.SetBool("Switch", true);
            curSpeed = switchVel;
            curRotSpeed = switchRot;
            Move();
        }

        if (target.CompareTag("Switch") && waitingForPlayer)
        {
            curSpeed = followSpeed;
            curRotSpeed = rotationSpeed;

            target = playerFollow;
        }

        if (sacrificed)
        {
            //anim.SetBool("Sacrificed", true);
            //sacAnimDuration = anim.GetCurrentAnimatorStateInfo(0).length;
            Destroy(this.gameObject, sacAnimDuration);
        }

    }

    private void FishDistConstrain()
    {
        float dist = Vector3.Distance(playerFollow.transform.position, this.transform.position);
        if (dist > distConstrain)
        {
            curRotSpeed = rotationSpeed;
            StartCoroutine(SpeedIncrease(tempCatchupSpeed));
            curSpeed = tempCatchupSpeed;
        }
        if (dist <= distConstrain)
        {
            tempCatchupSpeed = catchupSpeed;
            curRotSpeed = rotationSpeed;
            curSpeed = followSpeed;
        }
        else if (dist < minDist)
        {
            curRotSpeed = orbitRotationSpeed;
        }
        Debug.Log(curRotSpeed);
    }

    IEnumerator SpeedIncrease(float speed)
    {
        speed += 0.1f;
        yield return speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player" && waitingForPlayer)
        {
            if (!other.gameObject.GetComponent<Player>().fishList.Contains(this.gameObject))
            {
                target = other.gameObject;
                playerFollow = other.gameObject;
                other.gameObject.GetComponent<Player>().fishList.Add(this.gameObject);
                //this.gameObject.GetComponent<Collider>().enabled = false;
                waitingForPlayer = false;
            }

        }
    }
    
}