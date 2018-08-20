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


    #region Variables

    public Rigidbody rigid;
    public GameObject target;
    public GameObject player;
    public GameObject sacTarget;
    public GameObject switchTarget;

    public Player playerScript;

    public float speed;
    public float rotationSpeed = 10f;

    #endregion



    // Use this for initialization
    void Start()
    {
        speed = Random.Range(0.5f, 5f);
        target = this.gameObject;
        playerScript = player.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null && target != this.gameObject)
        {
            rigid.velocity = transform.forward * speed;

            var targetRotation = Quaternion.LookRotation(target.transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        else if (target.name == "Player" && Input.GetKeyDown(KeyCode.J) && playerScript.nearSacrifice)
        {
            target = playerScript.nearSacObj;
        }

        else if (target == sacTarget && Input.GetKeyUp(KeyCode.J) && playerScript.nearSacrifice)
        {
            target = player;
        }

        else if (target == player && Input.GetKeyDown(KeyCode.J) && playerScript.nearSwitch)
        {
            target = playerScript.nearSwitchObj;
        }
        else if (target == switchTarget && Input.GetKeyDown(KeyCode.J) && playerScript.nearSwitch)
        {
            target = player;
        }



    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            target = other.gameObject;
            other.gameObject.GetComponent<Player>().fishList.Add(this.gameObject);
            this.gameObject.GetComponent<Collider>().enabled = false;
        }
    }


    private void OnDestroy()
    {
        //playerScript.
    }
}
