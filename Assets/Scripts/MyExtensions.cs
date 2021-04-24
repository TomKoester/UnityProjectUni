using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class MyExtensions
{
    public static void PlayRandom(this AudioSource audioSource, IList<AudioClip> clips)
    {
        int i = Random.Range(0, clips.Count);
        audioSource.PlayOneShot(clips[i]);
    }
}