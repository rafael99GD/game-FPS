using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    [Header("Weapon Sway Settings")]
    // La rotación inicial del arma, que se establece al comienzo
    private Quaternion startRotation;

    // La cantidad de movimiento que el arma tiene al mover el mouse
    public float swayAmount = 8;

    private void Start()
    {
        // Establece la rotación inicial del arma cuando comienza el juego
        startRotation = transform.localRotation;
    }

    private void Update()
    {
        // Llama a la función que gestiona el movimiento del arma
        Sway();
    }

    private void Sway()
    {
        // Obtiene los movimientos del mouse en los ejes X y Y
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // Calcula la rotación en el eje X y Y del arma basado en el movimiento del mouse
        Quaternion xAngle = Quaternion.AngleAxis(mouseX * -1.25f, Vector3.up);
        Quaternion yAngle = Quaternion.AngleAxis(mouseY * -1.25f, Vector3.left);

        // Establece la rotación objetivo combinando la rotación inicial con las rotaciones del mouse
        Quaternion targetRotation = startRotation * xAngle * yAngle;

        // Realiza una transición suave entre la rotación actual del arma y la rotación objetivo
        transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, Time.deltaTime * swayAmount);
    }
}
