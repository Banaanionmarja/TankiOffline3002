using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    public Text scoreText;
    public Text HealthText;
    public Text livesText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetScore(float score)
    {
        scoreText.text = "Score: " + score;
    }
    public void SetHealth(float health, float max)
    {
        scoreText.text = "Health: " + health + "/" + max;
    }
    public void SetLives(float current, float max)
    {
        scoreText.text = "Lives: " + current + "/" + max;
    }
}
