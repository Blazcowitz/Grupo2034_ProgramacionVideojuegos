using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitToFinalScene : MonoBehaviour
{
    public string finalSceneName = "FinalScene";

    void Update()
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        if (pos.x < 0 || pos.x > 1 || pos.y < 0 || pos.y > 1)
        {
            SceneManager.LoadScene(finalSceneName);
        }
    }
}
