using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    [SerializeField]
    private int SPEED = 10;

    private bool isMoving = false;
    private Rigidbody2D rgbg2D;
    public int bounceMax = 2;
    private int bounceCount = 0;
    private Vector2 deathPos;
    private Vector3 velocity;
    public GameObject dieAnim;

    public GameObject bounceEffect;
	

    
    public void initialize(Vector3 pForwardSource)
    {
        float lVelocityX = pForwardSource.x * SPEED;
        float lVelocityY = pForwardSource.y * SPEED;
        velocity = new Vector3(lVelocityX, lVelocityY);
        GetComponent<Rigidbody2D>().velocity = velocity;
    }
    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        bounceCount++;
        if (bounceCount == bounceMax)
        {
            deathPos = transform.position;
            Destroy(this.gameObject);
        } else
        {
            Debug.Log(collision.collider.tag);
            Vector3 norm = collision.contacts[0].normal;
            if (collision.collider.tag == ("Wall"))
            {
                Instantiate(bounceEffect, collision.contacts[0].point, Quaternion.Euler(0, 0, Mathf.Atan2(norm.y, norm.x) * Mathf.Rad2Deg + 90));
            }
            
            initialize(Vector2.Reflect(velocity.normalized, norm));
        }
    }

    private void OnDestroy()
    {
        if (dieAnim != null)
        {
            Instantiate(dieAnim, deathPos, Quaternion.identity);
        }
    }
}
