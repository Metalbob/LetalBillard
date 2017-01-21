using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallParticleController : MonoBehaviour {

    [System.Serializable]
    public struct ParticleSetup
    {
        public Gradient trailColor;
        public Color colorOver;
        public float length;
    }

    public ParticleSetup setup;


	// Use this for initialization
	void Start () {
        OnChangeColors();

    }
	
    void OnValidate()
    {
        OnChangeColors();
    }

    void OnChangeColors()
    {
        var _particleSystems = GetComponentsInChildren<ParticleSystem>();

        foreach (var particleSystem in _particleSystems)
        {
            var trails = particleSystem.trails;
            trails.colorOverLifetime = setup.trailColor;
            trails.colorOverTrail = setup.colorOver;
            var main = particleSystem.main;
            main.startLifetime = setup.length;
        }
    }

	// Update is called once per frame
	void Update () {
	    
	}
}
