using UnityEngine;
using System.Collections;

public class BossHealthBarController : EnemyBehaviourTemplate {

    ThrowLogBoss logBoss;
    EnemyHealthBarController ehbc;
    CamLerpScript camLerp;
    UnlockableManager unlockStuff;
    Camera mCam;
    public TypeWriter twman;

    public bool shouldDiaEnable = false;

    [Header("Explosion stuff")]
    public bool initiateExplosion;
    public float cdToExplode = 0.3f;
    public float newCdToExplode = 0.3f;
    public float timeIsExploding = 0.02f;
    public float newTimeIsExploding = 0.02f;

    [Header("Camera Shake stuff")]
    public float elapsed = 0f;
    public float duration = 1f;
    public float shakeTime = 0.7f;
    public float magnitude = 0.5f;

    protected override void Start()
    {
        mCam = Camera.main;
        startPosition = this.transform.position;
        SetEnemyHealth();
        player = FindObjectOfType<PlayablePlayer>();
        camLerp = FindObjectOfType<CamLerpScript>();
        ehbc = FindObjectOfType<EnemyHealthBarController>();
    }

    protected override void Update()
    {
        Obliteration("ThrowLogBoss");
        Obliteration("FlyAndSlamBoss");
    }

    void Obliteration(string bossName)
    {
        if (health <= 0 && gameObject.name == bossName)
        {
            ehbc.isBossInitiated = false;
            ehbc.healthBar.gameObject.SetActive(false);
            if(GetComponent<FlyAndSlamBoss>() != null)
            {
                GetComponent<FlyAndSlamBoss>().enabled = false;
            }
            
            ExplodeWithin();
            StartCoroutine(CameraShake());
            StartCoroutine(WaitWithDestroying(4.5f, 3f));
            if (shouldDiaEnable)
            {
                twman.startDialogue = true;
            }
        }
    }

    IEnumerator DoCamStuffBossA(float timer)
    {
        yield return new WaitForSeconds(timer);
        ehbc.isBossInitiated = false;
        ehbc.healthBar.gameObject.SetActive(false);
        camLerp.startA = true;
    }

    void DoCamStuffBossB()
    {
    }

    IEnumerator WaitWithDestroying(float explosionTimer, float camTimer)
    {
        initiateExplosion = true;
        ehbc.healthBar.gameObject.SetActive(false);
        yield return new WaitForSeconds(explosionTimer);
        initiateExplosion = false;
        
        this.gameObject.SetActive(false);
    }

    void ExplodeWithin()
    {
        bool isExploding = false;

        if (initiateExplosion)
        {
            cdToExplode -= Time.deltaTime;
        }

        if (cdToExplode <= 0)
        {
            var losc = transform.localScale;
            var pointToExplodeAt = transform.TransformPoint(
                Random.Range(-losc.x, losc.x),
                Random.Range(-losc.y, losc.y), 0);

            isExploding = true;
            timeIsExploding -= Time.deltaTime;
            if (isExploding)
            {
                ExplosionPool.Instance.impactPoint = pointToExplodeAt;

                ExplosionPool.Instance.ExplodeHere();
            }

            if (timeIsExploding <= 0)
            {
                isExploding = false;
                cdToExplode = newCdToExplode;
            }

            if (!isExploding && cdToExplode >= 0)
            {
                timeIsExploding = newTimeIsExploding;
            }

        }
    }

    IEnumerator WaitAbitMan()
    {
        yield return new WaitForSeconds(1f);
        camLerp.switchingToCinMode = true;
    }

    IEnumerator CameraShake()
    {
        Vector3 originalCamPos = mCam.transform.position;
        while (elapsed < duration)
        {
            elapsed = Time.deltaTime;

            float percToComplete = elapsed / duration;
            float damper = 1f - Mathf.Clamp(4f * percToComplete - 3f, 0f, 1f);

            float x = Random.value * 1.5f - 1f;
            float y = Random.value * 1.5f - 1f;
            x *= magnitude * damper;
            y *= magnitude * damper;

            mCam.transform.position = new Vector3(x + originalCamPos.x, y + originalCamPos.y, originalCamPos.z);
            yield return null;
        }
        elapsed = 0f;
        mCam.transform.position = originalCamPos;
    }

}
