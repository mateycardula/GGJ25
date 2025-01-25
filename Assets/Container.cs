using UnityEngine;

public class Container : MonoBehaviour
{
    [SerializeField] private GameObject bubble;
    private float elapsedTime = 0f;
    
    void Start()
    {
    }
    
    void Update()
    {
        elapsedTime += Time.deltaTime;
        
        if (elapsedTime >= 1f)
        {
            elapsedTime = 0f;
            int randomInt = Random.Range(0, 100);
            
            if (randomInt < Config.Instance.SPAWN_CHANCE)
            {
                Instantiate(bubble);
            }
        }
    }

    public void GameOver()
    {
        // Transition to GameOver scene
        Destroy(gameObject);
    }
}