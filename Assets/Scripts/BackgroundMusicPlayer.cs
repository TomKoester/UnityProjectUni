using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(AudioSource))]
public class BackgroundMusicPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string[] filepaths = Directory.GetFiles(Path.Combine("Assets", "Sounds", "Resources"), "*.wav");
        string randomPath = filepaths[Random.Range(0, filepaths.Length)];
        string aduioClipName = Path.GetFileNameWithoutExtension(randomPath);

        AudioSource audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.clip = Resources.Load<AudioClip>(aduioClipName);
        audioSource.Play();
    }
}
