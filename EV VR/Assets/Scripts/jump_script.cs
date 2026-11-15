using UnityEngine;
using UnityEngine.InputSystem;

public class ControlSalto : MonoBehaviour
{
    [SerializeField] InputActionReference accionSalto;
    [SerializeField] LayerMask capasPiso = ~0;
    [SerializeField] float distanciaExtra = 0.15f;
    [SerializeField] float tiempoCoyote = 0.1f;
    [SerializeField] float bloqueoPostSalto = 0.3f;

    CharacterController cc;
    float ultimaVezEnPiso;
    float ultimoSalto = -10f;

    void Awake()
    {
        cc = GetComponentInParent<CharacterController>();
    }

    void Update()
    {
        if (accionSalto == null) return;
        InputAction accion = accionSalto.action;

        if (DetectarPiso()) ultimaVezEnPiso = Time.time;

        bool puedeSaltar =
            Time.time - ultimaVezEnPiso < tiempoCoyote &&
            Time.time - ultimoSalto > bloqueoPostSalto;

        if (puedeSaltar)
        {
            if (!accion.enabled) accion.Enable();
            if (accion.WasPressedThisFrame()) ultimoSalto = Time.time;
        }
        else if (!accion.IsPressed())
        {
            // En el aire y con el botón suelto: se bloquea una nueva pulsación
            if (accion.enabled) accion.Disable();
        }
    }

    void OnDisable()
    {
        if (accionSalto != null) accionSalto.action.Enable();
    }

    bool DetectarPiso()
    {
        if (cc.isGrounded) return true;

        Vector3 centro = cc.transform.TransformPoint(cc.center);
        float radio = cc.radius * 0.9f;
        float distancia = cc.height / 2f - radio + cc.skinWidth + distanciaExtra;

        return Physics.SphereCast(centro, radio, Vector3.down, out _,
            distancia, capasPiso, QueryTriggerInteraction.Ignore);
    }
}