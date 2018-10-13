using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodDoor : MonoBehaviour
{

    public GameObject barrier;

    public bool sacrificed = false;

    public Vector3 knockback;

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
            if (Input.GetKeyDown(KeyCode.J) && !sacrificed && other.gameObject.GetComponent<Player>().fishList.Count > 0)
            {
                other.gameObject.GetComponent<Player>().fishList[0].GetComponent<Fish>().sacrificed = true;
                other.gameObject.GetComponent<Player>().fishList.RemoveAt(0);
                sacrificed = true;
            }
            else if (other.gameObject.GetComponent<Player>().fishList.Count <= 0)
            {
                knockback = other.transform.position - transform.position;

                if (knockback.magnitude > 0.2)
                {
                    other.gameObject.GetComponent<CharacterController>().Move(knockback * Time.deltaTime);
                }

                knockback = Vector3.Lerp(knockback, Vector3.zero, 5 * Time.deltaTime);
                

            }
            
        }
    }


}

