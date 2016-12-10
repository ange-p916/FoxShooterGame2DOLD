using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

public class PlayerHealthController : MonoBehaviour
{
    public Image deathImg;
    private Slider healthBar;
    private Text healthText;
    tk2dSprite sprender;
    //private SpriteRenderer sprender;
    private PlayablePlayer player;
    public bool isInvincible = false;
    tk2dSpriteAnimator anim;
    private Text text;
    public float curHealth = 7;
    public float maxHealth = 7;

    void Start()
    {
        anim = GetComponent<tk2dSpriteAnimator>();
        curHealth = GameScript.current.currentHealth;
        maxHealth = GameScript.current.maxHealth;
        print(GameScript.current.currentHealth + " " + GameScript.current.maxHealth);
        player = GetComponent<PlayablePlayer>();
        sprender = GetComponent<tk2dSprite>();
        healthBar = GameObject.Find("HealthCanvas").transform.GetChild(0).GetComponent<Slider>();
        healthText = GameObject.Find("HealthCanvas").transform.GetChild(1).GetComponent<Text>();
    }

    void Update()
    {
        healthBar.value = curHealth;
        healthBar.maxValue = maxHealth;
        healthText.text = "Health: " + curHealth;
    }



    public void HealAgain()
    {
        if (curHealth <= maxHealth)
        {
            curHealth++;
        }
    }


    public void PlayerTakeDamage(float damage, Transform enemyPos,float knockbackvel = 2f)
    {
        if (!isInvincible)
        {
            if (!MusicManager.Instance.hitsound.isPlaying)
            {
                MusicManager.Instance.hitsound.PlayDelayed(0f);
            }

            player.velocity.x = (enemyPos.position - this.transform.position).x > 0 ? Vector2.left.x * knockbackvel : Vector2.right.x * knockbackvel;

            curHealth -= damage;
            healthBar.value = curHealth;
            isInvincible = true;
            StartCoroutine(TakeDamageAgain());
            StartCoroutine(BlinkColor());
        }

        if (curHealth <= 0)
        {
            CheckpointManager.Instance.isDead = true;
        }
    }

    private IEnumerator TakeDamageAgain()
    {
        yield return new WaitForSeconds(1f);
        isInvincible = false;
    }

    private IEnumerator BlinkColor()
    {
        yield return new WaitForSeconds(0.01f);
        sprender.color = Color.black;
        yield return new WaitForSeconds(1f);
        sprender.color = Color.white;
    }

    void DeathSequence()
    {

    }

    IEnumerator DeathSequenceCo()
    {
        deathImg.enabled = true;
        //play death anim
        if(!anim.IsPlaying("PlayerDeathIdle"))
        {
            anim.Play("PlayerDeathIdle");
        }
        yield return new WaitForSeconds(2f); //hold first frame
        //lerp to middle of screen
        //play animation
        if(!anim.IsPlaying("PlayerDeath"))
        {
            anim.Play("PlayerDeath");
        }
        //show canvas

    }

}
