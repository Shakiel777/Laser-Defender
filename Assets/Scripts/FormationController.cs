using UnityEngine;
using System.Collections;

public class FormationController : MonoBehaviour {

    public GameObject enemyPrefab;
    public float width = 10f;
    public float height = 5f;
    public float speed = 5f;
    public float padding = 1f;
    public float spawnDelay = 0.5f;

    private int direction = 1;
    private float boundaryRightEdge, boundaryLeftEdge;

    // Use this for initialization
    void Start ()
    {
        Camera camera = Camera.main;
        float distance = transform.position.z - camera.transform.position.z;
        boundaryLeftEdge = camera.ViewportToWorldPoint(new Vector3(0, 0, distance)).x + padding;
        boundaryRightEdge = camera.ViewportToWorldPoint(new Vector3(1, 1, distance)).x - padding;
        SpawnUntilFull();
    }

    void SpawnFormation()
    {
        foreach (Transform child in transform)
        {
            GameObject enemy = Instantiate(enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
            enemy.transform.parent = child;
        }
    }

    void SpawnUntilFull()
    {
        Transform freePosition = NextFreePosition();
        if (freePosition)
        {
            GameObject enemy = Instantiate(enemyPrefab, freePosition.position, Quaternion.identity) as GameObject;
            enemy.transform.parent = freePosition;
        }
        if (NextFreePosition())
        {
            Invoke("SpawnUntilFull", spawnDelay);
        }
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

        if (AllMembersDead())
        {
            SpawnUntilFull();
        }
    }

    bool AllMembersDead()
    {
        foreach(Transform childPositionGameObject in transform)
        {
            if (childPositionGameObject.childCount > 0)
            {
                return false;
            } 
        }
        return true;
    }

    Transform NextFreePosition()
    {
        foreach (Transform childPositionGameObject in transform)
        {
            if (childPositionGameObject.childCount == 0)
            {
                return childPositionGameObject;
            }
        }
        return null;
    }

}
