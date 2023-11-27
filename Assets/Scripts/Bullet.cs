using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    
    private Rigidbody2D rb;
    public float forceMagnitude = 10.0f;
    public GameObject AsteroidManager;
    
    // Start is called before the first frame update
    
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.velocity = transform.up * forceMagnitude;
        Invoke("DestroyBullet", 5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Shot ()
    {
        rb.AddForce(transform.up * forceMagnitude);
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Find the GameManager in the scene
        GameManager gameManager = FindObjectOfType<GameManager>();
        if (collision.gameObject.CompareTag("SmallAsteroid"))
        {
            gameManager.AddScore();
            Destroy(collision.gameObject); // Destruir l'asteroide petit
            Destroy(gameObject); // Destruir la bala
        }
        if (collision.gameObject.CompareTag("MediumAsteroid"))
        {
            gameManager.AddScore();
            Destroy(collision.gameObject); // Destruir l'asteroide mitja
            Destroy(gameObject); // Destruir la bala
        }
        if (collision.gameObject.CompareTag("LargeAsteroid"))
        {
            gameManager.AddScore();
            Destroy(collision.gameObject); // Destruir l'asteroide gran
            Destroy(gameObject); // Destruir la bala
        }
        Destroy(gameObject);
    }
    
    void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
