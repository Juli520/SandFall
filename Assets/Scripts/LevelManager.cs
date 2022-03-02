using System;
using Photon.Pun;
using UnityEngine;

public class LevelManager : MonoBehaviourPun
{
    public static LevelManager Instance = null;
    
    [Header("Objects")] 
    public GameObject[] players;
    public Transform[] spawnPoints;
    [Header("Initial Count")] 
    public float timeToStart = 5;
    [Header("Scene Changer")]
    public string loseScene;
    public string winScene;
    [HideInInspector] public int playersDead;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        
        SpawnPlayers();
    }

    private void Update()
    {
        if(!photonView.IsMine) return;
        
        if(playersDead == 3)
            LoadWinScene();
    }

    private void SpawnPlayers()
    {
        switch (PhotonNetwork.LocalPlayer.ActorNumber)
        {
            case 1:
                PhotonNetwork.Instantiate("Prefabs/" + players[0].name, spawnPoints[0].position, Quaternion.identity);
                break;
            case 2:
                PhotonNetwork.Instantiate("Prefabs/" + players[1].name, spawnPoints[1].position, Quaternion.identity);
                break;
            case 3:
                PhotonNetwork.Instantiate("Prefabs/" + players[2].name, spawnPoints[2].position, Quaternion.identity);
                break;
            case 4:
                PhotonNetwork.Instantiate("Prefabs/" + players[3].name, spawnPoints[3].position, Quaternion.identity);
                break;
        }
    }
    
    public void SumPlayersDead()
    {
        photonView.RPC("SumPlayersDeadRPC", RpcTarget.All);
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
    public void SmmPlayersDeadRPC()
    {
        playersDead++;
    }
}
