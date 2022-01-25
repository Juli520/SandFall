using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPowerUp : MonoBehaviour
{
    public float duration = 5f;
    public float multiplier = 2f;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            other.gameObject.GetComponent<PlayerState>().ApplyBuff(multiplier, duration);
            Destroy(gameObject);
        }
    }
}
