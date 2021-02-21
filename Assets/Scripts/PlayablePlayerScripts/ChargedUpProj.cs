using UnityEngine;
using System.Collections;

public class ChargedUpProj : MonoBehaviour {

    public LayerMask whatIsEnemy;

    private Rigidbody2D rb2d;
    private float timer;
    public float travelDuration = 2f;

    public float radius;

    void OnEnable()
    {
        timer = 0;
    }

    void Update()
    {
        var hits = Physics2D.CircleCastAll(transform.position, radius, Vector2.zero, radius, whatIsEnemy);

        foreach (var hit in hits)
        {
            if(hit.collider.gameObject.layer == 11)
            {
                ExplosionPool.Instance.impactPoint = hit.point;
                ExplosionPool.Instance.ExplodeHere();

                hit.collider.GetComponent<EnemyBehaviourTemplate>().TakeDamage(3f);
            }
            if (hit.collider.gameObject.layer == 11 && hit.collider.gameObject.CompareTag("Boss"))
            {
                ExplosionPool.Instance.impactPoint = hit.point;
                ExplosionPool.Instance.ExplodeHere();

                hit.collider.GetComponent<EnemyBehaviourTemplate>().TakeDamage(3f);
                gameObject.SetActive(false);
            }
            
        }

        timer += Time.deltaTime;
        if (timer >= travelDuration)
        {
            gameObject.SetActive(false);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.layer == 8)
        {
            ExplosionPool.Instance.impactPoint = col.contacts[0].point;
            ExplosionPool.Instance.ExplodeHere();

            this.gameObject.SetActive(false);
        }
    }

}
