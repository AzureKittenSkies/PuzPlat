using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    public bool active = true;
    public GameObject doorObj;



    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!active)
        {
            doorObj.gameObject.SetActive(false);
        }

        else if (active)
        {
            doorObj.gameObject.SetActive(true);
        }
    }
}
