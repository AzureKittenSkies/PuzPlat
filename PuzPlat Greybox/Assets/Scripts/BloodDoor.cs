using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodDoor : MonoBehaviour
{

    public Material doorMatActive;
    public Material doorMatInactive;
    public GameObject barrier;

    public bool sacrificed = false;


    // Update is called once per frame
    void Update()
    {
        if (sacrificed)
        {
            Destroy(barrier);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.J) && !sacrificed)
            {
                other.GetComponent<Fish>().sacrificed = true;
            }
        }
    }


}

