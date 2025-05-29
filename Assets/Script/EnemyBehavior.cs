using System.Collections;
using TMPro;
using UnityEngine;


public class EnemyBehavior : MonoBehaviour
{
    public float spawncooldown, shotcooldown;
    public GameObject projectile, particle;
    public int HP, Score, EnemyType, bulletpershot;
    public bool IsShooting { get; set; } = false;
    bool finishdead = false;
    public Transform location, location2;
    public ParticleSystem ShootEffect, ShootEffect2, deadEffect1, deadEffect2;
    public TextMeshProUGUI HPDisplay;
    public AudioSource damagesound, explosionsound;
    public SpriteRenderer sprite;
    public BoxCollider2D enemycollider;
    private ScreenStart score;
    private PlayerController playercontroller;
    private PlayerSetActive playeractive;
    int bulletswap = 0;
    void Start()
    {
        enemycollider.enabled = false;
        IsShooting = true;
        StartCoroutine(Invicible());
        if (EnemyType == 1)
        {
            StartCoroutine(spawnprojectileA(spawncooldown));
        }
        else if (EnemyType == 2)
        {
            StartCoroutine(spawnprojectileB(spawncooldown));
        }
        else if (EnemyType == 3)
        {
            StartCoroutine(spawnprojectileC(spawncooldown));
        }

        damagesound = GetComponent<AudioSource>();
        score = GameObject.FindGameObjectWithTag("Scoremanage").GetComponent<ScreenStart>();
        playeractive = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerSetActive>();
        playercontroller = GameObject.FindGameObjectWithTag("Playermanage").GetComponent<PlayerController>();
        if (score.multiply > 0)
        {
            HP = HP + (HP * 15 * score.multiply / 100);
            shotcooldown = shotcooldown - (shotcooldown * 7.67f * score.multiply / 100);
        }
    }

    // Update is called once per frame
    void Update()
    {
        HPDisplay.text = HP.ToString();
        if (HP <= 0 && finishdead == false)
        {
            explosionsound.pitch = Random.Range(0.4f, 1.6f);
            explosionsound.Play();
            sprite.enabled = false;
            IsShooting = false;
            enemycollider.enabled = false;
            HPDisplay.enabled = false;
            particle.SetActive(false);
            deadEffect1.Play(); deadEffect2.Play();
            finishdead = true;
            score.updatescore(Score);
            score.EnemiesRemaining();
            StartCoroutine(deleteobj());
        }

        if (playercontroller.isDead == true)
        {
            IsShooting=false;
        }

        if(playeractive.isRestart == true)
        {
            Restart();
        }
    }

    void Restart()
    {
        HP -= 10000;
        damagesound.pitch = Random.Range(0.4f, 1.6f);
        damagesound.Play();
    }
    //EnemyA
    IEnumerator spawnprojectileA(float cooldown)
    {
        yield return new WaitForSeconds(spawncooldown);
        while (IsShooting == true)
        {
            GameObject instantiate = Instantiate(projectile);
            instantiate.transform.position = location.transform.position;
            ShootEffect.Play();
            yield return new WaitForSeconds(shotcooldown);
        }
    }
    //EnemyB
    IEnumerator spawnprojectileB(float cooldown)
    {
        yield return new WaitForSeconds(spawncooldown);
        while (IsShooting == true)
        {
            GameObject instantiate = Instantiate(projectile);
            if (bulletswap % 2 == 0)
            {
                instantiate.transform.position = location.transform.position;
                ShootEffect.Play();
            }
            else if (bulletswap % 2 != 0)
            {
                instantiate.transform.position = location2.transform.position;
                ShootEffect2.Play();
            }
            bulletswap++;
            yield return new WaitForSeconds(shotcooldown);
        }
    }
    //EnemyC
    IEnumerator spawnprojectileC(float cooldown)
    {
        yield return new WaitForSeconds(spawncooldown);
        while (IsShooting == true)
        {
            GameObject instantiate = Instantiate(projectile);
            instantiate.transform.position = location.transform.position;
            ShootEffect.Play();
            yield return new WaitForSeconds(shotcooldown);
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("PlayerProjectile"))
        {
            damagesound.pitch = Random.Range(0.4f, 1.6f);
            damagesound.Play();
            HP -= playercontroller.Damage;
            StartCoroutine(effect());
        }
    }

    IEnumerator effect()
    {
        sprite.color = Color.red; HPDisplay.color = Color.red;
        yield return new WaitForSeconds(0.05f);
        sprite.color = Color.white; HPDisplay.color = Color.white;
    }
    IEnumerator deleteobj()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }

    IEnumerator Invicible()
    {
        yield return new WaitForSeconds(1f);
        enemycollider.enabled = true;
    }
}
