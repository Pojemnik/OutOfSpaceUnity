using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPortalController : MonoBehaviour
{
    public float descendSpeed;
    public GameObject projectilePrefab;
    public float projectileSpeed;
    public GameObject target;
    public GameObject levelManegerObject;

    private LevelManager levelManager;
    private Transform targetTransform;
    private Rigidbody2D rb2d;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        targetTransform = target.transform;
        levelManager = levelManegerObject.GetComponent<LevelManager>();
    }

    private void FixedUpdate()
    {
        if(transform.position.y <= targetTransform.position.y)
        {
            GameObject newProjectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
            newProjectile.SetActive(true);
            Rigidbody2D projectileRB = newProjectile.GetComponent<Rigidbody2D>();
            projectileRB.velocity = new Vector2(projectileSpeed, 0);
            levelManager.changeLevel.AddListener(newProjectile.GetComponent<Projectile>().TargetHit);
            Destroy(gameObject);
        }
        else
        {
            Vector2 newPosition = rb2d.position;
            newPosition.y += descendSpeed * Time.fixedDeltaTime;
            rb2d.MovePosition(newPosition);
        }
    }
}
