using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
    public int targetBuildIndex;
    public void DoSwitch()
    {
        SceneManager.LoadScene(targetBuildIndex);
    }
}
