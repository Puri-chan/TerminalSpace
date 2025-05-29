using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.U2D;


public class ScreenStart : MonoBehaviour
{
    public bool intro, isWaveStart, gamebegin = false;
    public ParticleSystem BGEffect, BGEffect2, LogoEffect;
    public PlayerController PlayerController;
    public PlayerAnimation PlayerAnimation;
    public GameObject[] enemies;
    public Transform[] wave1, wave2, wave3, wave4, wave5, wave6, wave7, wave8, wave9, wave10;
    int enemiesremain = -1;
    public int Score = 0, wave = 1, multiply;
    public TextMeshProUGUI ScoreText;
    public Animator scoretext;
    public PlayerSetActive playeractive;
    public AudioSource Music, Intromusic, IntroSound;
    public GameObject Maincam, ShakeCam;

    void Start()
    {
        StartCoroutine(WaitTimer());
        Score = 0;
        Music = GameObject.Find("GameEvent").GetComponent<AudioSource>();
        Intromusic = GameObject.Find("MusicIntro").GetComponent<AudioSource>();
        IntroSound = GameObject.Find("IntroSound").GetComponent<AudioSource>();
        ScoreText = GameObject.FindGameObjectWithTag("Score").GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        Debug.Log(enemiesremain);
        //Gain More Hp and speed
        if (enemiesremain == 0)
        {
            playeractive.isRestart = false;
        }
        if (enemiesremain == 0 && isWaveStart == true)
        {
            Debug.Log("Wave: " + wave);
            isWaveStart = false;
            wave++;
            if(wave > 10)
            {
                multiply++;
                wave = 1;
            }
        }

        if (!isWaveStart && gamebegin) // If wave not started yet
        {
            switch (wave)
            {
                case 1: //wave 1
                    StartCoroutine(Wave1());
                    break;
                case 2:
                    StartCoroutine(Wave2());
                    break;
                case 3:
                    StartCoroutine(Wave3());
                    break;
                case 4:
                    StartCoroutine(Wave4());
                    break;
                case 5:
                    StartCoroutine(Wave5());
                    break;
                case 6:
                    StartCoroutine(Wave6());
                    break;
                case 7:
                    StartCoroutine(Wave7());
                    break;
                case 8:
                    StartCoroutine(Wave8());
                    break;
                case 9:
                    StartCoroutine(Wave9());
                    break;
                case 10:
                    StartCoroutine(Wave10());
                    break;

            }
        }
    }
    IEnumerator WaitTimer()
    {
        yield return new WaitForSeconds(1.5f);
        LogoEffect.Play();
        Intromusic.Play(); IntroSound.Play();
        yield return new WaitForSeconds(0.5f);
        intro = true;
    }

    public IEnumerator Starting()
    {
        yield return new WaitForSeconds(1.5f);
        BGEffect.Play(); BGEffect2.Play();
        PlayerAnimation.animator.SetBool("GameStart", true);

        yield return new WaitForSeconds(1f);
        PlayerController.GameStart = true;
        yield return new WaitForSeconds(1f);
        gamebegin = true;
        scoretext.SetBool("IsStart", true);
        Music.Play();
    }

    public void updatescore(int score)
    {
        if( PlayerController.isDead == false)
        {
            Score += score;
            ScoreText.text = Score.ToString();
        }
    }

    public void EnemiesRemaining()
    {
        StartCoroutine(ShakeEffect());
        enemiesremain--;
    }
    public void ShakeActivate()
    {
        StartCoroutine(ShakeEffect());
    }    
    IEnumerator ShakeEffect()
    {
        Maincam.SetActive(false); ShakeCam.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        Maincam.SetActive(true); ShakeCam.SetActive(false);
    }
    IEnumerator Wave1()
    {
        isWaveStart = true;
        enemiesremain = wave1.Length;
        yield return new WaitForSeconds(1f);

        for (int i = 0; i < wave1.Length; i++)
        {
            GameObject enemy = Instantiate(enemies[0]);
            enemy.transform.position = wave1[i].transform.position;
            yield return new WaitForSeconds(0.65f);
        }
    }

    IEnumerator Wave2()
    {
        isWaveStart = true;
        enemiesremain = wave2.Length;
        yield return new WaitForSeconds(1f);

        for (int i = 0; i < wave2.Length; i++)
        {
            GameObject enemy = Instantiate(enemies[0]);
            enemy.transform.position = wave2[i].transform.position;
            yield return new WaitForSeconds(0.45f);
        }
    }

    IEnumerator Wave3()
    {
        isWaveStart = true;
        enemiesremain = wave3.Length;
        yield return new WaitForSeconds(1f);
        GameObject enemy = Instantiate(enemies[0]);
        enemy.transform.position = wave3[0].transform.position;
        yield return new WaitForSeconds(0.55f);
        GameObject enemy1 = Instantiate(enemies[1]);
        enemy1.transform.position = wave3[1].transform.position;
        yield return new WaitForSeconds(0.45f);
        for (int i = 2; i < 4; i++)
        {
            GameObject enemy2 = Instantiate(enemies[0]);
            enemy2.transform.position = wave3[i].transform.position;
        }
    }

    IEnumerator Wave4()
    {
        isWaveStart = true;
        enemiesremain = wave4.Length;
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < 2; i++)
        {
            GameObject enemy = Instantiate(enemies[0]);
            GameObject enemy1 = Instantiate(enemies[0]);
            enemy.transform.position = wave4[i].transform.position;
            enemy1.transform.position = wave4[i + 2].transform.position;
            yield return new WaitForSeconds(0.65f);
        }
        for (int i = 4; i < 6; i++)
        {
            GameObject enemy2 = Instantiate(enemies[1]);
            enemy2.transform.position = wave4[i].transform.position;
            yield return new WaitForSeconds(0.65f);
        }

    }

    IEnumerator Wave5()
    {
        isWaveStart = true;
        enemiesremain = wave5.Length;
        yield return new WaitForSeconds(1f);
        GameObject enemy2 = Instantiate(enemies[1]);
        yield return new WaitForSeconds(0.5f);
        for (int i = 1; i < 4; i++)
        {
            GameObject enemy = Instantiate(enemies[0]);
            enemy.transform.position = wave5[i].transform.position;
            yield return new WaitForSeconds(0.25f);
        }
        yield return new WaitForSeconds(0.55f);
        for (int i = 4; i < wave5.Length; i++)
        {
            GameObject enemy1 = Instantiate(enemies[1]);
            enemy1.transform.position = wave5[i].transform.position;
            yield return new WaitForSeconds(0.50f);
        }
    }

    IEnumerator Wave6()
    {
        isWaveStart = true;
        enemiesremain = wave6.Length;
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < wave6.Length; i++)
        {
            GameObject enemy1 = Instantiate(enemies[1]);
            enemy1.transform.position = wave6[i].transform.position;
            yield return new WaitForSeconds(1.37f);
        }
    }

    IEnumerator Wave7()
    {
        isWaveStart = true;
        enemiesremain = wave7.Length;
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < 2; i++)
        {
            GameObject enemy = Instantiate(enemies[1]);
            enemy.transform.position = wave7[i].transform.position;
            yield return new WaitForSeconds(0.5f);
        }
        yield return new WaitForSeconds(0.25f);
        GameObject enemy2 = Instantiate(enemies[2]);
        enemy2.transform.position = wave7[2].transform.position;
    }

    IEnumerator Wave8()
    {
        isWaveStart = true;
        enemiesremain = wave8.Length;
        yield return new WaitForSeconds(1f);

        for (int i = 0; i < 3; i++)
        {
            GameObject enemy = Instantiate(enemies[0]);
            enemy.transform.position = wave8[i].transform.position;
            yield return new WaitForSeconds(0.25f);
        }
        for (int i = 3; i < 5; i++)
        {
            GameObject enemy = Instantiate(enemies[1]);
            enemy.transform.position = wave8[i].transform.position;
            yield return new WaitForSeconds(1.17f);
        }
        GameObject enemy2 = Instantiate(enemies[2]);
        enemy2.transform.position = wave8[5].transform.position;
    }

    IEnumerator Wave9()
    {
        isWaveStart = true;
        enemiesremain = wave9.Length;
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < 2; i++)
        {
            GameObject enemy1 = Instantiate(enemies[1]);
            enemy1.transform.position = wave9[i].transform.position;
            yield return new WaitForSeconds(0.67f);
        }
        yield return new WaitForSeconds(0.45f);
        for (int i = 2; i < 4; i++)
        {
            GameObject enemy2 = Instantiate(enemies[2]);
            enemy2.transform.position = wave9[i].transform.position;
            yield return new WaitForSeconds(1.35f);
        }
    }

    IEnumerator Wave10()
    {
        isWaveStart = true;
        enemiesremain = wave10.Length;
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < wave10.Length; i++)
        {
            GameObject enemy2 = Instantiate(enemies[2]);
            enemy2.transform.position = wave10[i].transform.position;
            yield return new WaitForSeconds(1.87f);
        }
    }
}
