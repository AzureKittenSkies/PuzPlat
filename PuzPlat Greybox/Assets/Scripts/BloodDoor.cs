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

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.J) && !sacrificed)
            {
                other.gameObject.GetComponent<Player>().fishList.RemoveAt(0);
                other.gameObject.GetComponent<Player>().fishList[0].GetComponent<Fish>().sacrificed = true;
                sacrificed = true;
            }
        }
    }


}

