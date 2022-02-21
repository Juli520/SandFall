using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class LevelManager : MonoBehaviourPun
{
    public GameObject[] players;
    public Transform[] spawnPoints;

    [Header("Scene Changer")] 
    [HideInInspector] public int playersDead;
    [HideInInspector] public bool iAlive;
    //public string loseScene;
    //public string winScene;

    private void Start()
    {
        if (PhotonNetwork.LocalPlayer.ActorNumber == 1)
            PhotonNetwork.Instantiate("Prefabs/" + players[0].name, spawnPoints[0].position, Quaternion.identity);
        else if (PhotonNetwork.LocalPlayer.ActorNumber == 2)
            PhotonNetwork.Instantiate("Prefabs/" + players[1].name, spawnPoints[1].position, Quaternion.identity);
        else if (PhotonNetwork.LocalPlayer.ActorNumber == 3)
            PhotonNetwork.Instantiate("Prefabs/" + players[1].name, spawnPoints[2].position, Quaternion.identity);
        else if (PhotonNetwork.LocalPlayer.ActorNumber == 4)
            PhotonNetwork.Instantiate("Prefabs/" + players[1].name, spawnPoints[3].position, Quaternion.identity);
    }

    //private void Update()
    //{
    //    if(!photonView.IsMine) return;
    //    
    //    if(playersDead == 3 && iAlive)
    //        photonView.RPC("LoadWinScene", photonView.Owner);
    //    else if(playersDead == 3 && !iAlive)
    //        photonView.RPC("LoadLoseScene", photonView.Owner);
    //}

    //[PunRPC]
    //public void SummPlayersDead()
    //{
    //    playersDead++;
    //}

    //[PunRPC]
    //private void LoadWinScene()
    //{
    //    if(winScene == string.Empty) return;
    //    
    //    PhotonNetwork.LoadLevel(winScene);
    //}
    //
    //[PunRPC]
    //private void LoadLoseScene()
    //{
    //    if(loseScene == string.Empty) return;
    //    
    //    PhotonNetwork.LoadLevel(loseScene);
    //}
}
