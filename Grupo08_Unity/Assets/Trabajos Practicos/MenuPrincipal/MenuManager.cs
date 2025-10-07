using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuDeEscenas : MonoBehaviour
{
    // Cargar la escena del menú principal
    public void CargarMenu()
    {
        SceneManager.LoadScene("MenuDeEscenas");
    }

    // TP01
    public void CargarEjercicio01()
    {
        SceneManager.LoadScene("Ejercicio_01_SimpleList");
    }

    // TP02
    public void CargarEjercicio02()
    {
        SceneManager.LoadScene("Ejercicio02_MyList");
    }

    public void CargarEjercicio03()
    {
        SceneManager.LoadScene("Ejercicio_03_Store");
    }

    // TP03
    public void CargarEjercicio04()
    {
        SceneManager.LoadScene("Ejercicio_04_Cola");
    }

    public void CargarEjercicio05()
    {
        SceneManager.LoadScene("Ejercicio_05_Pila");
    }

    // TP05
    public void CargarEjercicio07()
    {
        SceneManager.LoadScene("Ejercicio_07_Recursion");
    }

    public void CargarEjercicio08()
    {
        SceneManager.LoadScene("Ejercicio_10_MyABBTree");
    }

    public void CargarEjercicio09()
    {
        SceneManager.LoadScene("Ejercicio_11_FuncionamientoABB");
    }

    // Salir del juego
    public void Salir()
    {
        Application.Quit();
    }
}
