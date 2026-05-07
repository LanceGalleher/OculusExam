using UnityEngine;

public class StackableCube : MonoBehaviour
{
    public int maxStackHeight = 4;

    public StackableCube below;
    public StackableCube top;

    private Rigidbody rb;

    private bool isLockedToStack = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (isLockedToStack) return;

        StackableCube other = collision.collider.GetComponent<StackableCube>();
        if (other == null) return;

        if (rb.linearVelocity.magnitude > 1.5f) return;

        TryAttachTo(other);
    }

    void TryAttachTo(StackableCube baseCube)
    {
        if (baseCube.top != null && baseCube.top != this)
        {
            baseCube.ApplyInstability();
            ApplyInstability();
            return;
        }

        if (baseCube.GetStackHeight() >= maxStackHeight)
        {
            ApplyInstability();
            baseCube.ApplyInstability();
            return;
        }

        below = baseCube;
        baseCube.top = this;

        isLockedToStack = true;

        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        rb.isKinematic = false;
    }

    public int GetStackHeight()
    {
        int height = 1;
        StackableCube current = below;

        while (current != null)
        {
            height++;
            current = current.below;
        }

        return height;
    }

    public void ApplyInstability()
    {
        rb.AddForce(Random.insideUnitSphere * 2f, ForceMode.Impulse);
        rb.AddTorque(Random.insideUnitSphere * 5f, ForceMode.Impulse);
    }

    public void Detach()
    {
        if (below != null)
        {
            below.top = null;
            below = null;
        }

        isLockedToStack = false;
    }
}