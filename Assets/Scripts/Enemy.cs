using UnityEngine;

public class Enemy : MonoBehaviour
{
    // How fast the platform moves (units / second)
    public float speed = 2f;
    // Array that is populated with points (Empty Objects positions) in the editor
    public Transform[] points;

    // Index for the current target point, default = 0
    private int i;
    private SpriteRenderer spriteRenderer;
    
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Moves the enemy towards points[i] at the given speed
        // speed * Time.deltaTime to make the movement frame independent
        transform.position = Vector2.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);

        // If the distance "plateform current position" <-> points[i] < 0.01f (close enough)
        // It will increment i++ --> points[i] becomes points[i]++
        if (Vector2.Distance(transform.position, points[i].position) < 0.25f)
        {
            i++;
            // If i becomes out of range, it will loop back to 0
            // i.e. go in the other direction to loop again
            if (i == points.Length)
            {
                i = 0;
            }
        }

        // Logic to make the sprite of the enemy look on the left if walking left
        // right if walking right
        spriteRenderer.flipX = (transform.position.x - points[i].position.x) < 0f;
    }
}
