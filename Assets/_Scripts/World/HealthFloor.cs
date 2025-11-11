using System.Collections;
using UnityEngine;

public class HealingFloor : MonoBehaviour
{
    [Header("Healing Settings")]
    // Cantidad de vida que se restaurará por segundo.
    public int healingPerSecond = 10;

    // Variable para rastrear si el jugador está dentro del área.
    private bool playerInside = false;

    // Para controlar la coroutine que aplica la curación.
    private Coroutine healingCoroutine;

    // Este método se llama cuando un objeto entra en el collider del HealingFloor.
    private void OnTriggerEnter(Collider other)
    {
        // Verificar si el objeto que entra es el jugador.
        if (other.CompareTag("Player"))
        {
            playerInside = true;

            // Comienza a curar si el jugador entra en el área y si no se está curando ya.
            if (healingCoroutine == null)
            {
                healingCoroutine = StartCoroutine(ApplyHealing());
            }
        }
    }

    // Este método se llama cuando un objeto sale del collider del HealingFloor.
    private void OnTriggerExit(Collider other)
    {
        // Verificar si el objeto que sale es el jugador.
        if (other.CompareTag("Player"))
        {
            playerInside = false;

            // Detener la coroutine si el jugador sale del área.
            if (healingCoroutine != null)
            {
                StopCoroutine(healingCoroutine);
                healingCoroutine = null;
            }
        }
    }

    // Coroutine que aplica la curación mientras el jugador esté dentro del área.
    private IEnumerator ApplyHealing()
    {
        // Ejecuta la curación mientras el jugador esté dentro.
        while (playerInside)
        {
            // Asegúrate de que el GameManager existe antes de llamar a AddHealth.
            if (GameManager.Instance != null)
            {
                // Añadir vida al jugador según la cantidad de curación por segundo.
                GameManager.Instance.AddHealth(healingPerSecond);
            }

            // Esperar 1 segundo antes de volver a curar.
            yield return new WaitForSeconds(1f);
        }
    }
}
