using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    // --- Variables públicas para configurar el punto de inicio del jugador ---
    [Header("-- Configuración de interacciones del jugador --")]
    public Transform startPosition;  // Referencia al punto donde el jugador será reposicionado en caso de muerte o caída.

    private void OnTriggerEnter(Collider other)
    {
        // Si el objeto con el que colisiona el jugador tiene la etiqueta "GunAmmo", se suman municiones.
        if (other.gameObject.CompareTag("GunAmmo"))
        {
            GameManager.Instance.gunAmmo += other.gameObject.GetComponent<AmmoBox>().ammo;
            Destroy(other.gameObject);  // Destruye el objeto de munición tras recogerlo.
        }

        // Si el objeto con el que colisiona el jugador tiene la etiqueta "HealthObject", se aumenta la salud.
        if (other.gameObject.CompareTag("HealthObject"))
        {
            GameManager.Instance.AddHealth(other.gameObject.GetComponent<HealthObject>().health);
            Destroy(other.gameObject);  // Destruye el objeto de salud tras recogerlo.
        }

        // Si el objeto con el que colisiona el jugador tiene la etiqueta "DamageObject", se pierde salud.
        if (other.gameObject.CompareTag("DamageObject"))
        {
            GameManager.Instance.LoseHealth(other.gameObject.GetComponent<DamageObject>().damage);
            Destroy(other.gameObject);  // Destruye el objeto de daño tras la colisión.
        }

        // Si el objeto con el que colisiona el jugador tiene la etiqueta "CoinObject", se suman puntos.
        if (other.gameObject.CompareTag("CoinObject"))
        {
            GameManager.Instance.AddPoints(other.gameObject.GetComponent<CoinObject>().points);
            Destroy(other.gameObject);  // Destruye el objeto de moneda tras recogerlo.
        }

        // Si el jugador cae en el "DeathFloor", pierde salud y se reposiciona en el punto de inicio.
        if (other.gameObject.CompareTag("DeathFloor"))
        {
            GameManager.Instance.LoseHealth(20);  // El jugador pierde 20 puntos de salud por caer en el suelo de la muerte.

            GetComponent<CharacterController>().enabled = false;  // Desactiva el controlador de personaje temporalmente para evitar problemas al reposicionar al jugador.
            gameObject.transform.position = startPosition.transform.position;  // Reposiciona al jugador en el punto de inicio.
            GetComponent<CharacterController>().enabled = true;  // Vuelve a activar el controlador de personaje.
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Si el jugador colisiona con una bala enemiga, pierde salud.
        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            GameManager.Instance.LoseHealth(5);  // El jugador pierde 5 puntos de salud por cada bala enemiga.
        }
    }
}
