using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DamageSource
{
    Bullet, Kaktus
}

public class HealthbarManager : MonoBehaviour
{
    private const int maxLife = 10;
    private float fullWidth;
    private float damageUITimer = float.PositiveInfinity;
    private int currentLife;
    private GameObject player;
    



    public RectTransform greenBar;
    public GameObject damageUI;
    public AudioClip clipDamageTaken;
    public AudioClip[] clipBulletDeath;

    public static HealthbarManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        fullWidth = greenBar.rect.width;
        currentLife = maxLife;
        player = GameObject.Find("Player");
    }

    public void TakeDamage(int damage, DamageSource source)
    {
        currentLife -= damage;
        
        if (currentLife <= 0)
        {
            currentLife = 0;
            GameManager.Instance.Transition(Trigger.Death);

            if (source == DamageSource.Bullet)
            {
                player.GetComponent<AudioSource>().PlayRandom(clipBulletDeath);
            }
        }
        damageUI.SetActive(true);
        
        damageUITimer = Time.fixedTime + 0.5f;
        greenBar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, (fullWidth / maxLife) * currentLife);
        player.GetComponent<AudioSource>().PlayOneShot(clipDamageTaken);
    }

    public void Update()
    {
        if (Time.fixedTime > damageUITimer)
        {
            damageUI.SetActive(false);
            damageUITimer = float.MaxValue;
        }


        
            
        

    }
}

