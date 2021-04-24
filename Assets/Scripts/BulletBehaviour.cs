using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Vector3 _direction;

    public Transform _player;
    public float _bulletVelocity;
    public AudioClip _clipReflected;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _direction = transform.position - (GameObject.Find("Player").transform.position + Vector3.up * 3.5f);
        _direction.Normalize();
        _rigidbody.velocity = -_direction * _bulletVelocity;
        _player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(_player.position + Vector3.up * 3.5f, transform.position);

        if (Input.GetKeyDown(KeyCode.R) && distance < 9)
        {
            _rigidbody.velocity = _direction * _bulletVelocity;
            _player.GetComponent<AudioSource>().PlayOneShot(_clipReflected);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            HealthbarManager.Instance.TakeDamage(2, DamageSource.Bullet);
        }
        Destroy(gameObject);
    }
}
