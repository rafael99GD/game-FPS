using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeapongLogic : MonoBehaviour
{
    [Header("Weapon Settings")]
    // Punto de origen desde donde se dispara la bala
    public Transform spawnPoint;

    // Prefab de la bala que se instancia cuando se dispara
    public GameObject bullet;

    // Fuerza con la que se dispara la bala
    public float shotForce = 1500f;

    // Tasa de disparo (tiempo en segundos entre disparos consecutivos)
    public float shotRate = 0.5f;

    private float shotRateTime = 0; // Tiempo del último disparo

    private AudioSource audioSource; // Para reproducir el sonido del disparo

    [Header("Audio Settings")]
    // Sonido que se reproduce al disparar
    public AudioClip shotSound;

    // Determina si el jugador puede disparar de forma continua
    public bool continueShooting = false;

    private void Start()
    {
        // Obtiene el componente de AudioSource
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // Verifica si el jugador ha presionado el botón de disparo
        if (Input.GetButtonDown("Fire1"))
        {
            // Si ha pasado el tiempo necesario para disparar y hay munición
            if (Time.time > shotRateTime && GameManager.Instance.gunAmmo > 0)
            {
                // Si continuar disparando es verdadero, dispara continuamente
                if (continueShooting)
                {
                    InvokeRepeating("Shoot", 0.001f, shotRate);
                }
                else
                {
                    // Dispara una sola vez
                    Shoot();
                }
            }
        }
        // Si se deja de presionar el botón y se estaba disparando continuamente, cancela la repetición
        else if (Input.GetButtonUp("Fire1") && continueShooting)
        {
            CancelInvoke("Shoot");
        }
    }

    // Función para disparar una bala
    public void Shoot()
    {
        // Verifica que haya munición disponible antes de disparar
        if (GameManager.Instance.gunAmmo > 0)
        {
            // Reproduce el sonido del disparo
            audioSource.PlayOneShot(shotSound);

            // Disminuye la cantidad de munición
            GameManager.Instance.gunAmmo--;

            // Crea una nueva bala en la posición del spawnPoint
            GameObject newBullet;
            newBullet = Instantiate(bullet, spawnPoint.position, spawnPoint.rotation);

            // Aplica una fuerza a la bala para dispararla
            newBullet.GetComponent<Rigidbody>().AddForce(spawnPoint.forward * shotForce);

            // Establece el tiempo del siguiente disparo
            shotRateTime = Time.time + shotRate;

            // Destruye la bala después de 3 segundos
            Destroy(newBullet, 3);
        }
        else
        {
            // Si no hay munición, cancela cualquier disparo repetido
            CancelInvoke("Shoot");
        }
    }
}
