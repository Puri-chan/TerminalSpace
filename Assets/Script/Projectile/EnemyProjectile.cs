using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class EnemyProjectile : MonoBehaviour
{
    public float projectilespeed;
    public AudioSource soundeffect;
    bool isHit = false;
    private ScreenStart speedmultiply;

    void Start()
    {
        soundeffect.pitch = Random.Range(0.8f, 1.2f);
        soundeffect.Play();
        speedmultiply = GameObject.FindGameObjectWithTag("Scoremanage").GetComponent<ScreenStart>();
        if (speedmultiply.multiply > 0)
        {
            projectilespeed = projectilespeed + (projectilespeed * 7.5f * speedmultiply.multiply / 100);
        }
    }
    private void Update()
    {
        enemyShootingA();
    }
    public void enemyShootingA()
    {
        if(isHit != true)
        {
            transform.position += transform.up * -projectilespeed * Time.deltaTime;
            if (transform.position.y < -9)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isHit = true;
        }
    }
}
