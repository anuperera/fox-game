using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LifeCount : MonoBehaviour
{
    public Image[] lives;
    public int livesRemaning;

    public void LoseLife()
    {

        if (livesRemaning == 0)
            return;
        livesRemaning--;
        lives[livesRemaning].enabled = false;

        if(livesRemaning==0)
        {
            FindObjectOfType<Fox>().Die();
            
            
        }
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Return))
          //  LoseLife();
    }
}
