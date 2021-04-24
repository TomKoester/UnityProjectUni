using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public AudioClip clipFound;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            other.gameObject.GetComponent<Flashattack>().enabled = true;
            other.gameObject.GetComponent<AudioSource>().PlayOneShot(clipFound);
        }
    }
}
