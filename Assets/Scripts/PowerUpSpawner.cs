using Photon.Pun;
using UnityEngine;
using Random = UnityEngine.Random;

public class PowerUpSpawner : MonoBehaviourPun
{
    public float timeToSpawn = 20f;
    public float boundaryX;
    public float boundaryZ;
    public float dropHeight = 35;
    public GameObject jumpPowerUp;

    private float _currentTime;

    private void Awake()
    {
        _currentTime = timeToSpawn;
    }

    private void Update()
    {
        if(!photonView.IsMine) return;
        
        if (_currentTime <= 0)
            Spawn();
        else
            _currentTime -= Time.deltaTime;
    }

    private void Spawn()
    {
        Vector3 position = new Vector3(Random.Range(-boundaryX, boundaryX), dropHeight, Random.Range(-boundaryZ, boundaryZ));
        Quaternion rotation = new Quaternion(0, Random.Range(0f, 360f), 0, 0);
        
        PhotonNetwork.Instantiate("Prefabs/" + jumpPowerUp.name, position, rotation);
        
        _currentTime = timeToSpawn;
    }
}
