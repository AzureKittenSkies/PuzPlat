using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
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
}
