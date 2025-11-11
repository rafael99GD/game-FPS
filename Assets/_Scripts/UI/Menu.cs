using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    // --- Variables del Menú ---
    [Header("-- Pausa del juego --")]
    public GameObject pausePanel;  // Panel que se muestra cuando el juego está pausado.
    private bool isGamePaused = false;  // Indica si el juego está pausado o no.

    void Update()
    {
        // Verifica si se presiona la tecla 'P' para pausar o reanudar el juego.
        if (Input.GetKeyDown(KeyCode.P))
        {
            isGamePaused = !isGamePaused;  // Alterna el estado de pausa.
            PauseGame();  // Llama al método que gestiona la pausa del juego.
        }
    }

    // --- Función para Pausar/Reanudar el Juego ---
    public void PauseGame()
    {
        if (isGamePaused)
        {
            Time.timeScale = 0;  // Detiene el tiempo en el juego (pausa).
            pausePanel.SetActive(true);  // Muestra el panel de pausa.
        }
        else
        {
            Time.timeScale = 1f;  // Reanuda el tiempo en el juego.
            pausePanel.SetActive(false);  // Oculta el panel de pausa.
        }
    }
}
