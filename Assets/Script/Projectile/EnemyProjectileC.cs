using UnityEngine;

public class EnemyProjectileC : MonoBehaviour
{
    public float projectilespeed;
    public AudioSource soundeffect;
    bool isHit = false;
    float direction;
    private ScreenStart speedmultiply;
    void Start()
    {
        direction = Random.Range(2.5f, -2.5f);
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
        enemyShootingC();
    }
    public void enemyShootingC()
    {
        if (isHit != true)
        {
            transform.position += transform.up * -projectilespeed * Time.deltaTime;
            transform.position += transform.right * direction * Time.deltaTime;

            if (transform.position.y < -9 || transform.position.x < -5 
                || transform.position.x > 5)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            Destroy (gameObject);
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
