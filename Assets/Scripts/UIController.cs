using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Android;

public class UIController : MonoBehaviour
{

    public Text scoreText;
    public Text healthText;
    public Text livesText;
    public Text endScore;

    public GameObject pauseMenu;
    public GameObject respawnScreen;
    public GameObject endScreen;
    public GameObject hud;

    private bool paused = false;

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
        if (paused && Input.GetButtonDown("Quit"))
        {
            QuitGame();
        }
    }

    public void SetScore(float score)
    {
        scoreText.text = "Score: " + score;
    }
    public void SetHealth(float current, float max)
    {
        healthText.text = "Health: " + current + "/" + max;
    }
    public void SetLives(int current, int max)
    {
        livesText.text = "Lives: " + current + "/" + max;
    }


    public void TogglePause()
    {
        if (pauseMenu.activeInHierarchy)
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
            paused = false;
        }
        else
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
            paused = true;
        }
    }

    public void ShowRespawnScreen()
    {
        respawnScreen.SetActive(true);
        Time.timeScale = 0.2f;
    }

    public void HideRespawnScreen()
    {
        respawnScreen.SetActive(false);
        Time.timeScale = 1f;
    }

    public void ShowEndScreen (float score)
    {
        hud.SetActive(false);
        endScore.text = "Pisteesi: " + score;
        endScreen.SetActive(true);
    }

    public void Restart()
    {
        hud.SetActive(true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }



}
