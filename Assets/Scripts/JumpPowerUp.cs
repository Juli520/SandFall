using Photon.Pun;
using UnityEngine;

public class JumpPowerUp : MonoBehaviourPun
{
    public float duration = 5f;
    public float multiplier = 2f;
    public float timeToDestroy = 10f;

    private void Awake()
    {
        Destroy(gameObject, timeToDestroy);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!photonView.IsMine) return;
        
        if (other.gameObject.layer == 8)
        {
            other.gameObject.GetComponent<PlayerState>().ApplyBuff(multiplier, duration);
            Destroy(gameObject);
        }
    }
}
