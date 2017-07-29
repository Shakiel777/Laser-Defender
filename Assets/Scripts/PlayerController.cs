using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float speed = 15.0f;
    public float padding = 1f;
    public GameObject projectile;
    public float projectileSpeed = 5f;
    public float firingRate = 0.2f;
    public float health = 250f;
    float minX;
    float maxX;

    // Use this for initialization
    void Start ()
    {
        float distance = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, distance));
        Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3(0.95f, 0f, distance));
        minX = leftmost.x + padding;
        maxX = rightmost.x - padding;
    }
    void Fire()
    {
        Vector3 offset = new Vector3(0, 1, 0);
        GameObject beam = Instantiate(projectile, transform.position + offset, Quaternion.identity) as GameObject;
        beam.GetComponent<Rigidbody2D>().velocity = new Vector3(0, projectileSpeed, 0);

    }
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            InvokeRepeating("Fire", 0.000001f, firingRate);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            CancelInvoke("Fire");
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Debug.Log("Left Arrow Pressed");
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            Debug.Log("Right Arrow Pressed");
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        // restrict the player to the game space.
        float newX = Mathf.Clamp(transform.position.x, minX, maxX);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }
    void OnTriggerEnter2D(Collider2D collider)
    {

        Projectile missile = collider.gameObject.GetComponent<Projectile>();
        if (missile)
        {
            health -= missile.getDamage();
            missile.Hit();
            if (health <= 0) { Destroy(gameObject); }

            print("Player Hit! (trigger)");
        }
    }
}
