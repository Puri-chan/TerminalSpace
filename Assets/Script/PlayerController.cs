using Pathfinding;
using System.Collections;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public AILerp playermovement;
    int shooting = 0;
    public Transform rifleleft, rifleright;
    public bool openprojectile = false, isshooting = false;
    public float cooldownspeed;
    public GameObject projectile, objparticle, objparticle2;
    public ParticleSystem shooteffectL, shooteffectR, explosion, explosion2;
    public bool GameStart;
    public bool isDead = false, isMoving = false;
    public int Damage;
    //DeathEffect
    public SpriteRenderer sprite;
    public AudioSource deadsound;
    public BoxCollider2D colliderplayer;
    private void Update()
    {
        touchmovement();
    }
    private void touchmovement()
    {
        //Mobile
        if (Input.touchCount == 1 && GameStart == true && isDead == false)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began && isshooting == false)
            {
                openprojectile = true;
                isshooting = true;
            }
            if(touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
            {
                playermovement.canMove = true;
                isMoving = true;
                this.transform.position = Camera.main.ScreenToWorldPoint(touch.position);
                if (openprojectile)
                {
                    StartCoroutine(shootprojectile(cooldownspeed));
                    openprojectile = false;
                }
            }
            else if(touch.phase == TouchPhase.Ended)
            {
                playermovement.canMove = false;
                isMoving = false;
                openprojectile = false;
                isshooting = false;
            }
        }
        //PC
        if (GameStart == true && isDead == false)
        {
            if (Input.GetMouseButtonDown(0) && isshooting == false)
            {
                openprojectile = true;
                isshooting = true;
            }
            if (Input.GetMouseButton(0))
            {
                Vector2 mouseposition = Input.mousePosition;
                playermovement.canMove = true;
                isMoving = true;
                this.transform.position = Camera.main.ScreenToWorldPoint(mouseposition);
                if (openprojectile)
                {
                    StartCoroutine(shootprojectile(cooldownspeed));
                    openprojectile = false;
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                playermovement.canMove = false;
                isMoving = false;
                openprojectile = false;
                isshooting = false;
            }
        }

    }
    IEnumerator shootprojectile(float cooldown)
    {
        while (isMoving == true)
        {
            GameObject instantiate = Instantiate(projectile);
            if (shooting % 2 == 0) 
            {
                instantiate.transform.position = rifleleft.transform.position;
                shooteffectL.Play();
            }    
            else if(shooting % 2 != 0) 
            {
                instantiate.transform.position = rifleright.transform.position;
                shooteffectR.Play();
            }
            shooting++;
            yield return new WaitForSeconds(cooldown);
        }
    }
}
