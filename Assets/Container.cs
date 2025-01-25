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
    [SerializeField] private PowerUpsUI _powerUpsUI;
    [SerializeField] private Lid lid;
    private float elapsedTime = 0f;
    private Bubble bubble;
    public List<Bubble> bubbles = new List<Bubble>();

    private int popAllCounter;
    private int coolOffCounter;
    private int lidCounter;

    public int PopAllCounter
    {
        get => popAllCounter;
        set
        {
            popAllCounter = value;
            var popAllSprite = value > 0 ? _powerUpsUI.popAllButtonSprites[1] : _powerUpsUI.popAllButtonSprites[0];
            _powerUpsUI.popAllButton.image.sprite = popAllSprite;
            _powerUpsUI.popAllPowerUpCounter.text = value.ToString();
        }
    }

    public int CoolOffCounter
    {
        get => coolOffCounter;
        set
        {
            coolOffCounter = value;
            var coolOffSprite = value > 0 ? _powerUpsUI.lowerLevelButtonSprites[1] : _powerUpsUI.lowerLevelButtonSprites[0];
            _powerUpsUI.lowerLevelButton.image.sprite = coolOffSprite; 
            _powerUpsUI.popAllPowerUpCounter.text = value.ToString();
        }
    }

    public int LidCounter
    {
        get => lidCounter;
        set
        {
            lidCounter = value;
            var lidSprite = value > 0 ? _powerUpsUI.lidButtonSpries[1] : _powerUpsUI.lidButtonSpries[0];
            _powerUpsUI.lidButton.image.sprite = lidSprite;
            _powerUpsUI.lidPowerUpCounter.text = value.ToString();
        }
    }

    void Start()
    {
        LidCounter = 2;
        PopAllCounter = 3;
        CoolOffCounter = 3;
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
        if (PopAllCounter == 0)
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
        
        PopAllCounter -=  1;
    }
    
    // LOWER LEVEL
    public void LowerLevel()
    {
        if (CoolOffCounter == 0)
        {
            return;
        }
        _scoreManager.levelTimer = Config.Instance.LEVEL_TIMER;
        if (_scoreManager.Level.level != 1)
        {
            _scoreManager.Level = Config.Instance.LEVELS[_scoreManager.Level.level - 2];
        }

        CoolOffCounter -= 1;
    }
    
    // PUT LID ON
    public void putLidOn()
    {
        if (LidCounter == 0)
        {
            return;
        }

        lid.gameObject.SetActive(true);
        
        LidCounter -= 1;
    }
    
}