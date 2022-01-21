using System.Collections;
using UnityEngine;

public class Fall : MonoBehaviour
{
    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _rb.useGravity = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 8)
            StartCoroutine(WaitForFall());
    }

    IEnumerator WaitForFall()
    {
        yield return new WaitForSeconds(1.5f);
        
        _rb.useGravity = true;
        _rb.constraints = RigidbodyConstraints.None;

        yield return new WaitForSeconds(1);
        Destroy(this.gameObject);
    }
}
