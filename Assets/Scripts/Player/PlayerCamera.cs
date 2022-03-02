using System;
using Photon.Pun;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public float distance;
    public Transform target;

    private void Start()
    {
        // Make sure the players spawned
        Invoke(nameof(SetTarget), 2f);
    }

    private void SetTarget()
    {
        PlayerState[] players = FindObjectsOfType<PlayerState>();
        foreach (PlayerState player in players)
        {
            if (PhotonView.Get(player).IsMine)
            {
                target = player.transform;
                break;
            }
        }
        
        if (target == null)
            Destroy(gameObject);
    }
    
    private void Update()
    {
        if(/*!photonView.IsMine ||*/ target == null) return;

        transform.position = new Vector3(transform.position.x, target.transform.position.y + distance, transform.position.z);
    }
    
}
