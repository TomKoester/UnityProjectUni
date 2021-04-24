using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public bool isFinish = false;
    public AudioClip[] clipReachedGoal;

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player") && GameManager.Instance.Transition(isFinish ? Trigger.FinishedGame : Trigger.ReachedGoal))
        {
            collider.gameObject.GetComponent<AudioSource>().PlayRandom(clipReachedGoal);
        }
    }
}
