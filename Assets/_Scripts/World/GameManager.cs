using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// GameManager es responsable de manejar el estado global del juego,
/// incluyendo la salud, munición, puntos y el manejo de la salida del juego.
/// </summary>
public class GameManager : MonoBehaviour
{
    // Instancia del GameManager (patrón Singleton)
    public static GameManager Instance { get; private set; }

    // Referencias a los elementos de UI
    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI healtText;
    public TextMeshProUGUI pointsText;

    // Variables relacionadas con el estado del jugador
    public int health = 100;  // Salud actual del jugador
    public int gunAmmo = 10;  // Munición actual del jugador
    public int points = 0;    // Puntos actuales del jugador
    public int maxHealth = 100; // Salud máxima del jugador

    // Variables para el control de la salida del juego
    private float holdTime = 5f;  // Tiempo necesario para mantener Escape
    private float holdTimer = 0f; // Temporizador para contar el tiempo que se mantiene Escape

    /// <summary>
    /// Método que se ejecuta al iniciar el objeto. 
    /// Aquí se establece la instancia de GameManager.
    /// </summary>
    private void Awake()
    {
        Instance = this;
    }

    /// <summary>
    /// Método que se ejecuta cada frame.
    /// Actualiza los textos de UI y controla la lógica de salida del juego.
    /// </summary>
    private void Update()
    {
        // Actualiza los textos de munición, salud y puntos en la UI
        ammoText.text = gunAmmo.ToString();
        healtText.text = health.ToString();
        pointsText.text = points.ToString();

        // Detecta si la tecla Escape está siendo presionada
        if (Input.GetKey(KeyCode.Escape))
        {
            holdTimer += Time.deltaTime; // Incrementa el temporizador cada frame que se mantenga Escape

            // Si el tiempo supera el umbral, cierra el juego
            if (holdTimer >= holdTime)
            {
                Debug.Log("Saliendo del juego...");
                Application.Quit(); // Cerrar el juego
            }
        }
        else
        {
            holdTimer = 0f; // Reinicia el temporizador si se suelta Escape
        }
    }

    /// <summary>
    /// Método que se llama cuando el jugador pierde salud.
    /// </summary>
    /// <param name="healthToReduce">La cantidad de salud a reducir.</param>
    public void LoseHealth(int healthToReduce)
    {
        health -= healthToReduce;  // Reduce la salud
        CheckHealth(); // Verifica si la salud es 0 o menos
    }

    /// <summary>
    /// Verifica si la salud del jugador es 0 o menor. Si es así, reinicia la escena.
    /// </summary>
    public void CheckHealth()
    {
        if (health <= 0)
        {
            Debug.Log("Has muerto");

            // Reinicia la escena actual en caso de muerte
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    /// <summary>
    /// Método que agrega salud al jugador. Si excede la salud máxima, se ajusta al valor máximo.
    /// </summary>
    /// <param name="health">Cantidad de salud a agregar.</param>
    public void AddHealth(int health)
    {
        if (this.health + health > maxHealth)
        {
            this.health = maxHealth; // Ajusta a la salud máxima
        }
        else
        {
            this.health += health; // Suma la salud
        }
    }

    /// <summary>
    /// Método que agrega puntos al jugador.
    /// </summary>
    /// <param name="points">Cantidad de puntos a agregar.</param>
    public void AddPoints(int points)
    {
        this.points += points; // Suma los puntos
    }
}
