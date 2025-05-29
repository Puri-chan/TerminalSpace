using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.U2D;

public class PlayerSetActive : MonoBehaviour
{
    public PlayerController controller;
    public Animator animator, extralife;
    public int score, highscore = 0;
    public TextMeshProUGUI Score, Highscore, countdown;
    public ScreenStart scorefinal;
    public GameObject retrytext;
    bool gameoveronscreen = false, chance = false, isExtra = false;
    public bool isRestart = false, isfinishwatch = false;
    public AudioSource restartsound, gameovermusic;
    int extarlife = 1; //per game
    bool click = false;

    private void Start()
    {
        restartsound = GameObject.Find("GameoverSound").GetComponent<AudioSource>();
        gameovermusic = GameObject.Find("GameoverMusic").GetComponent<AudioSource>();
    }
    public void Update()
    {
        if (Input.touchCount > 0 && gameoveronscreen == true)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began && gameoveronscreen == true)
            {
                click = true;
            }
        }
        if (Input.GetMouseButtonDown(0) && gameoveronscreen == true) 
        {
            click = true;
        }

        if (click == true)
        {
            click = false;
            if (chance == false)
            {
                StartCoroutine(Restart());
            }
            else if (chance == true)
            {
                isExtra = true;
                StopCoroutine(extraLife());
                StartCoroutine(ADS());
            }
        }
        if (isfinishwatch == true)
        {
            Time.timeScale = 1;
            StartCoroutine(Continue());
            isfinishwatch = false;
            isExtra = false;
        }
    }

    //Player collide to enemy projectiles
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyProjectile") || collision.CompareTag("Enemy"))
        {
            //Player Dead
            scorefinal.ShakeActivate();
            scorefinal.Music.Stop();
            controller.isDead = true;
            controller.sprite.enabled = false;
            controller.deadsound.Play();
            controller.objparticle.SetActive(false); controller.objparticle2.SetActive(false);
            controller.colliderplayer.enabled = false;
            controller.explosion.Play(); controller.explosion2.Play();
            controller.isMoving = false;
            gameovermusic.Play();
            StartCoroutine(UIpopup());
        }
    }
    //Extra Life
    IEnumerator extraLife()
    {
        yield return new WaitForSeconds(3);
        chance = true; gameoveronscreen = true;
        extralife.SetBool("Extralife", true);
        for (int i = 10; i >= 0; i--)
        {
            countdown.text = i.ToString();
            yield return new WaitForSeconds(1);
            if (isExtra == true)
            {
                yield break;
            }
        }
        extralife.SetBool("Extralife", false);
        chance = false; gameoveronscreen = false;
        yield return new WaitForSeconds(1);
        StartCoroutine(UIpopup());
    }
    //ADS wow I love this one XDDDDDD
    IEnumerator ADS()
    {
        extarlife--;
        gameovermusic.Stop();
        restartsound.Play();
        gameoveronscreen = false;
        controller.transform.position = new Vector2(0, -2);
        yield return new WaitForSeconds(1);
        Time.timeScale = 0;
    }

    //AfterADS
    IEnumerator Continue()
    {
        yield return new WaitForSeconds(0.5f);
        extralife.SetBool("Extralife", false);
        yield return new WaitForSeconds(0.5f);
        isRestart = true;
        scorefinal.wave--;
        for (int i = 0; i < 5; i++)
        {
            controller.sprite.enabled = true;
            yield return new WaitForSeconds(0.05f);
            controller.sprite.enabled = false;
            yield return new WaitForSeconds(0.05f);
        }
        Reseteverything();
    }
    void Reseteverything()
    {
        controller.sprite.enabled = true;
        controller.isDead = false;
        controller.isMoving = true;
        controller.objparticle.SetActive(true); controller.objparticle2.SetActive(true);
        controller.colliderplayer.enabled = true;
        controller.isshooting = false;
        scorefinal.Music.Play();
        scorefinal.isWaveStart = true;
    }
    //Game Over
    IEnumerator UIpopup()
    {
        Debug.Log(chance);
        score = scorefinal.Score;
        if (score > highscore)
        {
            highscore = score;
        }
        Score.text = score.ToString();
        Highscore.text = highscore.ToString();
        yield return new WaitForSeconds(1);
        animator.SetBool("isGameover", true);
        yield return new WaitForSeconds(2);
        retrytext.SetActive(true);
        chance = false;
        gameoveronscreen = true;
    }

    //Game restart at start
    IEnumerator Restart()
    {
        gameovermusic.Stop();
        restartsound.Play();
        gameoveronscreen = false;
        retrytext.SetActive(false);
        controller.transform.position = new Vector2(0, -2);
        animator.SetBool("isGameover", false);
        yield return new WaitForSeconds(2f);
        isRestart = true;
        scorefinal.wave = 0;
        scorefinal.Score = 0;
        scorefinal.ScoreText.text = scorefinal.Score.ToString();
        scorefinal.multiply = 0;
        for (int i = 0; i < 5; i++)
        {
            controller.sprite.enabled = true;
            yield return new WaitForSeconds(0.05f);
            controller.sprite.enabled = false;
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(0.05f);
        Reseteverything();
        extarlife = 1;
    }
}
