using System.Collections;
using Photon.Pun;
using UnityEngine;

public class Sand : MonoBehaviourPun
{
    public float standTime = 1f;
    public float destroyTime = 0.5f;
    
    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.useGravity = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(!photonView.IsMine) return;
        
        if (collision.gameObject.layer == 8)
            StartCoroutine(WaitForFall());
    }

    private IEnumerator WaitForFall()
    {
        yield return new WaitForSeconds(standTime);
        
        _rb.useGravity = true;
        _rb.constraints = RigidbodyConstraints.None;

        yield return new WaitForSeconds(destroyTime);
        Destroy(gameObject);
    }
}
