using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] float health = 100;
    [SerializeField] float shotCounter;
    [SerializeField] GameObject playerLaser;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3f;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] GameObject explosion;
    [SerializeField] AudioClip laserSound;
    [SerializeField] AudioClip deathSFX;
    [SerializeField] int scoreValue = 50;
    [Range(0f, 1f)] [SerializeField] float laserVolume;
    [Range(0f, 1f)] [SerializeField] float explosionVolume;

    // Start is called before the first frame update
    void Start()
    {
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }

    // Update is called once per frame
    void Update()
    {
        CountDownAndShoot();
    }

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if(shotCounter <= 0f)
        {
            Fire();
            shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    private void Fire()
    {
        GameObject laser = Instantiate(playerLaser, transform.position, Quaternion.identity) as GameObject;
        AudioSource.PlayClipAtPoint(laserSound, Camera.main.transform.position, laserVolume);
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();

        if (!damageDealer) { return; }
            ProcessHit(other, damageDealer);
        
    }

    private void ProcessHit(Collider2D other, DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();

        damageDealer.Hit();
        if (health <= 0)
        {
            AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, explosionVolume);
            FindObjectOfType<GameSession>().AddToScore(scoreValue);
            Destroy(gameObject);
            GameObject VFXexplosion = Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(VFXexplosion, .5f);
        }
    }
}
