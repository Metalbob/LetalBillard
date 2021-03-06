﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour {

    public float deadzone = 0.2f;
    public float fireRate = 1.0f;

    private Vector2 axis;
    public GameObject bulletPrefab;
    private Animator _anim;
    private float cooldown = 1.0f;
    private PlayerInput _input;
    private bool scheduledShot = false;
    private bool prevFireInput = false;


    // Use this for initialization
    void Start () {
        _anim = transform.parent.gameObject.GetComponent<Animator>();
        _input = GetComponentInParent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        cooldown -= Time.deltaTime;
        if ((GameState.Instance.CurState == GameState.State.RoundInProgress ||
            GameState.Instance.CurState == GameState.State.StartRound ||
            GameState.Instance.CurState == GameState.State.EndRound) &&
            !GetComponentInParent<PlayerController>()._isDead)
        {
                       

            Vector2 inputAxis = _input.aimAxis;

            if (inputAxis.magnitude > deadzone)
            {
                axis = inputAxis;
            }
            transform.parent.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(axis.y, axis.x) * Mathf.Rad2Deg + 64);

        }

        if (GameState.Instance.CurState == GameState.State.RoundInProgress &&
            !GetComponentInParent<PlayerController>()._isDead)
        {
            GameObject bullet;

            if (_input.fire > 0.2) // Todo: Inpractical in case you press a the wrong time you to have wait a whole fireFrame before firering. Also, don't count frame, count second.
            {
               // prevFireInput = true;
                scheduledShot = true;

                if (cooldown <= 0.0f && scheduledShot)
                {
                    bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                    bullet.GetComponent<Bullet>().initialize(transform.right, _input.playerIndex);
                    bullet.GetComponent<SpriteRenderer>().color = _input.color;
                    cooldown = fireRate;
                    _anim.SetBool("isShooting", true);
                    AudioManager.instance.Play(Resources.Load<AudioClip>("Audio/shot"));
                    
                    scheduledShot = false;
                }
                else _anim.SetBool("isShooting", false);
            }
            


        }
    }
}