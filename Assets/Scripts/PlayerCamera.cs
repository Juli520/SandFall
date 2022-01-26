using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Transform target;
    public float distance;

    private void Update()
    {
        if (target == null)
            return;
        
        transform.position = new Vector3(transform.position.x, target.transform.position.y + distance, transform.position.z);
    }
}
