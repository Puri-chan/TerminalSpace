using UnityEngine;

public class EnemyProjectileB : MonoBehaviour
{
    public float projectilespeed;
    public AudioSource soundeffect;
    bool isHit = false;
    private Rigidbody2D rb;
    private GameObject player;
    Vector2 target;
    private ScreenStart speedmultiply;
    void Start()
    {
        soundeffect.pitch = Random.Range(0.8f, 1.2f);
        soundeffect.Play();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        target = player.transform.position - transform.position;
        speedmultiply = GameObject.FindGameObjectWithTag("Scoremanage").GetComponent<ScreenStart>();
        if (speedmultiply.multiply > 0)
        {
            projectilespeed = projectilespeed + (projectilespeed * 7.5f * speedmultiply.multiply / 100);
        }
    }
    private void Update()
    {
        enemyShootingB();
    }
    public void enemyShootingB()
    {
        if(isHit != true)
        {
            rb.linearVelocity = new Vector2(target.x, target.y).normalized * projectilespeed;
            if (transform.position.y < -9)
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
