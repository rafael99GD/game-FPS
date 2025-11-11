using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Método que se llama cuando la bala colisiona con otro objeto.
    private void OnCollisionEnter(Collision collision)
    {
        // Verifica si el objeto con el que colisiona tiene la etiqueta "Enemy".
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Si colisiona con un enemigo, destruye ese objeto enemigo.
            Destroy(collision.gameObject);
        }

        // Verifica si el objeto con el que colisiona tiene la etiqueta "Enemy".
        if (collision.gameObject.CompareTag("EnemyCube"))
        {
            // Si colisiona con un enemigo, destruye ese objeto enemigo.
            GameManager.Instance.AddPoints(30);
            Destroy(collision.gameObject);
        }
    }
}
