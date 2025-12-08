using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void StartGame()
    {
        Debug.Log("Botón presionado: cargando SampleScene"); // ? Este es el Debug.Log
        SceneManager.LoadScene("SampleScene");
    }
}
