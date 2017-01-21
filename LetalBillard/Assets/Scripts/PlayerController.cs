using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour {

    public float speed = 10;
    public float dec = 0.01f;
    public float deadPoint = 0.2f;

    private Rigidbody2D _rb;
    private Vector2 _vel;
    private Animator _anim;
    private PlayerInput _input;

    [SerializeField]
    private float rumbleTime = 1.0f;
    [SerializeField]
    [Range(0, 1)]
    private float vibrationStrength = 1.0f;

    // Use this for initialization
    void Start () {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _input = GetComponentInParent<PlayerInput>();
    }
	
    void Update()
    {
        if (GameState.Instance.CurState == GameState.State.RoundInProgress ||
            GameState.Instance.CurState == GameState.State.StartRound ||
            GameState.Instance.CurState == GameState.State.EndRound)
        {
            move();
        }
    }

    private void move()
    {
        Vector2 axis;
        axis = _input.moveAxis;

        _vel += Time.deltaTime * axis * speed;
        if (_vel.magnitude  > deadPoint) _anim.SetBool("isMoving", true);
        else _anim.SetBool("isMoving", false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Bullet>() != null)
        {
            if (collision.gameObject.GetComponent<Bullet>().index != GetComponent<PlayerInput>().playerIndex)
                StartCoroutine(death(1.0f));
        }
    }

    IEnumerator death(float timeDead)
    {
        _anim.SetBool("isDead", true);
        AudioManager.instance.Play(Resources.Load<AudioClip>("Audio/dead"));
        Rumble(rumbleTime, vibrationStrength);
        yield return new WaitForSeconds(timeDead);
        _anim.SetBool("isDead", false);
        //GameState.Instance.respawn(this.gameObject);
        StartCoroutine(GameState.Instance.StopRound(_input.playerIndex));
    }

    public void Rumble(float time, float strength)
    {
        StartCoroutine(RumbleEnum(time, strength));
    }

    IEnumerator RumbleEnum(float time, float strength)
    {
        GamePadState state = GamePad.GetState((PlayerIndex)0);
        GamePad.SetVibration((PlayerIndex)(_input.playerIndex - 1), strength, strength);

        yield return new WaitForSeconds(time);
        GamePad.SetVibration((PlayerIndex)(_input.playerIndex - 1), 0, 0);
    }

    // Update is called once per frame
    void FixedUpdate ()
    {
        if (GameState.Instance.CurState == GameState.State.RoundInProgress ||
            GameState.Instance.CurState == GameState.State.StartRound ||
            GameState.Instance.CurState == GameState.State.EndRound)
        {
            _rb.velocity = _vel;
            _vel *= Mathf.Pow(dec, Time.fixedDeltaTime);
        }
        
	}

    public void StopVelocityPlayer()
    {
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        GetComponent<Rigidbody2D>().angularVelocity = 0;
        GetComponent<Rigidbody2D>().Sleep();
        _anim.SetBool("isMoving", false);
    }
}
