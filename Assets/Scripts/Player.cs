using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{

    [Header("Player")]
    [SerializeField] float playerSpeed = 10f;
    [SerializeField] int health = 200;

    [Header("Projectile")]
    [SerializeField] GameObject playerLaser;
    [SerializeField] float projectileSpeed=10f;
    [SerializeField] float projectileFiringPeriod = 0.1f;

    Coroutine firingCoroutine;

    float xMin;
    float xMax;

    float yMin;
    float yMax;
    // Start is called before the first frame update
    void Start()
    {

        SetUpMoveBoundaries();
       
    }

   

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
    }

    private void Fire()
    {
        
        if (Input.GetButtonDown("Fire1"))
        {
            firingCoroutine =StartCoroutine(FireLaserRoutine());
            
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }
        
    }
   private IEnumerator FireLaserRoutine()
    {
        while (true)
        {
            GameObject laser =
                Instantiate(playerLaser,
                transform.position,
                Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            yield return new WaitForSeconds(projectileFiringPeriod);
        }
    }
    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal")*Time.deltaTime * playerSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * playerSpeed;
        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin+0.5f, xMax-0.5f);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin+0.5f, yMax-0.5f);
        transform.position = new Vector2(newXPos, newYPos);
        
    }
    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.gameObject.GetComponent<DamageDealer>();

        if (!damageDealer) { return; }
        ProcessHit(collision, damageDealer);
        
    }

    private void ProcessHit(Collider2D collision, DamageDealer damageDealer)
    {
        
        health -= damageDealer.GetHitOnPlayer();
        damageDealer.Hit();
        if(health <= 0)
        {
            Destroy(gameObject);
            FindObjectOfType<Level>().LoadGameOver();
        }
    }
}
