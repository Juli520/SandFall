using UnityEngine;

public class PushingProjectile : MonoBehaviour
{
    public float force;
    public float speed;

    private void Update()
    {
        transform.position += transform.forward * Time.deltaTime * speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 8)
        {
            collision.gameObject.GetComponent<Rigidbody>();
            collision.rigidbody.AddForce(Vector3.forward * force, ForceMode.Impulse);
            Destroy(this.gameObject);
        }
    }
}
