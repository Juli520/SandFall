﻿using System;
using System.Collections;
using Photon.Pun;
using UnityEngine;

public class PlayerState : MonoBehaviourPun
{
    [Header("Movement")]
    public float speed = 5;
    public float jumpForce = 7;
    public float rotationSpeed = 5;
    [Header("Attack")] 
    public float attackRate = 3f;
    public float pushForce = 5f;
    public Collider hitBox;
    [Header("Death")]
    public float deathHeight = -2f;
    
    [SerializeField] private Camera _myCam;
    private Photon.Realtime.Player _myPlayer;
    
    private float _baseJumpForce;
    
    private void Awake()
    {
        if (!photonView.IsMine)
            Destroy(_myCam.gameObject);
        else
            photonView.RPC("SetOwn", RpcTarget.AllBuffered, PhotonNetwork.LocalPlayer);

        _baseJumpForce = jumpForce;
    }
    
    [PunRPC]
    private void SetOwn(Photon.Realtime.Player _myPlayer)
    {
        this._myPlayer = _myPlayer;
    }

    private void Update()
    {
        if(!photonView.IsMine) return;
        
        if (transform.position.y <= deathHeight)
            KillPlayer();
    }

    public void ApplyBuff(float multiplier, float duration)
    {
        if(!photonView.IsMine) return;
        
        StartCoroutine(Apply(multiplier, duration));
    }
    
    /// <summary>
    /// Applies the buff using the specific multiplier and duration
    /// </summary>
    /// <param name="multiplier"></param>
    /// <param name="duration"></param>
    /// <returns></returns>
    private IEnumerator Apply(float multiplier, float duration)
    {
        jumpForce *= multiplier;
        yield return new WaitForSeconds(5);
        jumpForce = _baseJumpForce;
    }

    private void KillPlayer()
    {
        photonView.RPC("SummPlayersDead", RpcTarget.All);
        PhotonNetwork.Destroy(gameObject);
    }
}
