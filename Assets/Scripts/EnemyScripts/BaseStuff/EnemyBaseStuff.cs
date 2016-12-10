using UnityEngine;
using System.Collections;

public class EnemyBaseStuff : MonoBehaviour
{
    public enum WhatEnemyIsThis { Stationary, Throwing, Jumping, Flying, Charging, Boss };
    public WhatEnemyIsThis EnemyType;

    public Vector3 startPosition;

    public float health;

    protected Rigidbody2D rb2d;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        startPosition = this.transform.position;
        SetEnemyHealth();
    }

    void OnEnable()
    {
        SetEnemyHealth();
    }

    void Update()
    {
        if (health <= 0)
        {
            if (Random.Range(1, 100) < 25)
            {
                CreateExtraLivesScript.Instance.healthPosition = this.gameObject.transform;
                CreateExtraLivesScript.Instance.CreateLives();
            }
            this.gameObject.SetActive(false);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
    }

    public void SetEnemyHealth()
    {
        this.GetComponent<SpriteRenderer>().material.color = Color.white;
        if (EnemyType == WhatEnemyIsThis.Stationary)
        {
            health = 1f;
        }

        if (EnemyType == WhatEnemyIsThis.Throwing)
        {
            health = 2f;
        }

        if (EnemyType == WhatEnemyIsThis.Jumping)
        {
            health = 2f;
        }
        if (EnemyType == WhatEnemyIsThis.Charging)
        {
            health = 5f;
        }
        if (EnemyType == WhatEnemyIsThis.Flying)
        {
            health = 1f;
        }
        if (EnemyType == WhatEnemyIsThis.Boss)
        {
            health = 50f;
        }
    }
}
