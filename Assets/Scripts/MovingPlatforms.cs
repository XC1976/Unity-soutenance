using UnityEngine;

public class MovingPlatforms : MonoBehaviour
{
    // How fast the platform moves (units / second)
    public float speed = 2f;
    // Array that is populated with points (Empty Objects positions) in the editor
    public Transform[] points;

    // Index for the current target point, default = 0
    private int i;
    
    void Start()
    {
        // Place the first target point at 0 on first frame
        transform.position = points[0].position;
    }

    void Update()
    {
        // Moves the plateform towards points[i] at the given speed
        // speed * Time.deltaTime to make the movement frame independent
        transform.position = Vector2.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);
        
        // If the distance "plateform current position" <-> points[i] < 0.01f (close enough)
        // It will increment i++ --> points[i] becomes points[i]++
        if (Vector2.Distance(transform.position, points[i].position) < 0.01f)
        {
            i++;
            // If i becomes out of range, it will loop back to 0
            // i.e. go in the other direction to loop again
            if (i == points.Length)
            {
                i = 0;
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If it collides with Player
        if (collision.gameObject.tag == "Player")
        {
            // collision.transform ==> Player object transform
            // .SetParent(transform) ==> Makes the player transform a child
            // of the plateform's Transform (current object)
            // Result ==> The player moves with the plateform
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // Cancel .SetParent(transform)
            // The player has independent world position (default)
            collision.transform.SetParent(null);
        }
    }
}
 