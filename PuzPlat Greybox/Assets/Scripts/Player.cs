﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{



    #region Movement variables
    [Header("Movement Variables")]
    [Space(5)]
    public float speed = 0f;
    public float walkSpeed = 5.0f;
    public float runSpeed = 7.5f;
    public float jumpSpeed = 20f;
    public float gravity = 20.0f;
    public CharacterController controller;
    private Vector3 moveDirection = Vector3.zero;
    #endregion

    #region Jumping varlables
    [Header("Jumping Variables")]
    [Space(5)]
    public int curJump = 0;
    #endregion

    #region Sacrifice and switch variables
    [Header("Sacrifice and Switch Variables")]
    [Space(5)]
    public bool nearSacrifice;
    public bool nearSwitch;
    public GameObject nearSacObj, nearSwitchObj;
    public Switch switchScript;
    public GameObject sacrificeTarget;
    #endregion

    #region Fish list variables
    [Header("Fish List Variables")]
    [Space(5)]
    public List<GameObject> fishList = new List<GameObject>();
    public GameObject curFish;
    public GameObject fishSpawn;
    public int fishIndex = 0;
    #endregion

    #region Animations
    [Header("Animations")]
    [Space(5)]
    public Animator anim;
    #endregion

    #region Camera
    [Header("Camera")]
    [Space(5)]
    public Transform cam;
    #endregion


    // Update is called once per frame
    void Update()
    {
        float inputH = Input.GetAxis("Horizontal");
        float inputV = Input.GetAxis("Vertical");

        if (inputH == 0 && inputV == 0)
        {
            anim.SetBool("isMoving", false);
        }

        if (inputH != 0 || inputV != 0)
        {
            if (!controller.isGrounded)
            {
                anim.SetBool("isMoving", true);
                moveDirection.x = Input.GetAxis("Horizontal") * speed;
                moveDirection.z = Input.GetAxis("Vertical") * speed;
                moveDirection = transform.TransformDirection(moveDirection);
            }

            //// rotate the player in the direction of camera
            //Vector3 euler = cam.transform.eulerAngles;
            //transform.rotation = Quaternion.AngleAxis(euler.y, Vector3.up

            if (Input.GetKey(KeyCode.J))
            {
                anim.SetBool("isSacrificing", true);
            }

        }

        if (controller.isGrounded)
        {
            anim.SetBool("isMoving", true);
            curJump = 0;

            moveDirection = new Vector3(inputH, 0, inputV);
            moveDirection *= speed;
            moveDirection = transform.TransformDirection(moveDirection);

        }

        if (Input.GetButtonDown("Jump"))
        {
            if (curJump <= fishList.Count)
            {
                anim.SetBool("isJumping", true);
                moveDirection.y = jumpSpeed;
                curJump++;
                anim.SetBool("isJumping", false);
            }
        }

        // rotate the player in the direction of camera
        Vector3 euler = cam.transform.eulerAngles;
        transform.rotation = Quaternion.AngleAxis(euler.y, Vector3.up);

        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }


    private void OnTriggerStay(Collider other)
    {
        Debug.Log("Collided with " + other.gameObject.tag);
        if (other.gameObject.tag == "Moving Platform")
        {
            Debug.Log("collided with moving platform");
            this.transform.SetParent(other.gameObject.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        this.transform.SetParent(null);
    }



}
