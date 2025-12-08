using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelOnExit : MonoBehaviour
{
    public string nextScene = "Level2";

    void Update()
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        if (pos.x < 0 || pos.x > 1 || pos.y < 0 || pos.y > 1)
        {
            SceneManager.LoadScene(nextScene);
        }
    }
}
