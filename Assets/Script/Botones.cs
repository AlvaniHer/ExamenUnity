using UnityEngine;
using UnityEngine.SceneManagement;

public class Botones : MonoBehaviour
{

    // Método para iniciar el juego
    public void Reinicio()
    {
        // Carga la escena del juego 
        SceneManager.LoadScene("Nivel1"); //Nivel principal 
    }


    // Método para salir del juego
    public void Salir()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();
    }

}