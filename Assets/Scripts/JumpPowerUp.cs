using Photon.Pun;
using UnityEngine;

public class JumpPowerUp : MonoBehaviourPun
{
    public float duration = 5f;
    public float multiplier = 2f;
    public float timeToDestroy = 10f;

    private void Awake()
    {
        Invoke(nameof(DestroyPowerUp), timeToDestroy);
    }

    private void DestroyPowerUp()
    {
        PhotonNetwork.Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        if(!photonView.IsMine) return;
        
        if (other.gameObject.layer == 8)
        {
            other.gameObject.GetComponent<PlayerState>().ApplyBuff(multiplier, duration);
            DestroyPowerUp();
        }
    }
}
