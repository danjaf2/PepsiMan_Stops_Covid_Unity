using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause2D : MonoBehaviour
{
    public GameObject menu;
    public GameObject overMenu;
    public bool paused = false;

    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.Find("Player");
       if (GiveMasks.score <= -20)
        {
            player.GetComponent<Mouvement>().enabled = false;
            Time.timeScale = 0;
            paused = true;
            Cursor.visible = true;
            overMenu.SetActive(true);
        }
       
        
        
        if (Input.GetButtonDown("Cancel"))
        {
            if (!paused)
            {
                player.GetComponent<Mouvement>().enabled = false;
                Time.timeScale = 0;
                paused = true;
                Cursor.visible = true;
                menu.SetActive(true);
            }
            else
            {
                player.GetComponent<Mouvement>().enabled = true;
                Time.timeScale = 1;
                Cursor.visible = true;
                paused = false;
                menu.SetActive(false);
            }
        }
    }
}
