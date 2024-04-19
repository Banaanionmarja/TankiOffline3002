using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    public Text scoreText;
    public Text healthText;
    public Text livesText;

    public GameObject pauseMenu;
    public GameObject respawnScreen;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            TogglePause();
        }
    }

    public void SetScore(float score)
    {
        scoreText.text = "Score: " + score;
    }
    public void SetUIHealth(float health, float max)
    {
        healthText.text = "Health: " + health + "/" + max;
    }
    public void SetLives(float current, float max)
    {
        livesText.text = "Lives: " + current + "/" + max;
    }


    public void TogglePause()
    {
        if (pauseMenu.activeInHierarchy)
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
        }
        else
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
        }
    }


    public void QuitGame()
    {
# if UNITY_EDITOR
        Application.Quit();
#else
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }



    public void ShowRespawnScreen()
    {
        respawnScreen.SetActive(true);
    }

    public void HideRespawnScreen()
    {
        respawnScreen.SetActive(false);
    }
}
