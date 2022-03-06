﻿using System.Collections;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Serialization;

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
    public bool playerAlive = true;
    
    private LevelManager _lvlManager;

    private float _baseJumpForce;
    
    private void Awake()
    {
        if(!photonView.IsMine) return;

        _lvlManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();

        _baseJumpForce = jumpForce;
    }

    private void Update()
    {
        if(!photonView.IsMine) return;
        
        if (transform.position.y <= deathHeight)
            KillPlayer();
        
        if (_lvlManager.playersDead == PhotonNetwork.PlayerList.Length - 1 && playerAlive)
            _lvlManager.LoadWinScene();
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
        playerAlive = false;
        _lvlManager.SumPlayersDead();
        _lvlManager.LoadLoseScene();
        PhotonNetwork.Destroy(gameObject);
    }
}
