using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthBar : MonoBehaviour
{
    public Image fillBar;
    public float health;

    public void LoseHealth(int value)
    {
        if(health <= 0)
        {
            FindObjectOfType<Fox>().Die();
            

        }
        health -= value;
        fillBar.fillAmount = health / 100;

        if(health<=0)
        {
            Debug.Log("YOU DIED");
        }
    }

    private void Update()
    {
       // if (Input.GetKeyDown(KeyCode.Return))
         //   LoseHealth(35);
    }
}
