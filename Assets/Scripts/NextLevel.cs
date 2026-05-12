using UnityEngine;

public class NextLevel : MonoBehaviour
{
    // This is used to dynamically put the name of the nevel level that is loaded with the button
    public string nextLevelName;
    
    public void LoadNextLevel() {
        // Reload level 1
        UnityEngine.SceneManagement.SceneManager.LoadScene(nextLevelName);
        // Unfreeze the game
        Time.timeScale = 1;
    }
}
