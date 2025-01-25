using System;
using Unity.VisualScripting;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int score;
    public float multiplier;
    public Bubble bubble;
    public float multiplierTimer;
    public float spawnInterval;
    public float levelTimer;
    void Awake()
    {
        bubble.scoreManager = this;
    } 
    void Start()
    {
        multiplier = Config.Instance.STARTING_MULTIPLIER;
        score = 0;
        multiplierTimer = 0f;
        spawnInterval = Config.Instance.SPAWN_ELAPSED_TIME_INTERVAL;
        levelTimer = Config.Instance.LEVEL_TIMER;
    }

    void Update()
    {
        ManageMultiplierTimer();
        ManageLevelTimer();
    }

    private void ManageMultiplierTimer()
    {
        if (multiplierTimer > 0)
        {
            multiplierTimer -= Time.deltaTime;
        }
        else
        {
            multiplierTimer = 0;
            multiplier = Config.Instance.STARTING_MULTIPLIER;
        }
    }
    
    private void ManageLevelTimer()
    {
        if (levelTimer > 0)
        {
            levelTimer -= Time.deltaTime;
        }
        else
        {
            IncreaseLevel();
            levelTimer = Config.Instance.LEVEL_TIMER;
        }
    }

    private void IncreaseLevel()
    {
        spawnInterval = (float) Math.Max(spawnInterval - 0.1, 0.1);
    }

    public void AddScore(int points)
    {
        score += (int) Math.Round(points * multiplier);
    }

    public void IncreaseMultiplier(Vector3 position)
    {
        multiplierTimer = Config.Instance.MULTIPLIER_TIMER;
        multiplier *= Config.Instance.MULTIPLIER_INCREASE;
    }
    
}
