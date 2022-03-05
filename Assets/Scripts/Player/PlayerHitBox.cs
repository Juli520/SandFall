using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class PlayerHitBox : MonoBehaviourPun
{
    private PlayerState _state;

    private void Awake()
    {
        _state = GetComponentInParent<PlayerState>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8 &&
            other.gameObject != gameObject.transform.parent.gameObject)
        {
            Vector3 direction = (other.transform.position - transform.position).normalized;
            other.gameObject.GetComponent<PlayerState>().PushPlayer(other.gameObject.GetComponent<PlayerState>().owner, direction * _state.pushForce);
        }
    }
}
