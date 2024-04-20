using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using Unity.Collections;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public Spawner spawner;

    public float score;
    public float scorePerTank;
    public int lives;
    public int enemyStartingAmount;
    public int maxEnemiesAmount;

    public int currentLives;
    public int currentEnemyAmount;

    private GameObject player;

    public static GameController instance;

    public UIController ui;

    public AudioSource audioSource;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < enemyStartingAmount; i++)
        {
            spawner.spawnEnemy();
        }

        player = spawner.spawnPlayer();

        score = 0f;
        currentLives = lives;
        currentEnemyAmount = enemyStartingAmount;

        ui.SetScore(score);
        ui.SetLives(currentLives, lives);

    }

    // Update is called once per frame
    void Update()
    {

        if (player == null)
        {
            if (currentLives > 0)
            {
                ui.ShowRespawnScreen();

                if (Input.GetButtonDown("Restart"))
                {
                    audioSource.Play();
                    player = spawner.spawnPlayer();
                    currentLives--;
                    ui.SetLives(currentLives, lives);
                    ui.HideRespawnScreen();
                }
            }
            else
            {
                ui.ShowEndScreen(score);
                if (Input.GetButtonDown("Restart"))
                {
                    ui.Restart();
                }
                if (Input.GetButtonDown("Quit") || Input.GetButtonDown("Cancel"))
                {
                    ui.QuitGame();
                }
            }
        }

    }

    public void EnemyDestroyed()
    {
        spawner.spawnEnemy();
        score += scorePerTank;
        ui.SetScore(score);
        if(currentEnemyAmount < maxEnemiesAmount)
        {
            spawner.spawnEnemy();
            currentEnemyAmount++;
        }
    }


    public void SetHealth(float currentHp, float maxHealth)
    {
        if (currentHp < 0) currentHp = 0f;
        ui.SetHealth(currentHp, maxHealth);
    }
}
