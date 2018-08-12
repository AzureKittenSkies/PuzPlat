using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishFollow : MonoBehaviour
{

    /* Notes
        fish group in cloud, following single game object.
            fish swarm player at a distance

        slow moving, slow turn speed

        fish disappears and reappears in character's hands. 
            Single fish moves to charcter's hands

        List of fish, add and remove gameobjects to prevent errors

     */


    #region Variables

    public Rigidbody rigid;
    public GameObject target;


    public float speed;
    public float rotationSpeed = 10f;

    #endregion



    // Use this for initialization
    void Start()
    {
        speed = Random.Range(0.5f, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        rigid.velocity = transform.forward * speed;

        var targetRotation = Quaternion.LookRotation(target.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);


    }

}
