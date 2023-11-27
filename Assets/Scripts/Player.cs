using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float forceMagnitude = 6.0f;
    public float rotateSpeed = 1.8f;
    private bool thrusting = false;
    private bool shoot = false;
    private float torque = 0.0f;
    //private float cooldown = 0.0f;
    private Rigidbody2D rb;
    public GameObject bullet;
    public GameManager gm;
    public GameObject Tail;
    public AudioSource audioSource1;
    public AudioClip tailSound;
    

    public GameObject firePoint;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
       thrusting = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
       
       if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
       {
           torque = 1.0f;
       }

       else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
       {
           torque = -1.0f;
       }
       else
       {
           torque = 0.0f;
       }

       if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0))
       {
           shoot = true;
       }

    }

    void FixedUpdate()
    {
        if (thrusting)
        {
          rb.AddForce(transform.up * forceMagnitude);
          GameObject tail = Instantiate(Tail, transform.position, Quaternion.identity);
          // Obtener la rotación actual del jugador y aplicarla a las partículas de la cola
          Quaternion rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + 180f);
          tail.transform.rotation = rotation;
          Destroy(tail, tail.GetComponent<ParticleSystem>().main.duration); // Destruye la cola después de su duración
          audioSource1.clip = tailSound;
          audioSource1.Play();
        }
        
        rb.AddTorque(torque * rotateSpeed);

        
        if (shoot)
        {
            Shoot();
        }
    }
    void Shoot()
    {
        // Instantiate a bullet at the player's position and rotation.
        GameObject newBullet = Instantiate(bullet, transform.position, transform.rotation, transform);
        shoot = false; // Reset the shoot flag.
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        gm.LiveControl();
        Destroy(other.gameObject);
    }
}
