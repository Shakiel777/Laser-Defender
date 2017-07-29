using UnityEngine;
using System.Collections;

public class EnemyDamage : MonoBehaviour {

    public GameObject Projectile;
    public float health = 150f;
    public float ProjectileSpeed = -10f;
    public float shotsPerSecond = 0.5f;


    void OnTriggerEnter2D(Collider2D collider)
    {
        
        Projectile missile = collider.gameObject.GetComponent<Projectile>();
        if (missile)
        {
            health -= missile.getDamage();
            missile.Hit();
            if (health <= 0) { Destroy(gameObject); }

            print("Enemy Hit! (trigger)");
        }
    }
    void Update()
    {
        float probability = Time.deltaTime * shotsPerSecond;
        if (Random.value < probability)
        {
            Fire();
        }
    }

    void Fire()
    {
        Vector3 startPosition = transform.position + new Vector3(0, -1, 0);
        GameObject missile = Instantiate(Projectile, startPosition, Quaternion.identity) as GameObject;
        missile.GetComponent<Rigidbody2D>().velocity = new Vector3(0, ProjectileSpeed, 0);
    }
}
