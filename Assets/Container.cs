using System.Collections.Generic;
using UnityEngine;

public class Container : MonoBehaviour
{
    [SerializeField] private Transform bubbleTransform;
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

        if (elapsedTime >= Config.Instance.SPAWN_ELAPSED_TIME_INTERVAL)
        {
            elapsedTime = 0f;
            int randomInt = Random.Range(0, 100);

            if (randomInt < Config.Instance.SPAWN_CHANCE)
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
            Destroy(bubble.gameObject);
        }
    }
}