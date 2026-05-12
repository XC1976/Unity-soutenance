using UnityEngine;

public class Coin : MonoBehaviour
{
    // Audio clip used when taking a coin
    public AudioClip coinAudio;

    // If the coin enters a collision with the player,
    // disappears and adds 1 to the public property coins in Player
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Player player = collision.gameObject.GetComponent<Player>();
            player.coins += 1;
            // PlaySFX() is a public function in Player, 0.4f is the volume 0 <-> 1
            player.PlaySFX(coinAudio, 0.4f);
            Destroy(gameObject);
        }
    }
}
