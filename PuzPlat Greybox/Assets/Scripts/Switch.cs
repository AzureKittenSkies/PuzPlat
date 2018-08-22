using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    /*
        Handle switch and fish behaviour from here

        if the player is within this switch's collider then check it for any fish


        if player selects it, set the fish's target to be this
        

     */


    #region In/active variables
    public bool active = false;

    public Material matActive, matInactive;
    public Material thisMat;
    #endregion


    #region Holding Fish References
    public GameObject heldFish;
    public GameObject holdingSwitch;

    #endregion

    #region Script references
    public Player playerScript;
    #endregion

    #region Player Detection
    public GameObject player;
    #endregion







    // Use this for initialization
    void Start()
    {
        //thisMat = GetComponent<Renderer>().material;
        holdingSwitch = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {

        // if the other collider is tagged "player"
        if (other.CompareTag("Player"))
        {
            player = other.gameObject;
            // set the playerScript variable to reference the player script attatched to the player
            playerScript = other.gameObject.GetComponent<Player>();

            // if the interact key is pressed 
            if (Input.GetKeyDown(KeyCode.J))
            {
                // and this switch is active
                if (active)
                {
                    // then get the fish held in switch to target player
                    heldFish.GetComponent<Fish>().target = player;
                    // and add it to the end of the fish list
                    playerScript.fishList.Add(heldFish);
                    // set the displaying material to be the inactive material
                    thisMat = matInactive;
                }

                // or if the switch is not active
                else if (!active)
                {
                    // then get a fish to target the holding switch
                    playerScript.fishList[0].GetComponent<Fish>().target = holdingSwitch;
                    heldFish = playerScript.fishList[0];
                    // remove the held fish from the fishList
                    playerScript.fishList.RemoveAt(0);
                    // set the displaying mterial to be the inactive material
                    thisMat = matActive;
                }
            }
        }



    }
}
