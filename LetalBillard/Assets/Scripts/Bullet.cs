using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    [SerializeField]
    private int SPEED = 10;

    private bool isMoving = false;
    private Rigidbody2D rgbg2D;
    [SerializeField]
    private int bounceCount = 0;
    private Transform deathPos;
    private Vector3 velocity;
    public GameObject dieAnim;
	
    
    public void initialize(Vector3 pForwardSource)
    {
        float lVelocityX = pForwardSource.x * SPEED;
        float lVelocityY = pForwardSource.y * SPEED;
        velocity = new Vector3(lVelocityX, lVelocityY);
    }
    
    // Use this for initialization
	void Start () {
        rgbg2D = GetComponent<Rigidbody2D>();
        isMoving = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (isMoving)
        {
            rgbg2D.velocity = velocity;
            isMoving = false;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        bounceCount++;
        if (bounceCount == 2)
        {
            deathPos = transform;
            Destroy(this.gameObject);
        }
    }

    private void OnDestroy()
    {
        Debug.Log("y");
        Instantiate(dieAnim, new Vector3(deathPos.position.x, deathPos.position.y, 0), Quaternion.identity);
    }
}
