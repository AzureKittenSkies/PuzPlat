using System.Collections;
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
    public float jumpSpeed = 25f;
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

        if (controller.isGrounded)
        {
            // rotate the player in the direction of camera
            Vector3 euler = cam.transform.eulerAngles;
            transform.rotation = Quaternion.AngleAxis(euler.y, Vector3.up);


            moveDirection = new Vector3(inputH, 0, inputV);
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;

            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
        }

        if (curJump >= fishList.Count)
        {

        }

        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.J))
        {
            //anim.SetBool("Holding", true);
            //fishList[fishList.Count].gameObject.GetComponent<Fish>().target = sacrificeTarget;

            if (nearSacrifice && fishList.Count > 0)
            {
                curFish = fishList[0];
                // anim.SetBool("Sacrificing", true);
                GameObject.Destroy(curFish);
                fishList.RemoveAt(0);
                nearSacObj.GetComponentInParent<BloodDoorHandler>().sacrificed = true;
                Destroy(nearSacObj);
            }

            if (nearSwitch && fishList.Count > 0 && !switchScript.active)
            {
                curFish = fishList[0];
                nearSwitchObj.GetComponent<Switch>().active = true;
                GameObject.Destroy(curFish);
                fishList.RemoveAt(0);
                //fishList[0].GetComponent<Fish>().target = nearSwitchObj;
            }

            if (nearSwitch && switchScript.active)
            {
                // attatch a fish to the 
                Instantiate(fishSpawn);
            }

        }

        if (Input.GetKeyUp(KeyCode.J))
        {
            if (fishList.Count >= 0)
            {
                //anim.SetBool("Holding", false);
                fishList[0].gameObject.GetComponent<Fish>().target = this.gameObject;
            }
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log(fishList.Count);
        }



    }


    // move trigger detection into the relevant object's scripts
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Fish")
        {
            //fishList.Add(other.gameObject);
        }

        if (other.gameObject.name == "Area Check" && !other.gameObject.GetComponentInParent<BloodDoorHandler>().sacrificed)
        {
            nearSacrifice = true;
            nearSacObj = other.gameObject.GetComponentInParent<BloodDoorHandler>().barrier;
        }

        if (other.gameObject.name == "Holding Switch")
        {
            if (!other.gameObject.GetComponent<Switch>().active)
            {
                nearSwitch = true;
                nearSwitchObj = other.gameObject;
                switchScript = other.gameObject.GetComponent<Switch>();
            }
        }

    }











}
