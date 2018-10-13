using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{

    public GameObject portalBarrier;

    public bool barrierActive = true;

    public int thisScene;

    // Use this for initialization
    void Start()
    {
        thisScene = SceneManager.GetActiveScene().buildIndex;
    }

    // Update is called once per frame
    void Update()
    {
        if (portalBarrier == null)
        {
            barrierActive = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !barrierActive)
        {
            SceneManager.LoadScene(thisScene + 1);
        }
    }


}
