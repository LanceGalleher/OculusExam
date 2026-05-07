using UnityEngine;

// Allows for cubes to stack on top of each other and should manage rules regarding the stacking (Only four cubes can be stacked on top of each other at a time).

// maxStackHeight - maximum cubes that are allowed to stack on top of each other
// StackableCube below/top - references to cubes above and below a cube in a stack

// Start() - Caches the Rigidbody for physics control
// OnCollisionEnter(Collision collision) - tries to get a stackablecube component from the object, if it's not a stackable cube it does nothing otherwise it will attempt to stack
// TryStackOn(StackableCube baseCube) - handles logic regarding stacking a cube on top of another cube. Should prevent stacking if the cube is 1) moving too fast or 2) the stack is too tall. It calculates how tall the current stack is. Lastly, it attempts to stabilize a stack by stopping it's movement when in a stack.
// GetStackHeightFrom(StackableCube start) - Caculates how many cubes are stacked vertically starting from a given cube. Utilizes raycasting downward to check for cubes underneath
// ApplyInstability(StackableCube cube) - Adds a random force to make the cubes fall when a stack is too tall

public class StackableCube : MonoBehaviour
{
    public int maxStackHeight = 4;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision collision)
    {
        StackableCube other = collision.collider.GetComponent<StackableCube>();
        if (other == null) return;

        TryStackOn(other);
    }

    void TryStackOn(StackableCube baseCube)
    {
        if (rb.linearVelocity.magnitude > 1.5f) return;

        int height = GetStackHeightFrom(baseCube);

        if (height >= maxStackHeight)
        {
            ApplyInstability(baseCube);
            ApplyInstability(this);
            return;
        }

        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    int GetStackHeightFrom(StackableCube start)
    {
        int height = 1;

        Transform current = start.transform;

        while (true)
        {
            Ray ray = new Ray(current.position, Vector3.down);

            if (Physics.Raycast(ray, out RaycastHit hit, 1.1f))
            {
                StackableCube next = hit.collider.GetComponent<StackableCube>();

                if (next == null)
                    break;

                height++;
                current = next.transform;
            }
            else
            {
                break;
            }
        }

        return height;
    }

    void ApplyInstability(StackableCube cube)
    {
        Rigidbody r = cube.GetComponent<Rigidbody>();
        r.AddForce(Random.insideUnitSphere * 2f, ForceMode.Impulse);
        r.AddTorque(Random.insideUnitSphere * 5f, ForceMode.Impulse);
    }
}