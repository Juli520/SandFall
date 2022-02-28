using System;
using Photon.Pun;
using UnityEngine;

public class PlayerCamera : MonoBehaviourPun
{
    public float distance;
    public Transform target;

    private void Start()
    {
        if(!photonView.IsMine) return;
        
        if (name == "Main Camera")
            target = GameObject.Find("Player 1(Clone)").GetComponent<Transform>();
        else if (name == "Main Camera (1)")
            target = GameObject.Find("Player 2(Clone)").GetComponent<Transform>();
        else if (name == "Main Camera (2)")
            target = GameObject.Find("Player 3(Clone)").GetComponent<Transform>();
        else if (name == "Main Camera (3)")
            target = GameObject.Find("Player 4(Clone)").GetComponent<Transform>();
    }

    private void Update()
    {
        if(!photonView.IsMine) return;

        transform.position = new Vector3(transform.position.x, target.transform.position.y + distance, transform.position.z);
    }
}
