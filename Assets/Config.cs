using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;


[DefaultExecutionOrder(-999)]
public class Config : MonoBehaviour
{
    public float SPAWN_CHANCE = 30;
    public Tuple<int, int> MASS = new Tuple<int, int>(1, 2);
    public float HORIZON_GRAVITY = 0.3f;
    public Tuple<float, float> GRAVITY_RANGE = new Tuple<float, float>(-0.1f, -0.2f);
    public Tuple<float, float> SIZE_RANGE = new Tuple<float, float>(0.4f, 0.65f);
    public Transform LEFT_SPAWN_LIMIT, RIGHT_SPAWN_LIMIT;
    public List<Color> colors = new List<Color>();
    public int NUMBER_OF_COLLISIONS_TO_POP = 3;
    
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
    }
}