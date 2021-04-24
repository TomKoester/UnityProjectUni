using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashattack : MonoBehaviour
{
    private Queue<EnemyShooting> disabledEnemies = new Queue<EnemyShooting>();
    private int count;
    private AudioSource audioSource;

    public GameObject line;
    public AudioClip clipBolt;

    private void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        count = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            GameObject enemy = EnemyShooting.FindNearestEnemy(transform.position);
            EnemyShooting script = enemy.GetComponent<EnemyShooting>();
            script.enabled = false;
            disabledEnemies.Enqueue(script);
            Invoke("ActivateFirstEnemy", 6);

            audioSource.PlayOneShot(clipBolt);

            CreateLightning(enemy);
            CreateLightning(enemy);
            CreateLightning(enemy);
            CreateLightning(enemy);

            count--;
            if (count == 0) enabled = false;
        }
    }
    private void ActivateFirstEnemy()
    {
        EnemyShooting script = disabledEnemies.Dequeue();
        script.enabled = true;
    }

    void CreateLightning(GameObject enemy)
    {
        var lineSpawned = Instantiate(line, Vector3.zero, Quaternion.identity);
        Lightning lightning = lineSpawned.GetComponent<Lightning>();
        lightning.startPoint = transform;
        lightning.endPoint = enemy.transform;
        Destroy(lineSpawned, 2);
    }
}
