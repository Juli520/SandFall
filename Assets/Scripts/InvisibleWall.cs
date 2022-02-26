using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleWall : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, LevelManager.Instance.timeToStart);
    }
}
