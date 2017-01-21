using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour {

    public string horizontalAxisName = "Horizontal2";
    public string verticalAxisName = "Vertical2";
    private Vector2 axis;
    public GameObject bulletPrefab;
    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update() {
        GameObject bullet;
        if (Input.GetAxis(horizontalAxisName) != 0 || Input.GetAxis(horizontalAxisName) != 0) axis = new Vector2(Input.GetAxis(horizontalAxisName), Input.GetAxis(verticalAxisName));
        transform.parent.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(axis.y, axis.x) * Mathf.Rad2Deg);
        if (Input.GetButtonDown("Fire1"))
        {
            bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.GetComponent<Bullet>().initialize(transform.right);
        }

    }
}
