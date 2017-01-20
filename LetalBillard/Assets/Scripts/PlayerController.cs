using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour {

    public int playerIndex = 0;
    public float speed = 10;
    public float dec = 0.01f;

    private Rigidbody2D _rb;
    private Vector2 _vel;

    public string horizontalAxisName = "Horizontal";
    public string verticalAxisName = "Vertical";


	// Use this for initialization
	void Start () {
        _rb = GetComponent<Rigidbody2D>();
	}
	
    void Update()
    {
        Vector2 axis = new Vector2(Input.GetAxis(horizontalAxisName), Input.GetAxis(verticalAxisName));
        _vel += Time.deltaTime * axis * speed;
    }

	// Update is called once per frame
	void FixedUpdate () {
        _rb.velocity = _vel;
        _vel *= Mathf.Pow(dec, Time.fixedDeltaTime);
	}
}
