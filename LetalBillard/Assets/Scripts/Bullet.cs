using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    [SerializeField]
    private int SPEED = 10;
    [SerializeField]
    private float decayTime = 2.0f;
    [SerializeField]
    private float decelerateTime = 1.0f;
    [SerializeField]
    private float decelerationRate = 0.8f;
    [SerializeField]
    private float hitTimePenalty = 0.3f;

    private bool isMoving = false;
    private Rigidbody2D rgbg2D;
    public int bounceMax = 2;
    private int bounceCount = 0;
    private Vector2 deathPos;
    private Vector3 velocity;
    private bool isDecelerating = false;
    public GameObject dieAnim;
    private Rigidbody2D _rgbg2D;
    public GameObject bounceEffect;
    public int index;

    public WallParticleController.ParticleSetup[] particleByPlayer;

    private void Start()
    {
        StartCoroutine(decay(decayTime));
        StartCoroutine(decelerate(decelerateTime));
        _rgbg2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (isDecelerating)
        {
                //velocity.x *= decelerationRate;
                //velocity.y *= decelerationRate;
            _rgbg2D.velocity = velocity;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="pForwardSource"></param>
    public void initialize(Vector3 pForwardSource, int pIndex)
    {
        float lVelocityX = pForwardSource.x * SPEED;
        float lVelocityY = pForwardSource.y * SPEED;
        velocity = new Vector3(lVelocityX, lVelocityY);
        GetComponent<Rigidbody2D>().velocity = velocity;
        index = pIndex;
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
            
            Vector3 norm = collision.contacts[0].normal;
            if (collision.collider.tag == ("Wall"))
            {
                Quaternion rot = Quaternion.Euler(0, 0, Mathf.Atan2(norm.y, norm.x) * Mathf.Rad2Deg + 90);
                GameObject go = Instantiate(bounceEffect, collision.contacts[0].point, rot);
                go.GetComponent<WallParticleController>().setup = particleByPlayer[index-1];
                decayTime -= hitTimePenalty;
                //Debug.LogError("HIT");
                //AudioManager.instance.Play(Resources.Load<AudioClip>("Audio/rebond"));
            }
            initialize(Vector2.Reflect(velocity.normalized, norm), index);
        }
    }

    private void OnDestroy()
    {
        if (dieAnim != null)
        {
            Instantiate(dieAnim, deathPos, Quaternion.identity);
        }
    }

    IEnumerator decay(float time)
    {
        float timer = 0.0f;
        while(timer < decayTime)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        //yield return new WaitForSeconds(time);
        Destroy(this.gameObject);
    }

    IEnumerator decelerate(float time)
    {
        yield return new WaitForSeconds(time);
        isDecelerating = true;
    }
}
