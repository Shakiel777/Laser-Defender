using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float speed = 15.0f;
    public float padding = 1f;
    public GameObject projectile;
    public float projectileSpeed = 5f;
    public float firingRate = 0.2f;
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
            GameObject beam = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
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
            // transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            Debug.Log("Right Arrow Pressed");
            // transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        // restrict the player to the game space.
        float newX = Mathf.Clamp(transform.position.x, minX, maxX);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }


    //Vector3 paddlePos = new Vector3(0.5f, this.transform.position.y, 0f);
    //float mousePosInBlocks = Input.mousePosition.x / Screen.width * 16;
    //paddlePos.x = Mathf.Clamp(mousePosInBlocks, minX, maxX);
    //this.transform.position = paddlePos;
}
