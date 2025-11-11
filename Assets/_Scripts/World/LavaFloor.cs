using System.Collections;
using UnityEngine;

public class LavaFloor : MonoBehaviour
{
    /// <summary>
    /// Daño que se aplicará al jugador por segundo mientras esté en la lava.
    /// </summary>
    public int damagePerSecond = 10;

    /// <summary>
    /// Variable para rastrear si el jugador está dentro del área de la lava.
    /// </summary>
    private bool playerInside = false;

    /// <summary>
    /// Variable para manejar la coroutine que aplica daño al jugador.
    /// </summary>
    private Coroutine damageCoroutine;

    /// <summary>
    /// Método llamado cuando un objeto entra en el área del trigger de la lava.
    /// </summary>
    /// <param name="other">El collider del objeto que entra.</param>
    private void OnTriggerEnter(Collider other)
    {
        // Verificar si el objeto que entra es el jugador.
        if (other.CompareTag("Player"))
        {
            playerInside = true;
            Debug.Log("Player inside");

            // Comienza a aplicar daño si el jugador entra en el área.
            if (damageCoroutine == null)
            {
                damageCoroutine = StartCoroutine(ApplyDamage());
            }
        }
    }

    /// <summary>
    /// Método llamado cuando un objeto sale del área del trigger de la lava.
    /// </summary>
    /// <param name="other">El collider del objeto que sale.</param>
    private void OnTriggerExit(Collider other)
    {
        // Verificar si el objeto que sale es el jugador.
        if (other.CompareTag("Player"))
        {
            playerInside = false;

            // Detener la coroutine si el jugador sale del área.
            if (damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine);
                damageCoroutine = null;
            }
        }
    }

    /// <summary>
    /// Coroutine que aplica daño al jugador mientras esté dentro del área de la lava.
    /// </summary>
    private IEnumerator ApplyDamage()
    {
        // Aplica daño de forma continua mientras el jugador esté dentro del área.
        while (playerInside)
        {
            // Asegúrate de que el GameManager existe antes de llamar a LoseHealth.
            if (GameManager.Instance != null)
            {
                GameManager.Instance.LoseHealth(damagePerSecond);
            }

            yield return new WaitForSeconds(1f); // Esperar 1 segundo antes de volver a aplicar daño.
        }
    }
}
