using UnityEngine;
using Random = System.Random;

public class Bubble : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] private Collider2D liquidArea;    
    private SpriteRenderer _spriteRenderer;
    private Transform _transform;
    private Color _color;
    private bool _hasReachedHorizon;
    private float _gravity;
    private float _size;
    private int _mass;
    // private Level
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        _transform = GetComponent<Transform>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _mass = Config.Instance.MASS.Item2;
        _color = Config.Instance.colors[new Random().Next(0, Config.Instance.colors.Count)];
        _hasReachedHorizon = false;
        _gravity = UnityEngine.Random.Range(Config.Instance.GRAVITY_RANGE.Item1, Config.Instance.GRAVITY_RANGE.Item2);
        _size = UnityEngine.Random.Range(Config.Instance.SIZE_RANGE.Item1, Config.Instance.SIZE_RANGE.Item2);
    }

    void Start()
    {
        _spriteRenderer.color = _color.BubbleColor;
        _transform.localScale = new Vector3(_size, _size, _size);
        
        var vector3 = _transform.position;
        vector3.y = Config.Instance.LEFT_SPAWN_LIMIT.position.y;
        _transform.position = vector3;

        var position = _transform.position;
        position.x = UnityEngine.Random.Range(Config.Instance.LEFT_SPAWN_LIMIT.position.x, Config.Instance.RIGHT_SPAWN_LIMIT.position.x);
        _transform.position = position;
        
        rb.mass = _mass;
        rb.gravityScale = _gravity;
    }
    private float elapsedTime = 0f;

    void Update()
    {
        elapsedTime += Time.deltaTime;
        
        if (elapsedTime >= 0.25f && !_hasReachedHorizon)
        {
            Vector2 dirVector;
            float direction = UnityEngine.Random.Range(0f, 100f);
            if (direction < 50f)
            {
                dirVector = Vector2.right*25f;
            }
            else
            {
                dirVector = Vector2.left*25f;
            }
            elapsedTime = 0f;
            rb.AddForce(dirVector, ForceMode2D.Force);
        }
    }

    private void OnMouseDown() {
        // Add score
        // Instantiate(poof, transform.position, transform.rotation);
        
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other == liquidArea)
        {
            rb.gravityScale = _gravity;
            rb.mass = Config.Instance.MASS.Item2;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other == liquidArea)
        {
            _hasReachedHorizon = true;
            rb.gravityScale = Config.Instance.HORIZON_GRAVITY;
            rb.mass = Config.Instance.MASS.Item1;
        }
    }
}
