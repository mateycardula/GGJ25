using System;
using Unity.VisualScripting;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private OvenUI _ovenUI;

    [SerializeField]
    private LevelSwitch levelSwitch;
    
    public float multiplier;
    public Bubble bubble;
    public float multiplierTimer;
    public float levelTimer;
    
    private Level _level;
    public Level Level
    {
        get => _level;
        set
        {
            if (_level?.level < value.level && _level != null) 
            {
                levelSwitch.targetAngle -= 90.0f;
            }

            if (_level?.level > value.level && _level != null)
            {
                levelSwitch.targetAngle += 90.0f;
            }
            
            _level = value;
            _ovenUI.LevelDisplayCounter.text = _level.level.ToString();
        }
    }

    private int _score;
    public int Score
    {
        get => _score;
        set
        {
            _score = value;
            _ovenUI.ScoreDisplayCounter.text = _score.ToString();
        }
    }

    void Awake()
    {
        bubble.scoreManager = this;
    } 
    void Start()
    {
        multiplier = Config.Instance.STARTING_MULTIPLIER;
        Score = 0;
        multiplierTimer = 0f;
        levelTimer = Config.Instance.LEVEL_TIMER;
        Level = Config.Instance.LEVELS[0];
        _ovenUI.LevelDisplayCounter.text = _level.level.ToString();
        _ovenUI.ScoreDisplayCounter.text = _score.ToString();
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
        if (_level.level == Config.Instance.MAX_LEVELS)
        {
            return;
        }
        
        Level = Config.Instance.LEVELS[_level.level];
    }

    public void AddScore(int points)
    {
        Score += (int) Math.Round(points * multiplier);
    }

    public void IncreaseMultiplier()
    {
        multiplierTimer = Config.Instance.MULTIPLIER_TIMER;
        multiplier *= Config.Instance.MULTIPLIER_INCREASE;
    }
    
}
