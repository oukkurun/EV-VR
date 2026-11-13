using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Gravity;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Movement;

public class DebugGravedad : MonoBehaviour
{
    public void AlCambiarGrounded(bool grounded)
    {
        Debug.Log($"[Gravedad] Grounded: {grounded}");
    }

    public void AlCambiarLock(GravityOverride estado)
    {
        Debug.Log($"[Gravedad] Lock: {estado}");
    }
}