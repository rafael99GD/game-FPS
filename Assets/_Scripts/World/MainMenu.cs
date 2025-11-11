using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    /// <summary>
    /// Cargar la escena de juego cuando el jugador presiona el botón "Play".
    /// </summary>
    public void PlayGame()
    {
        // Cargar la escena con el índice 1 en Build Settings.
        SceneManager.LoadScene(1);
    }

    /// <summary>
    /// Cerrar el juego cuando el jugador presiona el botón "Exit".
    /// </summary>
    public void ExitGame()
    {
        // Mostrar mensaje en la consola antes de cerrar el juego.
        Debug.Log("Saliendo del juego...");

        // Salir del juego.
        Application.Quit();
    }
}
