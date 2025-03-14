using UnityEngine;
using Oculus.Interaction.Samples;

public class PassthroughInitializer : MonoBehaviour
{
    void Awake()
    {
        // Set passthrough to be enabled before MRPassthrough runs its OnEnable.
        MRPassthrough.PassThrough.IsPassThroughOn = true;
    }
}
