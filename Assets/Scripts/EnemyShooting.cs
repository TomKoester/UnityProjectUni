using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    private static List<GameObject> enemies = new List<GameObject>();
    private float timer;

    public float bulletCooldown;
    public float bulletVelocity;
    public GameObject bullet;

    // Start is called before the first frame update
    void Start()
    {
        timer = Time.fixedTime + bulletCooldown;
    }

    private void OnEnable()
    {
        enemies.Add(gameObject);
    }

    void OnDisable()
    {
        enemies.Remove(gameObject);
    }

    public static GameObject FindNearestEnemy(Vector3 player)
    {
        GameObject nearestEnemy = null;
        float minDist = float.MaxValue;

        foreach (GameObject enemy in enemies)
        {
            float d = Vector3.Distance(enemy.transform.position, player);

            if (d < minDist)
            {
                nearestEnemy = enemy;
                minDist = d;
            }
        }
        return nearestEnemy;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.fixedTime > timer)
        {
            SpawnBullet();
            timer = Time.fixedTime + bulletCooldown;
        }
    }

    private void SpawnBullet()
    {
        var bulletSpawned = Instantiate(bullet, transform.position, Quaternion.identity);
        Destroy(bulletSpawned, 2);
    }
}
