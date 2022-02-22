using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitBox : MonoBehaviour
{
    private PlayerState _state;

    private void Awake()
    {
        _state = GetComponentInParent<PlayerState>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 8 && 
           other.gameObject != gameObject.transform.parent.gameObject)
            PushPlayer(other.gameObject);
    }
    
    private void PushPlayer(GameObject body)
    {
        Rigidbody rb = body.gameObject.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * _state.pushForce, ForceMode.Impulse);
    }
}
