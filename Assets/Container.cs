using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using Random = UnityEngine.Random;

public class Container : MonoBehaviour
{
    [SerializeField] private Transform bubbleTransform;
    [SerializeField] private ScoreManager _scoreManager;
    [SerializeField] private Lid lid;
    private float elapsedTime = 0f;
    private Bubble bubble;
    public List<Bubble> bubbles = new List<Bubble>();

    private int popAllCounter { get; set; } = 3;
    private int coolOffCounter { get; set; } = 4;
    private int lidCounter { get; set; } = 2;
    void Start()
    {
        bubble = bubbleTransform.GetComponent<Bubble>();
    }

    void Update()
    {
        // Spawn bubbles
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= _scoreManager.Level.spawnInterval)
        {
            elapsedTime = 0f;
            int randomInt = Random.Range(0, 100);

            if (randomInt < _scoreManager.Level.spawnChance)
            {
                bubble.container = this;
                var newBubble = Instantiate(bubbleTransform, Vector3.zero, Quaternion.identity);
                bubbles.Add(newBubble.GetComponent<Bubble>());
            }
        }
        
        if (Input.GetMouseButtonDown(0))
        {
            var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
            var hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider)
            {
                if (hit.collider.CompareTag("Bubble"))
                {
                    // ReSharper disable once Unity.PerformanceCriticalCodeInvocation
                    hit.collider.gameObject.GetComponent<Bubble>()?.DestroyBubbleOnTap();
                }
            }
        }
        
    }
    
    public void GameOver()
    {
        // Transition to GameOver scene
        Destroy(gameObject);
    }

    public void DeleteBubbles(HashSet<Bubble> deleteBubbles)
    {
        foreach (var b in deleteBubbles)
        {
            b.DestroyBubble(0.3f);
        }
    }
    
    // POWER UPS
    public void PopAllFromColor()
    {
        if (popAllCounter == 0)
        {
            return;
        }
        var color = bubbles[Random.Range(0, bubbles.Count)].GetColor();
        var destroyBubbles = bubbles.FindAll(b => b.GetColor() == color);

        foreach (var b in destroyBubbles)
        {
            _scoreManager.AddScore(Config.Instance.SCORE_FOR_POPPED_BELOW_HORIZON);
            b.DestroyBubble(0.3f);
        }
        
        _scoreManager.IncreaseMultiplier();
        
        popAllCounter -=  1;
    }
    
    // LOWER LEVEL
    public void LowerLevel()
    {
        if (coolOffCounter == 0)
        {
            return;
        }
        _scoreManager.levelTimer = Config.Instance.LEVEL_TIMER;
        if (_scoreManager.Level.level != 1)
        {
            _scoreManager.Level = Config.Instance.LEVELS[_scoreManager.Level.level - 2];
        }

        coolOffCounter -= 1;
    }
    
    // PUT LID ON
    public void putLidOn()
    {
        if (lidCounter == 0)
        {
            return;
        }

        lid.gameObject.SetActive(true);
        
        lidCounter -= 1;
    }
    
}