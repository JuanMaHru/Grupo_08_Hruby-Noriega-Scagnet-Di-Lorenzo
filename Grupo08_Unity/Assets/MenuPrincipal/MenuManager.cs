using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuDeEscenas : MonoBehaviour
{
    // Cargar la escena del menú principal
    public void CargarMenu()
    {
        SceneManager.LoadScene("MenuPrincipal/MenuDeEscenas");
    }

    // TP01
    public void CargarEjercicio01()
    {
        SceneManager.LoadScene("TP 01/Scenes/Ejercicio_01_SimpleList");
    }

    // TP02
    public void CargarEjercicio02()
    {
        SceneManager.LoadScene("TP 02/Scenes/Ejercicio02_MyList");
    }

    public void CargarEjercicio03()
    {
        SceneManager.LoadScene("TP 02/Scenes/Ejercicio_03_Store");
    }

    // TP03
    public void CargarEjercicio04()
    {
        SceneManager.LoadScene("TP 03/Scenes/Ejercicio_04_Cola");
    }

    public void CargarEjercicio05()
    {
        SceneManager.LoadScene("TP 03/Scenes/Ejercicio_05_Pila");
    }

    // TP05
    public void CargarEjercicio07()
    {
        SceneManager.LoadScene("TP 05/Scenes/Ejercicio_07_Recursion");
    }

    // Salir del juego
    public void Salir()
    {
        Application.Quit();
    }
}
