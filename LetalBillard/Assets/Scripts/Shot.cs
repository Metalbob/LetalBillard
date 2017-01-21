using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour {

    
    public int fireRate = 30;

    private Vector2 axis;
    public GameObject bulletPrefab;
    private Animator _anim;
    private int frameCount = 0;
    private PlayerInput _input;
    private bool canShoot = true;

    // Use this for initialization
    void Start () {
        _anim = transform.parent.gameObject.GetComponent<Animator>();
        _input = GetComponentInParent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameState.Instance.CurState == GameState.State.RoundInProgress ||
            GameState.Instance.CurState == GameState.State.StartRound ||
            GameState.Instance.CurState == GameState.State.EndRound)
        {
            frameCount++;
            

            Vector2 inputAxis = _input.aimAxis;

            if (inputAxis.magnitude > 0.4f)
            {
                axis = inputAxis;
            }
            transform.parent.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(axis.y, axis.x) * Mathf.Rad2Deg + 64);
//<<<<<<< .mine
//            if (_input.fire > 0.2 && canShoot) // Todo: Inpractical in case you press a the wrong time you to have wait a whole fireFrame before firering. Also, don't count frame, count second.






//=======
        }

        if (GameState.Instance.CurState == GameState.State.RoundInProgress)
        {
            GameObject bullet;

            if (_input.fire > 0.2 && frameCount % fireRate == 0) // Todo: Inpractical in case you press a the wrong time you to have wait a whole fireFrame before firering. Also, don't count frame, count second.
//>>>>>>> .theirs
            {
                canShoot = false;
                _anim.SetBool("isShooting", true);
                bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                bullet.GetComponent<Bullet>().initialize(transform.right, _input.playerIndex);
                Debug.LogError("SHOOOOOT");
                AudioManager.instance.Play(Resources.Load<AudioClip>("Audio/shot"));
            }
            else if (frameCount % fireRate == 0) canShoot = true;

            else _anim.SetBool("isShooting", false);
        }
    }
}
