using System.Collections;
using Photon.Pun;
using UnityEngine;

public class PlayerState : MonoBehaviourPun
{
    [Header("Movement")]
    public float speed = 5;
    public float jumpForce = 7;

    private float _baseJumpForce;
    
    private void Awake()
    {
        _baseJumpForce = jumpForce;
    }

    public void ApplyBuff(float multiplier, float duration)
    {
        StartCoroutine(Apply(multiplier, duration));
    }
    
    private IEnumerator Apply(float multiplier, float duration)
    {
        jumpForce *= multiplier;
        yield return new WaitForSeconds(5);
        jumpForce = _baseJumpForce;
    }
}
