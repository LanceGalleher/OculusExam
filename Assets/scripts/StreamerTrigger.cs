using UnityEngine;

// Triggers a particle system when triggers are pressed

// Update()- R/LTrigger reads right and left trigger input and then if either are pressed it calls EmitStreamers()
// EmitStreamers() - Handles the particle event system. Plays the system and emites 5 particles

public class StreamerTrigger : MonoBehaviour
{
    public ParticleSystem streamers;

    void Update()
    {
        float Rtrigger = OVRInput.Get(OVRInput.RawAxis1D.RIndexTrigger);

        float Ltrigger = OVRInput.Get(OVRInput.RawAxis1D.LIndexTrigger);

        if (Rtrigger > 0.9f)
        {
            EmitStreamers();
        }

        if (Ltrigger > 0.9f)
        {
            EmitStreamers();
        }
    }

    void EmitStreamers()
    {
        if (!streamers.isPlaying)
        {
            streamers.Play();
        }

        streamers.Emit(5);
    }
}
