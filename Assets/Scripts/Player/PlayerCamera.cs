using System;
using Photon.Pun;
using UnityEngine;

public class PlayerCamera : MonoBehaviourPun
{
    public float distance;
    [SerializeField, HideInInspector]private Transform _target;

    private void Awake()
    {
        if(!photonView.IsMine) return;
        
        _target = transform.parent;
    }

    private void Update()
    {
        if(!photonView.IsMine) return;
		
        transform.position = new Vector3(transform.position.x, _target.transform.position.y + distance, transform.position.z);
    }
}
