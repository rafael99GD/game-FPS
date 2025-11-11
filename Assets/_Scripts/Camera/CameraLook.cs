using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour
{
    // --- Variables públicas para configuración ---
    [Header("-- Configuración de la cámara --")]
    public float mouseSensitivity = 80f;  // Sensibilidad del ratón para la rotación de la cámara.
    public Transform playerBody;          // Referencia al cuerpo del jugador para moverlo al rotar la cámara.

    private float xRotation = 0;          // Rotación en el eje X de la cámara, usada para controlar el movimiento vertical.

    // Start is called before the first frame update
    void Start()
    {
        // Bloquea el cursor al centro de la pantalla para ocultarlo y evitar que se mueva fuera del área visible.
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        // Obtiene el movimiento del ratón en los ejes X y Y, multiplicado por la sensibilidad y el tiempo entre fotogramas.
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Calcula la rotación vertical de la cámara en el eje X.
        xRotation -= mouseY;

        // Limita la rotación vertical para que no gire más de 90 grados hacia arriba o hacia abajo.
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Aplica la rotación vertical a la cámara.
        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);

        // Rota el cuerpo del jugador en el eje Y, usando la rotación del ratón en el eje X.
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
