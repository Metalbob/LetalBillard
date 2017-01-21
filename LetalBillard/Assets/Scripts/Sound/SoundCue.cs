using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundCue : MonoBehaviour {

    private List<AudioSource> sources;

	// Use this for initialization
	void Start ()
    {
        sources = new List<AudioSource>(GetComponents<AudioSource>());
        
	}

    public void Play()
    {
        if (sources.Count == 0)
        {
            Debug.LogWarning("No sources");
            return;
        }

        sources[Random.Range(0, sources.Count)].Play();
    }
}
