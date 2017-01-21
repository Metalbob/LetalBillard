using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour {

    public string horizontalAxisNameP1 = "Horizontal2_Player1";
    public string verticalAxisNameP1 = "Vertical2_Player1";
    public string horizontalAxisNameP2 = "Horizontal2_Player2";
    public string verticalAxisNameP2 = "Vertical2_Player2";
    public string fireP1 = "Fire1_Player1";
    public string fireP2 = "Fire1_Player2";
    public int fireRate = 30;

    private Vector2 axis;
    public GameObject bulletPrefab;
    private Animator _anim;
    private PlayerController _ctrl;
    private int frameCount = 0;

    // Use this for initialization
    void Start () {
        _anim = transform.parent.gameObject.GetComponent<Animator>();
        _ctrl = transform.parent.gameObject.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameState.Instance.CurState == GameState.State.RoundInProgress)
        {
            frameCount++;
            GameObject bullet;
            string horizontalAxisName;
            string verticalAxisName;
            string fire;
            float atk;

            if (_ctrl.playerIndex == 1)
            {
                horizontalAxisName = horizontalAxisNameP1;
                verticalAxisName = verticalAxisNameP1;
                fire = fireP1;
            }
            else
            {
                horizontalAxisName = horizontalAxisNameP2;
                verticalAxisName = verticalAxisNameP2;
                fire = fireP2;
            }

            if (Input.GetAxis(horizontalAxisName) < -0.4 || Input.GetAxis(horizontalAxisName) > 0.4 || Input.GetAxis(verticalAxisName) < -0.4 || Input.GetAxis(verticalAxisName) > 0.4) axis = new Vector2(Input.GetAxis(horizontalAxisName), Input.GetAxis(verticalAxisName));
            transform.parent.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(axis.y, axis.x) * Mathf.Rad2Deg + 64);
            atk = Input.GetAxis(fire);
            if (atk > 0.2 && frameCount % fireRate == 0)
            {
                _anim.SetBool("isShooting", true);
                bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                bullet.GetComponent<Bullet>().initialize(transform.right);
            }
            else _anim.SetBool("isShooting", false);
    }
    }
}
