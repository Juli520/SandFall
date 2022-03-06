using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class InvisibleWall : MonoBehaviourPun
{
    private void Start()
    {
        Invoke(nameof(DestroyWall), LevelManager.Instance.timeToStart);
    }

    private void DestroyWall()
    {
        Destroy(gameObject);
    }
}
