using System.Collections.Generic;
using UnityEngine;

public class Container : MonoBehaviour
{
    [SerializeField] private Transform bubbleTransform;
    [SerializeField] private ScoreManager _scoreManager;
    private float elapsedTime = 0f;
    private Bubble bubble;
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

            Debug.Log(_scoreManager.Level);
            if (randomInt < _scoreManager.Level.spawnChance)
            {
                bubble.container = this;
                var newBubble = Instantiate(bubbleTransform, Vector3.zero, Quaternion.identity);
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

    public void DeleteBubbles(HashSet<Bubble> bubbles)
    {
        foreach (var bubble in bubbles)
        {
            bubble.DestroyBubble(0.3f);
        }
    }
}