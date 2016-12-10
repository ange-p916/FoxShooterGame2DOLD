using UnityEngine;
using System.Collections;
using Rewired;

public class PlayerProjectile : MonoBehaviour
{
    Player input;
    Rigidbody2D rb2d;
    public LayerMask collisionMask;
    public float rotAngle;
    public float timer = 0f;
    public float travelDuration = 2f;
    PlayerShotController psc;
    PlayablePlayer player;
    tk2dSpriteAnimator anim;

    public bool isChargingUp = false;

    void Start()
    {
        psc = GetComponent<PlayerShotController>();
        anim = GetComponent<tk2dSpriteAnimator>();
        rb2d = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayablePlayer>();
        transform.localScale = new Vector3(player.lookRight ? 1 : -1, 1, 1);
        input = ReInput.players.GetPlayer(0);
    }

    void OnEnable()
    {
        timer = 0;
    }

    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, rb2d.velocity, 0.1f, collisionMask);

        if(hit)
        {
            rb2d.velocity = hit.normal + Vector2.up;
            Vector2 reflectDir = Vector2.Reflect(rb2d.velocity, hit.normal);
            float rot = rotAngle - Mathf.Atan2(reflectDir.y, reflectDir.x) * Mathf.Rad2Deg;
            transform.eulerAngles = new Vector3(0, 0, rot);
        }

        timer += Time.deltaTime;
        if (timer >= travelDuration)
        {
            gameObject.SetActive(false);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        //11 = enemy layer, 16 = medusahead layer
        if (col.gameObject.layer == 11 || col.gameObject.layer == 16)
        {
            col.gameObject.GetComponent<EnemyBehaviourTemplate>().TakeDamage(1f);
            //col.gameObject.GetComponent<EnemyFlyingAndChasing>().startToChase = true;
            //col.gameObject.SendMessage("TakeDamage", 1, SendMessageOptions.RequireReceiver);
            this.gameObject.SetActive(false);

            ExplosionPool.Instance.impactPoint = col.contacts[0].point;
            ExplosionPool.Instance.ExplodeHere();

        }
        else if (col.gameObject.layer == 11 && col.gameObject.CompareTag("Boss"))
        {
            col.gameObject.GetComponent<EnemyBehaviourTemplate>().TakeDamage(1f);
            this.gameObject.SetActive(false);

            ExplosionPool.Instance.impactPoint = col.contacts[0].point;
            ExplosionPool.Instance.ExplodeHere();
        }

        //ground layer
        if (col.gameObject.layer == 8)
        {
            this.gameObject.SetActive(false);

            ExplosionPool.Instance.impactPoint = col.contacts[0].point;
            ExplosionPool.Instance.ExplodeHere();
        }
    }

}
