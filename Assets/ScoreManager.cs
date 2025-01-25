using System;
using Unity.VisualScripting;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int score;
    public float multiplier;
    public Bubble bubble;
    public float multiplierTimer;
    public float levelTimer;
    public Level Level;
    void Awake()
    {
        bubble.scoreManager = this;
    } 
    void Start()
    {
        multiplier = Config.Instance.STARTING_MULTIPLIER;
        score = 0;
        multiplierTimer = 0f;
        levelTimer = Config.Instance.LEVEL_TIMER;
        Level = Config.Instance.LEVELS[0];
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
        if (Level.level == Config.Instance.MAX_LEVELS)
        {
            return;
        }
        
        Level = Config.Instance.LEVELS[Level.level];
    }

    public void AddScore(int points)
    {
        score += (int) Math.Round(points * multiplier);
    }

    public void IncreaseMultiplier()
    {
        multiplierTimer = Config.Instance.MULTIPLIER_TIMER;
        multiplier *= Config.Instance.MULTIPLIER_INCREASE;
    }
    
}
