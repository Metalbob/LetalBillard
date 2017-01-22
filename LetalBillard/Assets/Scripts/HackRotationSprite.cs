using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackRotationSprite : MonoBehaviour
{
    private Transform _parent;

    [SerializeField]
    private Vector3 vec3;

    void Start()
    {
        _parent = gameObject.transform.parent;
        gameObject.transform.parent = null;
    }
	void Update ()
    {
        transform.position = _parent.transform.transform.position + vec3;
	}
}
