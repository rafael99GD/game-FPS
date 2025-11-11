using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    // --- Variables para el Cambio de Armas ---
    [Header("-- Armas del Jugador --")]
    public GameObject[] weapons;  // Array de las armas del jugador, que se pueden seleccionar con la rueda del ratón.

    [Header("-- Selección de Arma --")]
    public int selectedWeapon = 0;  // Índice del arma seleccionada actualmente.

    void Update()
    {
        // Guarda el arma previamente seleccionada para comparar después.
        int previousWeapon = selectedWeapon;

        // Detecta la rueda del ratón para cambiar el arma hacia arriba.
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (selectedWeapon >= weapons.Length - 1)
            {
                selectedWeapon = 0;  // Si es la última arma, selecciona la primera.
            }
            else
            {
                selectedWeapon++;  // Selecciona el siguiente arma.
            }
        }

        // Detecta la rueda del ratón para cambiar el arma hacia abajo.
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (selectedWeapon <= 0)
            {
                selectedWeapon = weapons.Length - 1;  // Si es la primera arma, selecciona la última.
            }
            else
            {
                selectedWeapon--;  // Selecciona el arma anterior.
            }
        }

        // Si el arma seleccionada ha cambiado, actualiza la selección de armas.
        if (previousWeapon != selectedWeapon)
        {
            SelectWeapon();
        }
    }

    // Método que maneja la visibilidad de las armas, activando la seleccionada y desactivando las demás.
    private void SelectWeapon()
    {
        int i = 0;

        // Itera sobre las armas del jugador.
        foreach (Transform weapon in transform)
        {
            // Asegura que solo se activen/desactiven las armas (Layer "Weapon").
            if (weapon.gameObject.layer == LayerMask.NameToLayer("Weapon"))
            {
                if (i == selectedWeapon)
                {
                    weapon.gameObject.SetActive(true);  // Activa el arma seleccionada.
                }
                else
                {
                    weapon.gameObject.SetActive(false);  // Desactiva las demás armas.
                }

                i++;
            }
        }
    }
}
