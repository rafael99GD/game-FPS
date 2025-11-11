using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    // --- Lógica para la duración de la bala ---
    void Start()
    {
        // Destruye la bala después de 3 segundos.
        Destroy(gameObject, 3);
    }

    // --- Lógica de colisión ---
    private void OnCollisionEnter(Collision collision)
    {
        // Si la bala colisiona con el jugador, destrúyela.
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);  // Destruye la bala.
        }
    }
}
