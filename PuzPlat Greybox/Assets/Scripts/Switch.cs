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


    #region In/active 
    public bool active = false;

    public Material matActive, matInactive;
    public Material thisMat;

    // Use this for initialization
    void Start()
    {
        thisMat = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            thisMat = matActive;
        }
        else if (!active)
        {
            thisMat = matInactive;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

        }
    }
}
