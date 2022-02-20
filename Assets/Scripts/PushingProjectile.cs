using Photon.Pun;
using UnityEngine;

public class PushingProjectile : MonoBehaviourPun
{
    public float force;
    public float speed;

    private void Update()
    {
        if(!photonView.IsMine) return;
        transform.position += transform.forward * Time.deltaTime * speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(!photonView.IsMine) return;
        
        if (collision.gameObject.layer == 8)
        {
            collision.rigidbody.AddForce(transform.forward * force, ForceMode.Impulse);
            Destroy(gameObject);
        }
    }
}
