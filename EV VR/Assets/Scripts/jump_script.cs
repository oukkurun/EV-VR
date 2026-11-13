using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Locomotion;

public class ControlSalto : MonoBehaviour
{
    [SerializeField] LayerMask capasPiso = ~0;
    [SerializeField] float distanciaExtra = 0.15f;
    [SerializeField] float tiempoCoyote = 0.1f;

    LocomotionProvider salto;
    CharacterController cc;
    float ultimaVezEnPiso;
    float ultimaVezSubiendo;
    float ultimaY;

    void Awake()
    {
        salto = GetComponent<LocomotionProvider>();
        cc = GetComponentInParent<CharacterController>();
    }

    void Start()
    {
        ultimaY = cc.transform.position.y;
    }

    void Update()
    {
        bool enPiso = DetectarPiso();
        if (enPiso) ultimaVezEnPiso = Time.time;

        float y = cc.transform.position.y;
        float velY = (y - ultimaY) / Mathf.Max(Time.deltaTime, 0.0001f);
        ultimaY = y;
        if (velY > 0.2f) ultimaVezSubiendo = Time.time;

        bool cayendo = velY < -0.5f;
        bool permitir = enPiso
            || Time.time - ultimaVezEnPiso < tiempoCoyote
            || !cayendo;

        if (salto.enabled != permitir) salto.enabled = permitir;
    }

    void OnDisable()
    {
        if (salto != null) salto.enabled = true;
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