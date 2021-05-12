using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListenSound : MonoBehaviour
{
    public AudioClip Sound;
    AudioSource audioSource;

    // Actually this script is used by Quiz Manager for Input Quiz with sound 
    private void Start()
    {
        audioSource = GameObject.Find("QuizManager").GetComponent<AudioSource>();
    }

    public void PlaySound()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(Sound);
        StartCoroutine(WaitForSound());
    }

    IEnumerator WaitForSound()
    {
        yield return new WaitUntil(() => audioSource.isPlaying == false);
    }
}
