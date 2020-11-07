using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListenSound : MonoBehaviour
{
    public AudioClip Sound;
    AudioSource audioSource;

    private void Start()
    {
        audioSource = GameObject.Find("QuizManager").GetComponent<AudioSource>();
    }

    public void PlaySound()
    {
        audioSource.Stop();
        //GameObject.FindGameObjectWithTag("Music").GetComponent<MusicScript>().StopMusic();
        audioSource.PlayOneShot(Sound);
        StartCoroutine(WaitForSound());
    }

    IEnumerator WaitForSound()
    {
        yield return new WaitUntil(() => audioSource.isPlaying == false);
        //GameObject.FindGameObjectWithTag("Music").GetComponent<MusicScript>().PlayMusic();
    }
}
