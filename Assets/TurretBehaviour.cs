using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBehaviour : MonoBehaviour {

    // Time of last shot
    private float lastShot;
    public float initialPeaceTime;
    public float fireDelay;
    public GameObject player;

    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletVelocity;
    private float angularVelocityMaximum = 10f;

    // Use this for initialization
    void Start () {
        lastShot = initialPeaceTime;
    }
	
	// Update is called once per frame
	void Update () {
        // Track
        Rigidbody rb = player.GetComponent<Rigidbody>();
        Vector3 dirPlayer = rb.position - bulletSpawn.position;
        Vector3 velPlayer = rb.velocity;
        float v = Vector3.Magnitude(velPlayer);
        float r = Vector3.Magnitude(dirPlayer);
        float t;
        // First-order tracking
        if (v == 0 || r == 0) {
            t = 0;
        }
        else {
            float psi = 180f - Vector3.Dot(dirPlayer, velPlayer) / r / v;
            float a = r * Mathf.Cos(psi);
            float b = bulletVelocity * bulletVelocity - v * v;
            if (a * a + b < 0 || b == 0) {
                t = 0;
            }
            else {
                t = r / b * (-a + Mathf.Sqrt(a * a + b));
            }
        }
        //Debug.Log("t" + t.ToString());

        // Aiming vector
        Vector3 dirAiming = dirPlayer + t * velPlayer;

        // Rotate turret
        transform.eulerAngles = new Vector3(0f, -135f + 180f / Mathf.PI * Mathf.Atan2(dirAiming[0], dirAiming[2]), 0f);

        // Shot
        if (Time.time - lastShot >= fireDelay / Mathf.Pow(Mathf.Max(1f,player.GetComponent<PlayerController>().count),2f) ) {
            lastShot = Time.time;
            ShootBullet(dirAiming);
        }
        //Debug.Log(dirPlayer);
	}

    void ShootBullet (Vector3 direction) {
        // Create the Bullet from the Bullet Prefab
        var bullet = (GameObject)Instantiate(
            bulletPrefab,
            bulletSpawn.position,
            bulletSpawn.rotation);

        // Add velocity to the bullet
        bullet.GetComponent<Rigidbody>().velocity = bulletVelocity * direction / Vector3.Magnitude(direction);
        bullet.GetComponent<Rigidbody>().angularVelocity = new Vector3(Random.Range(0f, angularVelocityMaximum), Random.Range(0f, angularVelocityMaximum), Random.Range(0f, angularVelocityMaximum));
    }
}
