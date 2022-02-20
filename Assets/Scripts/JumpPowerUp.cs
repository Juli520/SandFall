using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class JumpPowerUp : MonoBehaviour
{
    public float duration = 5f;
    public float multiplier = 2f;
    public float timeToDestroy = 10f;

    private void Awake()
    {
        Destroy(gameObject, timeToDestroy);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == 8)
        {
            other.gameObject.GetComponent<PlayerState>().ApplyBuff(multiplier, duration);
            Destroy(gameObject);
        }
    }
}
