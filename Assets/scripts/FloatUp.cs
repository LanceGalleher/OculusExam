using System;
using UnityEngine;

public class FloatUp : MonoBehaviour
{
    private bool floatUpToggle = false; // whether the balloon is floating up
    [SerializeField] float floatUpSpeed = 1.2f;

    // Update is called once per frame
    void Update()
    {
        // if the float up toggle is enabled, the balloon should be floating up
        if (floatUpToggle)
        {
            FloatBallonUp();
        }
    }

    /// <summary>
    /// Determines whether the balloon should be floating up.
    /// Call this method in the When Unselect() Interactable Unity Event Wrapper event with a value of true in order to make the balloon start floating up when the player lets go of it.
    /// </summary>
    /// <param name="val">true - balloon floats up, false - balloon is stationary</param>
    public void SetFloatUpToggle(bool val)
    {
        floatUpToggle = val;
    }

    /// <summary>
    /// This increases the y value of the balloon by the speed * the delta time, which allows it to slowly float up over time according to the set speed.
    /// </summary>
    public void FloatBallonUp()
    {
        transform.position += new Vector3(0, floatUpSpeed * Time.deltaTime, 0);
    }
}
