using UnityEngine;
using UnityEngine.InputSystem;

public class PJ_manager : MonoBehaviour
{
    [SerializeField] GameObject traslacion;
    [SerializeField] GameObject jump;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        traslacion.SetActive(false);
        jump.SetActive(false);
    }

    public void Comenzar()
    {
        traslacion.SetActive(true);
        jump.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
