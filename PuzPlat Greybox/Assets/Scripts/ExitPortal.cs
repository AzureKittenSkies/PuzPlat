using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitPortal : MonoBehaviour
{
    public GameObject player;
    public GameObject barrier;


    public int fishRequired;
    public Vector3 knockback;

    public bool active = true;


    private void Update()
    {
        if (!active)
        {
            Destroy(barrier);
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player = other.gameObject;
            if (player.GetComponent<Player>().fishList.Count >= fishRequired)
            {
                active = false;
            }

            else if (other.GetComponent<Player>().fishList.Count < fishRequired)
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
