using UnityEngine;

public class SpawnProjectiles : MonoBehaviour
{
    public float fireRate;
    public GameObject bullet;
    public Transform spawn;
    private float _currentFireRate;

    private void Start()
    {
        _currentFireRate = fireRate;
    }

    private void Update()
    {
        if (_currentFireRate <= 0)
            Shoot();
        else
            _currentFireRate -= Time.deltaTime;
    }

    private void Shoot()
    {
        Instantiate(bullet, new Vector3(spawn.position.x, spawn.position.y, spawn.position.z), spawn.rotation);
        _currentFireRate = fireRate;
    }
}
