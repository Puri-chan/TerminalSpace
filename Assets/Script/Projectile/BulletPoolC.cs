using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class BulletPoolC : MonoBehaviour
{
    public static BulletPoolC instance;
    [SerializeField] private GameObject bulletInstantiant;
    private bool notEnough;
    private List<GameObject> bullets;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        bullets = new List<GameObject>();
    }

    public GameObject GetBullet()
    {
        if (bullets.Count == 0)
        {
            for (int i = 0; i < bullets.Count; i++)
            {
                if (!bullets[i].activeInHierarchy)
                {
                    return bullets[i];
                }
            }
        }

        if(notEnough)
        {
            GameObject bullet = Instantiate(bulletInstantiant);
            bullet.SetActive(false);
            bullets.Add(bullet);
            return bullet;
        }

        return null;
    }
}
