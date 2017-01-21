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
    private Animator _anim;

    public string horizontalAxisName = "Horizontal";
    public string verticalAxisName = "Vertical";


	// Use this for initialization
	void Start () {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
	}
	
    void Update()
    {
        move();
    }

    private void move()
    {
        Vector2 axis = new Vector2(Input.GetAxis(horizontalAxisName), Input.GetAxis(verticalAxisName));
        _vel += Time.deltaTime * axis * speed;
        if (_vel.x <= -0.2 || _vel.x >= 0.2 || _vel.y <= -0.2 || _vel.y >= 0.2) _anim.SetBool("isMoving", true);
        else _anim.SetBool("isMoving", false);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet") death();
    }

    private void death()
    {
        _anim.SetBool("isDead", true);
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void FixedUpdate () {
        _rb.velocity = _vel;
        _vel *= Mathf.Pow(dec, Time.fixedDeltaTime);
	}
}
