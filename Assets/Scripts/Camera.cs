using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform target;
    public float distance;

    private void Update()
    {
        transform.position = new Vector3(transform.position.x, target.transform.position.y + distance, transform.position.z);
    }
}
