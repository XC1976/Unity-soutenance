using UnityEngine;

public class RestartGame : MonoBehaviour
{
    public void LoadCurrentScene() {
        // Reload level 1
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level1");
        // Unfreeze the game
        Time.timeScale = 1;
    }
}
