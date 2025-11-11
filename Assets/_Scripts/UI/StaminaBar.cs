using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    // --- Variables de la Barra de Estamina ---
    [Header("-- Barra de Estamina --")]
    public Slider staminaSlider;  // Referencia al Slider que representa la barra de estamina en el UI.

    [Header("-- Configuración de Estamina --")]
    public float maxStamina = 100;  // Cantidad máxima de estamina del jugador.
    private float currentStamina;  // Estamina actual del jugador.

    [Header("-- Regeneración y Pérdida de Estamina --")]
    private float regenerateStaminaTime = 0.1f;  // Tiempo entre cada regeneración de estamina.
    private float regererateAmount = 2;  // Cantidad de estamina que se regenera en cada ciclo.

    private float losingStaminaTime = 0.1f;  // Tiempo entre cada decremento de estamina al usarla.

    private Coroutine myCoroutineLosing;  // Coroutine para la pérdida de estamina.
    private Coroutine myCoroutineRegenerate;  // Coroutine para la regeneración de estamina.

    // Start is called before the first frame update
    void Start()
    {
        // Inicializa la estamina actual y ajusta el slider de la UI.
        currentStamina = maxStamina;
        staminaSlider.maxValue = maxStamina;
        staminaSlider.value = maxStamina;
    }

    // --- Función para Usar Estamina ---
    public void UseStamina(float amount)
    {
        // Verifica si hay suficiente estamina para realizar la acción.
        if (currentStamina - amount > 0)
        {
            // Detiene la regeneración de estamina si está activa.
            if (myCoroutineLosing != null)
            {
                StopCoroutine(myCoroutineLosing);
            }

            // Inicia la pérdida de estamina.
            myCoroutineLosing = StartCoroutine(LosingStaminaCoroutine(amount));

            // Detiene la pérdida de estamina si está activa y comienza la regeneración.
            if (myCoroutineRegenerate != null)
            {
                StopCoroutine(myCoroutineRegenerate);
            }

            myCoroutineRegenerate = StartCoroutine(RegenerateStaminaCoroutine());
        }
        else
        {
            // Si no hay suficiente estamina, muestra un mensaje de advertencia y desactiva el sprint.
            Debug.Log("No hay estamina");
            FindObjectOfType<PlayerMovement>().isSprinting = false;
        }
    }

    // --- Coroutine para Perder Estamina ---
    private IEnumerator LosingStaminaCoroutine(float amount)
    {
        // Mientras haya estamina, sigue disminuyéndola.
        while (currentStamina >= 0)
        {
            currentStamina -= amount;  // Disminuye la estamina.
            staminaSlider.value = currentStamina;  // Actualiza la barra de estamina en el UI.

            yield return new WaitForSeconds(losingStaminaTime);  // Espera antes de continuar.
        }

        myCoroutineLosing = null;

        // Desactiva el sprint cuando la estamina llega a cero.
        FindObjectOfType<PlayerMovement>().isSprinting = false;
    }

    // --- Coroutine para Regenerar Estamina ---
    private IEnumerator RegenerateStaminaCoroutine()
    {
        // Espera 1 segundo antes de empezar a regenerar estamina.
        yield return new WaitForSeconds(1);

        // Mientras la estamina no esté llena, la regeneramos.
        while (currentStamina < maxStamina)
        {
            currentStamina += regererateAmount;  // Regenera estamina.
            staminaSlider.value = currentStamina;  // Actualiza la barra de estamina en el UI.

            yield return new WaitForSeconds(regenerateStaminaTime);  // Espera antes de continuar.
        }

        myCoroutineRegenerate = null;
    }
}
