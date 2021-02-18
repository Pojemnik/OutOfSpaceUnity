using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float projectileSpeed;
    public Vector2 projectileOffset;
    public AudioClip shootSound;
    public LevelManager levelManager;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Shoot()
    {
        Vector3 projectilePosition = transform.position + new Vector3(projectileOffset.x, projectileOffset.y);
        GameObject newProjectile = Instantiate(projectilePrefab, projectilePosition, transform.rotation);
        newProjectile.SetActive(true);
        Rigidbody2D projectileRB = newProjectile.GetComponent<Rigidbody2D>();
        projectileRB.velocity = new Vector2(0, projectileSpeed);
        audioSource.PlayOneShot(shootSound);
        levelManager.changeLevel.AddListener(newProjectile.GetComponent<Projectile>().TargetHit);
    }
}
