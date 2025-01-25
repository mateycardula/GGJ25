using System;
using UnityEngine;

public class Lid : MonoBehaviour
{
    private float timer = 0;
    
    void Start()
    {
        this.gameObject.SetActive(false);
    }

    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            this.gameObject.SetActive(false);
            timer = 0;
        }
    }

    private void OnEnable()
    {
        // animacija
        timer = Config.Instance.POWERUP_LID_TIMER;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bubble"))
        {
            other.gameObject.GetComponent<Bubble>().DestroyBubble(0);
        }
    }
}
