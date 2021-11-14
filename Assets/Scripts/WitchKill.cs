using System;
using System.Collections;
using UnityEngine;

public class WitchKill : MonoBehaviour
{
    public event Action PlayerDied;

    static WitchKill _instance;

    public static WitchKill GetInstance()
    {
        return _instance;
    }

    void Awake()
    {
        _instance = this;
    }

    IEnumerator Start()
    {
        while (transform.position.y < 6.2 && transform.position.y > -6.2)
        {
            yield return new WaitForSeconds(1f);
        }
        PlayerDied?.Invoke();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        bool powerup = collision.collider.CompareTag("Potion");
        bool coin = collision.collider.CompareTag("Coins");

        if (!powerup && !coin)
        {
            PlayerDied?.Invoke();
        }
            
    }
}
