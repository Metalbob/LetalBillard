using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    [SerializeField]
    private int SPEED = 10;
    [SerializeField]
    private float decayTime = 1.0f;
    [SerializeField]
    private int decelerateTime = 90;
    [SerializeField]
    private float decelerateRate = 0.5f;

    private bool isMoving = false;
    private Rigidbody2D rgbg2D;
    public int bounceMax = 2;
    private int bounceCount = 0;
    private Vector2 deathPos;
    private Vector3 velocity;
    public GameObject dieAnim;
    private int frameCount = 0;
    private bool decelerate = false;
	

    
    public void initialize(Vector3 pForwardSource)
    {
        float lVelocityX = pForwardSource.x * SPEED;
        float lVelocityY = pForwardSource.y * SPEED;
        velocity = new Vector3(lVelocityX, lVelocityY);
        GetComponent<Rigidbody2D>().velocity = velocity;
    }

    private void Awake()
    {
        StartCoroutine(decay(decayTime));

    }

    private void Update()
    {

        frameCount++;
        if (frameCount % decelerateTime == 0)
            decelerate = true;
        if (decelerate)
        {
            velocity.x -= decelerateRate;
            velocity.y -= decelerateRate;
        }
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
            initialize(Vector2.Reflect(velocity.normalized, collision.contacts[0].normal));
        }
    }
    IEnumerator decay(float time)
    {
        yield return new WaitForSeconds(time);
        deathPos = transform.position;
        Destroy(this.gameObject);
    }
    private void OnDestroy()
    {
        if (dieAnim != null)
        {
            Instantiate(dieAnim, deathPos, Quaternion.identity);
        }
    }
}
