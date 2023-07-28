using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class GameManager : MonoBehaviour
{
    private float currentTime = 0;
    public TextMeshProUGUI timerText; // Have to assign in the Inspector

    // Start is called before the first frame update
    void Start()
    {
        currentTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += 1 * Time.deltaTime; // Update the internal timer var

        // Update Timer Text to reflect to current time
        timerText.text = currentTime.ToString("F0");

    }

    public void GameOver(){
        //This will reset the scene
    }
}
