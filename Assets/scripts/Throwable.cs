using UnityEngine;

public class Throwable : MonoBehaviour
{
    private Rigidbody rb;

    private bool pickedUp = false;
    private Transform handTransform;

    public float throwMultiplier = 1.5f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.useGravity = true;
        rb.isKinematic = false;
    }

    void Update()
    {
        float triggerRight = OVRInput.Get(OVRInput.RawAxis1D.RIndexTrigger);

        if (pickedUp)
        {
            transform.position = handTransform.position;
            transform.rotation = handTransform.rotation;

            if (triggerRight < 0.1f)
            {
                DropObject();
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        float triggerRight = OVRInput.Get(OVRInput.RawAxis1D.RIndexTrigger);

        if (!pickedUp && other.CompareTag("hand") && triggerRight > 0.9f)
        {
            PickUp(other.transform);
        }
    }

    void PickUp(Transform hand)
    {
        pickedUp = true;
        handTransform = hand;

        rb.isKinematic = true;
        rb.useGravity = false;

        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    void DropObject()
    {
        pickedUp = false;

        rb.isKinematic = false;
        rb.useGravity = true;

        Vector3 throwVelocity =
            OVRInput.GetLocalControllerVelocity(OVRInput.Controller.RTouch);

        rb.linearVelocity = throwVelocity * throwMultiplier;
    }
}