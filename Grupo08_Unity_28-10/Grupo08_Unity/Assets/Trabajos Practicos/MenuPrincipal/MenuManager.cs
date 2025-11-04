using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuDeEscenas : MonoBehaviour
{
    public void CargarMenu()
    {
        SceneManager.LoadScene("MenuDeEscenas");
    }

    public void CargarEjercicio01()
    {
        SceneManager.LoadScene("Ejercicio_01_SimpleList");
    }

    public void CargarEjercicio02()
    {
        SceneManager.LoadScene("Ejercicio02_MyList");
    }

    public void CargarEjercicio03()
    {
        SceneManager.LoadScene("Ejercicio_03_Store");
    }

    public void CargarEjercicio04()
    {
        SceneManager.LoadScene("Ejercicio_04_Cola");
    }

    public void CargarEjercicio05()
    {
        SceneManager.LoadScene("Ejercicio_05_Pila");
    }

    public void CargarEjercicio07()
    {
        SceneManager.LoadScene("Ejercicio_07_Recursion");
    }

    public void CargarEjercicio10()
    {
        SceneManager.LoadScene("Ejercicio_10_MyABBTree");
    }

    public void CargarEjercicio11()
    {
        SceneManager.LoadScene("Ejercicio_11_FuncionamientoABB");
    }

    public void CargarEjercicio13()
    {
        SceneManager.LoadScene("Ejercicio_13_HighScore");
    }

    public void CargarEjercicio17()
    {
        SceneManager.LoadScene("Ejercicio17_MapaEspacial");
    }

    public void Salir()
    {
        Application.Quit();
    }
}
