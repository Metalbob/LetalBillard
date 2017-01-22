using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillCam : MonoBehaviour {

    [HideInInspector]
    public float originalSize;
    [HideInInspector]
    public Vector3 originalPos;
    [HideInInspector]
    public Transform cam;
    [HideInInspector]
    public static Transform target;
    public float targetScale = 2;
    public float lerpForce;
    public float scaleLerpForce;
    private Camera main;

    public static KillCam instance;

	// Use this for initialization
	void Start ()
    {
        main = Camera.main;
        cam = main.transform;
        originalSize = main.orthographicSize;
        originalPos = cam.position;
        instance = this;
    }

    public static void Reset()
    {
        instance.cam.position = instance.originalPos;
        instance.main.orthographicSize = instance.originalSize;
        target = null;
    }
	
	// Update is called once per frame
	void Update () {
		if (target != null)
        {
            var pt = Vector3.Lerp(cam.position, target.position, Time.deltaTime * lerpForce);
            pt.z = originalPos.z;
            cam.position = pt;
            main.orthographicSize = Mathf.Lerp(main.orthographicSize, targetScale, Time.deltaTime * scaleLerpForce);
        }
	}
}
