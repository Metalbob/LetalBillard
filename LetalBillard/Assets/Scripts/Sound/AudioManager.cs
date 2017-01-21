using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public static AudioManager instance { get; private set; }
    private AudioSource source;
	// Use this for initialization
	void Start ()
    {
        instance = this;
        DontDestroyOnLoad(this);
        source = GetComponent<AudioSource>();
	}
	
	public void Play(AudioClip clip)
    {
        //source.clip = clip;
        //source.PlayOneShot(clip);
        StartCoroutine(PlaySolo(clip));
    }

    IEnumerator PlaySolo(AudioClip clip)
    {
        AudioSource tempSource = gameObject.AddComponent<AudioSource>();
        tempSource.clip = clip;
        tempSource.Play();

        while (tempSource.isPlaying)
            yield return null;

        Destroy(tempSource);
    }
}
