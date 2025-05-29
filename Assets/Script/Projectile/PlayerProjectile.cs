using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    public float projectilespeed;
    public AudioSource soundeffect;
    bool isHit =false;

    private void Start()
    {
        soundeffect.pitch = Random.Range(0.8f, 1.2f);
        soundeffect.Play();

    }
    public void Update()
    {
        if(isHit != true)
        {
            transform.position += transform.up * projectilespeed * Time.deltaTime;
            if (transform.position.y > 5)
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
        if (collision.tag == "Enemy")
        {
            isHit = true;
        }
    }
}
