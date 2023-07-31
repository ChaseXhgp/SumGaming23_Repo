using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro; 

public class GameManager : MonoBehaviour
{
    private float currentTime = 0;

    [SerializeField]
    private bool _gameHasEnded = false;

    public GameObject player;
    public TextMeshProUGUI timerText; // Have to assign in the Inspector

    [SerializeField] GameObject mainCanvas, gameOverCanvas;

    [SerializeField] private float _highScore; 

    // Start is called before the first frame update
    void Start()
    {
        currentTime = 0;
        _gameHasEnded = false;

        mainCanvas.SetActive(true);
        gameOverCanvas.SetActive(false);

        //_highScore = PlayerPrefs.GetFloat("HighScore", 0);

    }

    void Awake()
    {
        _highScore = PlayerPrefs.GetFloat("highScore", 0);
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += 1 * Time.deltaTime; // Update the internal timer var

        // Update Timer Text to reflect to current time
        timerText.text = currentTime.ToString("F0");

    }


    private void SaveScore()
    {
        if (currentTime > PlayerPrefs.GetFloat("highScore", 0))
        {
            //Save new high score
            PlayerPrefs.SetFloat("highScore", currentTime);
        }
    }


    private void ReloadCurrentScene()
    {
        //Reset the scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void LoadStartMenu()
    {
         SceneManager.LoadScene(0); // loads main menu
    }

    public void EndGame()
    {
        if(!_gameHasEnded) //if its false
        {
            print("Kill Player");

            _highScore = PlayerPrefs.GetFloat("highScore");
            SaveScore();

            _gameHasEnded = true;

            //Disable the player
            player.GetComponent<FPSController>().enabled = false;


            //Enable lose panel
            mainCanvas.SetActive(false);
            gameOverCanvas.SetActive(true);

            Invoke("LoadStartMenu", 2f);
        }
    }
    
}
