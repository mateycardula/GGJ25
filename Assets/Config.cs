using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.Serialization;


[DefaultExecutionOrder(-999)]
public class Config : MonoBehaviour
{
    public float SPAWN_CHANCE = 50;
    public Tuple<int, int> MASS = new Tuple<int, int>(1, 2);
    public float HORIZON_GRAVITY = 0.3f;
    public Tuple<float, float> GRAVITY_RANGE = new Tuple<float, float>(-0.1f, -0.2f);
    public Tuple<float, float> SIZE_RANGE = new Tuple<float, float>(0.6f, 0.85f);
    public Transform LEFT_SPAWN_LIMIT, RIGHT_SPAWN_LIMIT;
    public List<Color> colors = new List<Color>();
    public int NUMBER_OF_COLLISIONS_TO_POP = 3;
    public float SPAWN_ELAPSED_TIME_INTERVAL = 1f;
    public float STARTING_MULTIPLIER = 1f;
    public int SCORE_FOR_POPPED_ABOVE_HORIZON = 1;
    public int SCORE_FOR_POPPED_BELOW_HORIZON = 1;
    public float MULTIPLIER_TIMER = 5f;
    public float MULTIPLIER_INCREASE = 1.2f;
    public float LEVEL_TIMER = 30f;
    public int MAX_LEVELS = 3;
    public float POWERUP_LID_TIMER = 5f;
    public float BASE_LIQUID_SPEED = 2f;

    public List<Level> LEVELS = new List<Level>();
    
    private static Config  s_Instance;
    public static Config Instance
    {
        get
        {
            return s_Instance;
        }

        private set => s_Instance = value;
    }
    
    
    private void Awake()
    {
        if (s_Instance == this)
        {
            return;
        }

        if (s_Instance == null)
        {
            s_Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        var level1 =
            new Level
            {
                mass = Instance.MASS.Item2,
                level = 1,
                spawnChance = Instance.SPAWN_CHANCE,
                spawnInterval = Instance.SPAWN_ELAPSED_TIME_INTERVAL,
                liquidSpeed = Instance.BASE_LIQUID_SPEED
            };
        var level2 =
            new Level
            {
                mass = Instance.MASS.Item2 + 1,
                level = 2,
                spawnChance = Instance.SPAWN_CHANCE + 10,
                spawnInterval = Instance.SPAWN_ELAPSED_TIME_INTERVAL - 0.2f,
                liquidSpeed = Instance.BASE_LIQUID_SPEED + 2f
            };
        var level3 =
            new Level
            {
                mass = Instance.MASS.Item2 + 2,
                level = 3,
                spawnChance = Instance.SPAWN_CHANCE + 20,
                spawnInterval = Instance.SPAWN_ELAPSED_TIME_INTERVAL - 0.35f,
                liquidSpeed = Instance.BASE_LIQUID_SPEED + 4f
            };
        
        LEVELS.Add(level1);
        LEVELS.Add(level2);
        LEVELS.Add(level3);

    }
}

public class Level
{
    public int level { get; set; }
    public float mass { get; set; }
    public float spawnChance { get; set; }
    public float spawnInterval { get; set; }
    
    public float liquidSpeed { get; set; }
}