using UnityEngine;

public class FinishFlag : MonoBehaviour
{
    public GameObject winUI;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            // Freeze the game
            Time.timeScale = 0;
            winUI.SetActive(true);
        }
    }
}
