using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Granade : MonoBehaviour
{
    [Header("Grenade Settings")]
    // El tiempo de retraso antes de que la granada explote
    public float delay = 3;

    // Temporizador para contar el tiempo hasta la explosión
    private float countdown;

    // El radio de la explosión
    public float radius = 5;

    // La fuerza de la explosión
    public float explosionForce = 70;

    // Bandera que indica si la granada ya explotó
    private bool exploded = false;

    // Efecto visual de la explosión
    public GameObject explosionEffect;

    // Fuente de audio para el sonido de la explosión
    private AudioSource audioSource;

    // Sonido de la explosión
    public AudioClip explosionSound;

    void Start()
    {
        // Inicializa el temporizador con el valor del retraso
        countdown = delay;

        // Obtiene el componente de AudioSource para reproducir el sonido
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Disminuye el temporizador cada fotograma
        countdown -= Time.deltaTime;

        // Si el temporizador llega a cero y la granada no ha explotado aún, explota
        if (countdown <= 0 && exploded == false)
        {
            Explode();
            exploded = true;
        }
    }

    private void Explode()
    {
        // Instancia el efecto de explosión en la posición de la granada
        Instantiate(explosionEffect, transform.position, transform.rotation);

        // Obtiene todos los objetos dentro del radio de la explosión
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        // Recorre todos los objetos dentro del radio
        foreach (var rangeObjects in colliders)
        {
            // Si el objeto tiene el componente AI, ejecuta la lógica de la granada en él
            AI ai = rangeObjects.GetComponent<AI>();
            if (ai != null)
            {
                ai.GrenadeImpact();
            }

            // Si el objeto tiene un Rigidbody, le aplica la fuerza de la explosión
            Rigidbody rb = rangeObjects.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce * 10, transform.position, radius);
            }
        }

        // Reproduce el sonido de la explosión
        audioSource.PlayOneShot(explosionSound);

        // Desactiva el collider y el renderer de la granada para hacerla invisible
        gameObject.GetComponent<CapsuleCollider>().enabled = false;
        gameObject.GetComponent<MeshRenderer>().enabled = false;

        // Destruye la granada después de un tiempo
        Destroy(gameObject, delay * 2);
    }
}
