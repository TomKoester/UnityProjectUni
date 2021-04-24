using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Gamestate
{
    ONGOING,
    WON,
    LOST,
    FINAL
}

public enum Trigger
{
    Death, ReachedGoal, FinishedGame
}

public class GameManager : MonoBehaviour
{
    private MooreMachine<Gamestate, Trigger> sm;

    public GameObject wonUI;
    public GameObject deathUI;
    
    
    public static GameManager Instance { get; private set; }
    // Start is called before the first frame update
    void Awake()
    {
        
        Instance = this;
        sm = MooreMachine<Gamestate, Trigger>.Create(Gamestate.ONGOING)
            .Configure(Gamestate.ONGOING)
            .CanTransitionOn(Trigger.Death, Gamestate.LOST)
            .CanTransitionOn(Trigger.ReachedGoal, Gamestate.WON)
            .CanTransitionOn(Trigger.FinishedGame, Gamestate.FINAL)

            .Configure(Gamestate.LOST)
            .Do(() =>
            {
                GameObject player = GameObject.Find("Player");
                Animator animator = player.GetComponent<Animator>(); 
                animator.SetBool("RUN", false);
                animator.SetBool("JUMP", false);
                animator.SetBool("DANCE", false);
                animator.SetBool("DIE", true);
                player.GetComponent<Playermovement>().isAlive = false;
                deathUI.SetActive(true);
                Invoke("Restart", 4.0f);
            })

            .Configure(Gamestate.WON)
            .Do(() =>
            {
                wonUI.SetActive(true);
                Invoke("StartNextLevel", 2.0f);
            })

            .Configure(Gamestate.FINAL)
            .Do(() =>
            {
                wonUI.SetActive(true);
                Invoke("FinishedGame", 2);
            })
            .Build();
    }

    public bool Transition(Trigger trigger) => sm.Transition(trigger);

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void StartNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    void FinishedGame()
    {
        SceneManager.LoadScene(2);
    }

}
