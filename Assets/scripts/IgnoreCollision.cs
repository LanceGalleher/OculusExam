using UnityEngine;

public class IgnoreCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Food"))
        {
            Physics.IgnoreCollision(
                collision.collider,
                GetComponent<Collider>()
            );
        }
    }
}
