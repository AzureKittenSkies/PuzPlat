using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 0f;
    public float walkSpeed = 5.0f;
    public float runSpeed = 7.5f;
    public float jumpSpeed = 10f;
    public float gravity = 20.0f;

    public bool nearSacrifice = false;


    public CharacterController controller;
    private Vector3 moveDirection = Vector3.zero;


    public List<GameObject> fishList = new List<GameObject>();

    public Animator anim;

    public GameObject sacrificeTarget;






    // Update is called once per frame
    void Update()
    {

        float inputH = Input.GetAxis("Horizontal");
        float inputV = Input.GetAxis("Vertical");

        if (controller.isGrounded)
        {

            moveDirection = new Vector3(inputH, 0, inputV);
            moveDirection = transform.TransformDirection(moveDirection);

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                speed = runSpeed;
            }

            else if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                speed = walkSpeed;
            }
            moveDirection *= speed;

            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.J))
        {
            anim.SetBool("Holding", true);
            fishList[fishList.Count].gameObject.GetComponent<FishFollow>().target = sacrificeTarget;

            if (nearSacrifice)
            {
                anim.SetBool("Sacrificing", true);
                GameObject.Destroy(fishList[fishList.Count]);
                fishList.RemoveAt(fishList.Count);
            }
        }

        if (Input.GetKeyUp(KeyCode.J))
        {
            anim.SetBool("Holding", false);
            fishList[fishList.Count].gameObject.GetComponent<FishFollow>().target = this.gameObject;

        }



    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Fish")
        {
            fishList.Add(other.gameObject);
        }

        if (other.gameObject.name == "Blood Door" || other.gameObject.name == "Holding Switch")
        {
            nearSacrifice = true;
        }
    }











}
