using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kaktus : MonoBehaviour
{
    private float timer;

    public float kaktusDmgCooldown = 1.0f;
    public int kaktusDmg = 1;

    void OnTriggerStay(Collider collision)
    {
        if (Time.fixedTime > timer)
        {
            HealthbarManager.Instance.TakeDamage(kaktusDmg, DamageSource.Kaktus);
            timer = Time.fixedTime + kaktusDmgCooldown;
        }
    }
}
