using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    // --- Referencias a objetos públicos para disparo y posición ---
    [Header("-- Disparo del Enemigo --")]
    public GameObject enemyBullet;                // Prefab de la bala del enemigo.
    public Transform spawnBulletPoint;            // Punto de spawn donde se crea la bala.
    public float bulletVelocity = 50;             // Velocidad de la bala.

    private Transform playerPosition;             // Referencia a la posición del jugador.

    void Start()
    {
        // Encuentra la posición del jugador en el juego.
        playerPosition = FindAnyObjectByType<PlayerMovement>().transform;

        // Llama al método ShootPlayer después de 3 segundos.
        Invoke("ShootPlayer", 3);
    }

    void Update()
    {
        // Este método se podría usar para más lógica en el futuro, pero está vacío por ahora.
    }

    // --- Lógica para disparar al jugador ---
    void ShootPlayer()
    {
        // Calcula la dirección del jugador con respecto al enemigo.
        Vector3 playerDirection = playerPosition.position - transform.position;

        // Instancia una nueva bala en el punto de disparo.
        GameObject newBullet;

        newBullet = Instantiate(enemyBullet, spawnBulletPoint.position, spawnBulletPoint.rotation);

        // Aplica una fuerza en la dirección del jugador a la bala.
        newBullet.GetComponent<Rigidbody>().AddForce(playerDirection * bulletVelocity, ForceMode.Force);

        // Llama nuevamente a ShootPlayer después de 3 segundos para disparar de nuevo.
        Invoke("ShootPlayer", 3);
    }
}
