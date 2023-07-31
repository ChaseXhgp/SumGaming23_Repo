using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public TextMeshProUGUI highScoreText;

    void Start() 
    {
        string hScore = PlayerPrefs.GetFloat("highScore").ToString("F1");

        highScoreText.text = string.Format("High Score: {0} Seconds", hScore);
    }

    void Awake() 
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

   public void PlayGame()
   {
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
   }

   public void QuitGame()
   {
    Application.Quit();
   }
}
