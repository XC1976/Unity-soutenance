using UnityEngine;

public class RestartGame : MonoBehaviour
{
    // This function does not need a trigger in script because
    // it is used with a onclick() on a button
    public void LoadCurrentScene() {
        // Reload level 1
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level1");
        // Unfreeze the game
        Time.timeScale = 1;
    }
}
