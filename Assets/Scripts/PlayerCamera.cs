using Photon.Pun;
using UnityEngine;

public class PlayerCamera : MonoBehaviourPun
{
    public Transform target;
    public float distance;

    private void Update()
    {
        if(!photonView.IsMine) return;
		
        if (target == null)
            return;
        
        transform.position = new Vector3(transform.position.x, target.transform.position.y + distance, transform.position.z);
    }
}
