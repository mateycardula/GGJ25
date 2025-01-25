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
        // Spawn bubbles
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

        // Check for pop withing poppable area

        if ()



    }



    public void GameOver()
    {
        // Transition to GameOver scene
        Destroy(gameObject);
    }
}