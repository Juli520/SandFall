using System;
using System.Collections;
using Photon.Pun;
using UnityEngine;

public class FallBlock : MonoBehaviourPun
{
    public float standTime = 1f;
    public float destroyTime = 0.5f;
    private bool _canFall = false;

    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.useGravity = false;
    }

    private void Start()
    {
        Invoke(nameof(SetFall), LevelManager.Instance.timeToStart);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 8 && _canFall)
            StartCoroutine(WaitForFall());
    }

    private IEnumerator WaitForFall()
    {
        yield return new WaitForSeconds(standTime);
        
        _rb.useGravity = true;
        _rb.constraints = RigidbodyConstraints.None;

        yield return new WaitForSeconds(destroyTime);
        photonView.RPC("DestroyBlock", RpcTarget.All);
    }

    [PunRPC]
    private void DestroyBlock()
    {
        //PhotonNetwork.Destroy(gameObject);
        Destroy(gameObject);
    }
    
    public void SetFall()
    {
        _canFall = true;
    }
}
