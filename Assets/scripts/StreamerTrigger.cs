using UnityEngine;

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

        streamers.Emit(10);
    }
}
