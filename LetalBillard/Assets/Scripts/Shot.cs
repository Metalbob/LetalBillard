using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour {

    public string horizontalAxisName = "Horizontal2";
    public string verticalAxisName = "Vertical2";
    private Vector2 axis;
    public GameObject bulletPrefab;
    private Animator _anim;
    // Use this for initialization
    void Start () {
        _anim = transform.parent.gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        GameObject bullet;
        Debug.Log(Input.GetAxis(horizontalAxisName)+", "+ Input.GetAxis(verticalAxisName));
        if (Input.GetAxis(horizontalAxisName) < -0.4 || Input.GetAxis(horizontalAxisName) > 0.4 || Input.GetAxis(verticalAxisName) < -0.4 || Input.GetAxis(verticalAxisName) > 0.4) axis = new Vector2(Input.GetAxis(horizontalAxisName), Input.GetAxis(verticalAxisName));
        transform.parent.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(axis.y, axis.x) * Mathf.Rad2Deg + 64);
        if (Input.GetButtonDown("Fire1"))
        {
             _anim.SetBool("isShooting", true);
            bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.GetComponent<Bullet>().initialize(transform.right);
        }
        else _anim.SetBool("isShooting", false);

    }
}
