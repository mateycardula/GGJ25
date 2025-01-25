using System;
using Unity.VisualScripting;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int score;
    public float multiplier;
    public Bubble bubble;
    public float timer;
    void Awake()
    {
        bubble.scoreManager = this;
    } 
    void Start()
    {
        multiplier = Config.Instance.STARTING_MULTIPLIER;
        score = 0;
        timer = 0f;
    }

    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            multiplier = Config.Instance.STARTING_MULTIPLIER;
        }
    }

    public void AddScore(int points)
    {
        score += (int) Math.Round(points * multiplier);
    }

    public void IncreaseMultiplier(Vector3 position)
    {
        timer = Config.Instance.MULTIPLIER_TIMER;
        multiplier *= Config.Instance.MULTIPLIER_INCREASE;
    }
    
}
