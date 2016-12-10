using UnityEngine;
using System.Collections;

public class EnemyBehaviourTemplate : MonoBehaviour {

    protected PlayablePlayer player;
    public enum WhatEnemyIsThis { Stationary, Throwing, Jumping, Flying, Charging, BossEasy, BossMedium, BossHard, TUTORIAL};
    public WhatEnemyIsThis EnemyType;
    [Header("Distances")]
    public float startActionDistance;
    public float distanceToPlayer;

    [Header("Start position")]
    public Vector3 startPosition;

    [Header("Health")]
    public float health;

    protected virtual void Start()
    {
        startPosition = this.transform.position;
        SetEnemyHealth();
    }

    protected virtual void Update()
    {
        if (health <= 0)
        {
            if (Random.Range(1, 100) < 35)
            {
                CreateExtraLivesScript.Instance.healthPosition = this.gameObject.transform;
                CreateExtraLivesScript.Instance.CreateLives();
            }
            if (this.gameObject.CompareTag("Boss"))
                return;

            this.gameObject.SetActive(false);
        }
    }
    protected virtual void OnEnable()
    {
        SetEnemyHealth();
    }
    public virtual void TakeDamage(float damage)
    {
        if(!MusicManager.Instance.hitsound.isPlaying)
        {
            MusicManager.Instance.hitsound.PlayDelayed(0f);
        }
        health -= damage;
    }

    public void SetEnemyHealth()
    {
        if (EnemyType == WhatEnemyIsThis.Stationary)
        {
            health = 2f;
        }

        if (EnemyType == WhatEnemyIsThis.Throwing)
        {
            health = 3f;
        }

        if (EnemyType == WhatEnemyIsThis.Jumping)
        {
            health = 3f;
        }
        if (EnemyType == WhatEnemyIsThis.Charging)
        {
            health = 4f;
        }
        if (EnemyType == WhatEnemyIsThis.Flying)
        {
            health = 2f;
        }
        if (EnemyType == WhatEnemyIsThis.BossEasy)
        {
            health = 30f;
        }
        if (EnemyType == WhatEnemyIsThis.BossMedium)
        {
            health = 50f;
        }
        if (EnemyType == WhatEnemyIsThis.BossHard)
        {
            health = 70f;
        }
        if(EnemyType == WhatEnemyIsThis.TUTORIAL)
        {
            health = 1000f;
        }
    }
}
