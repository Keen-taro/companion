using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnProjectile : MonoBehaviour
{
    public GameObject projectilePrefab;
    public int projectilesPerShot = 3; // Number of projectiles to spawn per shot
    public float fireRate = 3f;
    public float spreadAngle = 30f; // Angle of spread between projectiles
    private float nextFireTime;
    private bool isSpawningEnabled = false;

    void OnEnable()
    {
        isSpawningEnabled = true;
    }

    void OnDisable()
    {
        isSpawningEnabled = false;
    }

    void Update()
    {
        if (isSpawningEnabled && Time.time > nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            FireProjectiles();
        }
    }

    void FireProjectiles()
    {
        for (int i = 0; i < projectilesPerShot; i++)
        {
            // Calculate spawn position with spread
            float angleOffset = (i - (projectilesPerShot - 1) / 2f) * (spreadAngle / (projectilesPerShot - 1));
            Quaternion rotationOffset = Quaternion.Euler(0, 0, angleOffset);

            Vector3 spawnPosition = transform.position + rotationOffset * transform.right * 1f;

            // Instantiate the projectile
            GameObject projectile = Instantiate(projectilePrefab, spawnPosition, transform.rotation * rotationOffset);
        }
    }
}
