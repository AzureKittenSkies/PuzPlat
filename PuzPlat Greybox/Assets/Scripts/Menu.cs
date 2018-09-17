using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public float scrW, scrH;

    public bool mainMenu;
    public bool optionMenu;
    


    // Use this for initialization
    void Start()
    {
        
    }

    private void Update()
    {
        scrW = Screen.width / 16;
        scrH = Screen.height / 9;
    }

    private void OnGUI()
    {
        if (mainMenu)
        {
            GUI.Box(new Rect(Scr(0, 0), Scr(19, 9)), "");
            GUI.Box(new Rect(Scr(5, 0.5f), Scr(6, 1.5f)), "Title");

            if (GUI.Button(new Rect(Scr(7, 3), Scr(2, 1)), "Play"))
            {
                mainMenu = false;
                SceneManager.LoadScene(1);
            }

            if (GUI.Button(new Rect(Scr(7,4.5f), Scr(2,1)), "Options"))
            {
                mainMenu = false;
                optionMenu = true;
            }

           // if (GUI.Button(new Rect(Scr(7, )
            {

            }
        }




    }


    private Vector2 Scr(float x, float y)
    {
        Vector2 coord = Vector2.zero;
        coord = new Vector2(scrW * x, scrH * y);
        return coord;
    }



}
