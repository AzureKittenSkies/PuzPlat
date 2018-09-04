using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{

    public CharacterController charController;

    public Vector3 impact = Vector3.zero;

    
    public void AddImpact(Vector3 dir, float force)
    {
        dir.Normalize();

        if (dir.y < 0)
        {
            dir.y = -dir.y;
        }




    }






}
