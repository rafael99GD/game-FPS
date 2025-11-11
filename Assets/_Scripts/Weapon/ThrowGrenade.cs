using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowGrenade : MonoBehaviour
{
    // --- Variables Públicas ---
    [Header("-- Configuración de la Granade --")]
    public float throwForce = 500;  // Fuerza con la que la granada será lanzada.

    [Header("-- Prefabs --")]
    public GameObject grenadePrefab;  // Prefab de la granada que se instanciará.

    void Update()
    {
        // Detecta si el jugador presiona la tecla 'E' para lanzar la granada.
        if (Input.GetKeyDown(KeyCode.E))
        {
            Throw();  // Llama al método Throw() cuando se presiona la tecla.
        }
    }

    // Método para lanzar la granada.
    public void Throw()
    {
        // Crea una nueva granada en la posición y rotación del jugador.
        GameObject newGrenade = Instantiate(grenadePrefab, transform.position, transform.rotation);

        // Aplica una fuerza al Rigidbody de la granada para lanzarla hacia adelante.
        newGrenade.GetComponent<Rigidbody>().AddForce(transform.forward * throwForce);
    }
}
