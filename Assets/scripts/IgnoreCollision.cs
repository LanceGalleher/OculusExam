using UnityEngine;

// Makes an object ignore collisions with objects tagged with "Food"
// OnCollisionEnter(Collision collision) - checks the tag of the object colliding with it. If it is tagged as "Food" then it should ignore physical collisions for both objects
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
