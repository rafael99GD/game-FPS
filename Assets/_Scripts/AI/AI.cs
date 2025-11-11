using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    // --- Variables para el movimiento de patrullaje ---
    public NavMeshAgent navMeshAgent;  // Agente de navegación para el movimiento del enemigo.
    public Transform[] destinations;   // Puntos de destino para que el enemigo patrulle.

    public float distanceToFollowPath = 2; // Distancia mínima para considerar que el enemigo ha llegado al destino.

    private int destinationIndex = 0;  // Índice del destino actual.

    [Header("-- FollowPlayer --")]
    // --- Variables para el seguimiento del jugador ---
    public bool followPlayer;           // Si el enemigo debe seguir al jugador.
    private GameObject player;          // Referencia al objeto del jugador.
    private float distanceToPlayer;     // Distancia entre el enemigo y el jugador.
    public float distanceToFollowPlayer = 10;  // Distancia máxima a la que el enemigo comenzará a seguir al jugador.

    void Start()
    {
        // Si no hay destinos configurados, desactiva este script.
        if (destinations == null || destinations.Length == 0)
            transform.gameObject.GetComponent<AI>().enabled = false;
        else
            navMeshAgent.destination = destinations[destinationIndex].position;  // Establece el primer destino.

        // Encuentra el jugador en la escena.
        player = FindAnyObjectByType<PlayerMovement>().gameObject;
    }

    void Update()
    {
        // Calcula la distancia entre el enemigo y el jugador.
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        // Si está cerca del jugador y la opción de seguir está activada, sigue al jugador.
        if (distanceToPlayer < distanceToFollowPlayer && followPlayer)
            FollowPlayer();
        else
            EnemyPath();
    }

    // --- Lógica para el patrullaje del enemigo ---
    public void EnemyPath()
    {
        // Establece el destino al siguiente punto de patrullaje.
        navMeshAgent.destination = destinations[destinationIndex].position;

        // Si el enemigo ha llegado a su destino, avanza al siguiente destino.
        if (Vector3.Distance(transform.position, destinations[destinationIndex].position) <= distanceToFollowPath)
        {
            // Si no es el último destino, pasa al siguiente.
            if (destinations[destinationIndex] != destinations[destinations.Length - 1])
                destinationIndex++;
            else
                destinationIndex = 0;  // Si es el último destino, regresa al primero.
        }
    }

    // --- Lógica para que el enemigo siga al jugador ---
    public void FollowPlayer()
    {
        navMeshAgent.destination = player.transform.position;  // Establece el destino del enemigo al jugador.
    }

    // --- Lógica para destruir al enemigo cuando es afectado por una granada ---
    public void GrenadeImpact()
    {
        Destroy(gameObject);  // Destruye el objeto (enemigo) al ser impactado por una granada.
    }
}
