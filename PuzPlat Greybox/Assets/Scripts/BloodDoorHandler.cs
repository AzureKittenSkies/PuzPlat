using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodDoorHandler : MonoBehaviour
{

    public Material doorMatActive;
    public Material doorMatInactive;
    public GameObject barrier;


    public bool sacrificed = false;





    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (sacrificed)
        {
            Destroy(barrier);
        }
    }
}

