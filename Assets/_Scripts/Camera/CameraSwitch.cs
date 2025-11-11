using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    // --- Variables públicas para la cámara ---
    [Header("-- Cámaras --")]
    public Camera thirdPersonCamera;            // Cámara en tercera persona.
    public Camera firstPersonCamera;            // Cámara en primera persona.

    private bool firstPersonEnable = true;      // Estado que determina si la cámara es en primera persona.

    // --- Variables para cambiar las armas según la vista ---
    [Header("-- Cambiar Armas --")]
    public Transform[] weaponsTransformsFirstPerson;  // Transformaciones de las armas en primera persona.
    public Transform[] weaponsTransformsThirdPerson;  // Transformaciones de las armas en tercera persona.
    public GameObject[] weapons;                       // Armas disponibles.

    private void Start()
    {
        firstPersonEnable = true;                    // Habilita por defecto la vista en primera persona.
        ChangeCamera();                               // Llama al método para ajustar la cámara al inicio.
    }

    void Update()
    {
        // Cambia la cámara cuando se presiona la tecla F3.
        if (Input.GetKeyDown(KeyCode.F3))
        {
            firstPersonEnable = !firstPersonEnable;    // Cambia entre primera y tercera persona.
            ChangeCamera();
        }
    }

    // --- Cambia entre la vista en primera y tercera persona ---
    public void ChangeCamera()
    {
        if (firstPersonEnable)
        {
            // Habilita la cámara en primera persona y deshabilita la de tercera persona.
            firstPersonCamera.enabled = true;
            thirdPersonCamera.enabled = false;

            // Ajusta las posiciones y rotaciones de las armas para la primera persona.
            ChangeWeaponsFirstPerson();
        }
        else
        {
            // Habilita la cámara en tercera persona y deshabilita la de primera persona.
            firstPersonCamera.enabled = false;
            thirdPersonCamera.enabled = true;

            // Ajusta las posiciones y rotaciones de las armas para la tercera persona.
            ChangeWeaponsThirdPerson();
        }
    }

    // --- Cambia las posiciones y rotaciones de las armas en primera persona ---
    public void ChangeWeaponsFirstPerson()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            // Ajusta cada arma para que coincida con su transformación en primera persona.
            weapons[i].transform.position = weaponsTransformsFirstPerson[i].transform.position;
            weapons[i].transform.rotation = weaponsTransformsFirstPerson[i].transform.rotation;
            weapons[i].transform.localScale = weaponsTransformsFirstPerson[i].transform.localScale;
        }
    }

    // --- Cambia las posiciones y rotaciones de las armas en tercera persona ---
    public void ChangeWeaponsThirdPerson()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            // Ajusta cada arma para que coincida con su transformación en tercera persona.
            weapons[i].transform.position = weaponsTransformsThirdPerson[i].transform.position;
            weapons[i].transform.rotation = weaponsTransformsThirdPerson[i].transform.rotation;
            weapons[i].transform.localScale = weaponsTransformsThirdPerson[i].transform.localScale;
        }
    }
}
