using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletSpawn : MonoBehaviour {
    public GameObject bullet;

    private GameObject instance;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("space")) {
            instance = Instantiate(bullet, new Vector3(8, 0, 0), Quaternion.identity) as GameObject;
            instance.GetComponent<Bullet>().initialize(135);
        }
	}
}
