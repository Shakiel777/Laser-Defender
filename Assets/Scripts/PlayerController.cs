using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float speed = 15.0f;
    public float padding = 1f;
    public GameObject projectile;
    public float projectileSpeed = 5f;
    public float firingRate = 0.2f;
    public float health = 250f;
    public int livesValue = 1;
    public AudioClip fireSound;
    public AudioClip death;
    private Lives lives;
    float minX = 5;
    float maxX = -5;

    

    // Use this for initialization
    void Start ()
    {
        float distance = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, distance));
        Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1f, 1f, distance));
        minX = leftmost.x + padding;
        maxX = rightmost.x - padding;
        lives = GameObject.Find("myShip").GetComponent<Lives>();
    }
    void Fire()
    {
        // Vector3 offset = new Vector3(0, 1, 0);
        GameObject beam = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
        beam.GetComponent<Rigidbody2D>().velocity = new Vector3(0, projectileSpeed, 0);
        AudioSource.PlayClipAtPoint(fireSound, transform.position);
    }
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            InvokeRepeating("Fire", 0.0001f, firingRate);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            CancelInvoke("Fire");
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
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
            if (health <= 0) { Die(); }
        }
    }
    void Die()
    {
        // Destroy(gameObject);
        
        AudioSource.PlayClipAtPoint(death, transform.position);
        lives.Ships(livesValue);
        StartCoroutine(Dead());
    }
    IEnumerator Dead()
    {
        Debug.Log("dead");
        GetComponent<Renderer>().enabled = false;
        yield return new WaitForSeconds(5);
        Debug.Log("respawn");
        GetComponent<Renderer>().enabled = true;
    }
}
