using System.Collections;
using System.Collections.Generic;
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

        if (currentLives > 0)
        {
            if (player == null)
            {
                ui.ShowRespawnScreen();

                if (Input.GetButtonDown("Restart"))
                {
                    player = spawner.spawnPlayer();
                    currentLives--;
                    ui.SetLives(currentLives, lives);
                    ui.HideRespawnScreen();
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


    public void SetHealth(float current, float maxHealth)
    {
        if (current < 0)
        {
            current = 0f;
        }
        ui.SetUIHealth(current, maxHealth);
    }
}
