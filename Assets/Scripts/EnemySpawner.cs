using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

    public GameObject enemyPrefab;
    public float width = 10f;
    public float height = 5f;
    public float speed = 5f;
    public float padding = 1f;

    private int direction = 1;
    private float boundaryRightEdge, boundaryLeftEdge;

    // Use this for initialization
    void Start ()
    {
        Camera camera = Camera.main;
        float distance = transform.position.z - camera.transform.position.z;
        boundaryLeftEdge = camera.ViewportToWorldPoint(new Vector3(0, 0, distance)).x + padding;
        boundaryRightEdge = camera.ViewportToWorldPoint(new Vector3(1, 1, distance)).x - padding;

        foreach (Transform child in transform)
        {
            GameObject enemy = Instantiate(enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
            enemy.transform.parent = child;
        }
            //float distanceToCamera = transform.position.z - Camera.main.transform.position.z;
            //Vector3 leftBoundry = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distanceToCamera));
            //Vector3 rightBoundry = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distanceToCamera));
            //xmax = rightBoundry.x;
            //xmin = leftBoundry.x;

            //foreach (Transform child in transform)
            //{
            //    GameObject enemy = Instantiate(enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
            //    enemy.transform.parent = child;
            //}
        }
    public void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height, 0f));
    }

    // Update is called once per frame
    void Update ()
    {
        float formationRightEdge = transform.position.x + 0.5f * width;

        float formationLeftEdge = transform.position.x - 0.5f * width;

        if (formationRightEdge > boundaryRightEdge)
        {

            direction = -1;

        }

        if (formationLeftEdge < boundaryLeftEdge)
        {

            direction = 1;

        }

        transform.position += new Vector3(direction * speed * Time.deltaTime, 0, 0);
        //if (movingRight)
        //   {
        //       transform.position += Vector3.right * speed * Time.deltaTime;
        //   }
        //   else
        //   {
        //       transform.position += Vector3.left * speed * Time.deltaTime;
        //   }

        //   float rightEdgeofFormation = transform.position.x + (0.5f * width);
        //   float leftEdgeofFormation = transform.position.x - (0.5f * width);
        //   if(leftEdgeofFormation < xmin || rightEdgeofFormation > xmax)
        //   {
        //       movingRight = !movingRight;
        //   }
    }
}
