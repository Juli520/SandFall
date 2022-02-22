using Photon.Pun;
using UnityEngine;

public class LevelManager : MonoBehaviourPun
{
    public GameObject player;
    public Transform[] spawnPoints;

    [Header("Scene Changer")] 
    [HideInInspector] public int playersDead;
    public string loseScene;
    public string winScene;

    private void Start()
    {
        if (PhotonNetwork.LocalPlayer.ActorNumber == 1)
            PhotonNetwork.Instantiate(player.name, spawnPoints[0].position, player.transform.rotation);
        else if (PhotonNetwork.LocalPlayer.ActorNumber == 2)
            PhotonNetwork.Instantiate(player.name, spawnPoints[1].position, Quaternion.identity);
        else if (PhotonNetwork.LocalPlayer.ActorNumber == 3)
            PhotonNetwork.Instantiate(player.name, spawnPoints[2].position, Quaternion.identity);
        else if (PhotonNetwork.LocalPlayer.ActorNumber == 4)
            PhotonNetwork.Instantiate(player.name, spawnPoints[3].position, Quaternion.identity);
    }

    private void Update()
    {
        if(!photonView.IsMine) return;
        
        if(playersDead == 3)
            LoadWinScene();
            
    }

    public void SummPlayersDead()
    {
        photonView.RPC("SummPlayersDeadRPC", RpcTarget.All);
    }

    public void LoadWinScene()
    {
        if(winScene == string.Empty) return;
        
        PhotonNetwork.LoadLevel(winScene);
    }

    public void LoadLoseScene()
    {
        if(loseScene == string.Empty) return;
        
        PhotonNetwork.LoadLevel(loseScene);
    }

    [PunRPC]
    public void SummPlayersDeadRPC()
    {
        playersDead++;
    }
}
