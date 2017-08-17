using UnityEngine;
using System.Collections;

public class EnemyFormation: MonoBehaviour {

    public GameObject Projectile;
    public float health = 150f;
    public float ProjectileSpeed = -10f;
    public float shotsPerSecond = 0.5f;
    public int scoreValue = 1;
    public AudioClip fireSound;
    public AudioClip death;
    private ScoreKeeper scoreKeeper;

    private void Start()
    {
        scoreKeeper = GameObject.Find("myScore").GetComponent<ScoreKeeper>();
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        
        Projectile missile = collider.gameObject.GetComponent<Projectile>();
        if (missile)
        {
            health -= missile.getDamage();
            missile.Hit();
            if (health <= 0)
            {
                Destroy(gameObject);
                Die();
            }

        }
    }
    void Die()
    {
        AudioSource.PlayClipAtPoint(death, transform.position);
        scoreKeeper.Score(scoreValue);
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
        AudioSource.PlayClipAtPoint(fireSound, transform.position);
    }
}
