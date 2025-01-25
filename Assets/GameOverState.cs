using System;
using UnityEngine;

public class GameOverState : MonoBehaviour
{
    [SerializeField] private Container container;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bubble"))
        {
            container.GameOver();
        }
    }
}
