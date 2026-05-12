using UnityEngine;

public class Coin : MonoBehaviour
{
    // If the coin enters a collision with the player,
    // disappears and adds 1 to the public property coins in Player
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag == "Player") {
            Player player = collision.gameObject.GetComponent<Player>();
            player.coins += 1;
            Destroy(gameObject);
        }
    }
}
