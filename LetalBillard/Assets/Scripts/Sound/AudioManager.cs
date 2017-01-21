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
        source.clip = clip;
        source.Play();
    }
}
