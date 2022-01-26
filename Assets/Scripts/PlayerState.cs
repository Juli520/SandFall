using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 5;
    public float jumpForce = 7;
    [Header("Death")]
    public float deathHeight = 0f;
    
    private float _baseJumpForce;
    
    private void Awake()
    {
        _baseJumpForce = jumpForce;
    }

    private void Update()
    {
        if (transform.position.y <= deathHeight)
            KillPlayer();
    }

    public void ApplyBuff(float multiplier, float duration)
    {
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
        Destroy(gameObject);
    }
}
