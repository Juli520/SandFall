using UnityEngine;
using TMPro;
using Photon.Pun;

public class PlayerNameTag : MonoBehaviourPun
{
    [SerializeField] private TextMeshProUGUI nameText;
    
    private void Start()
    {
        if(!photonView.IsMine) return;

        photonView.RPC("SetNameRPC", RpcTarget.All);
    }

    [PunRPC]
    public void SetNameRPC()
    {
        nameText.text = photonView.Owner.NickName;
    }
}
