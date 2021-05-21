using UnityEngine;

public class PreloadScript : MonoBehaviour
{
    private void Awake()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene_WindowsTesting");
    }
}
