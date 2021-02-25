using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpDropper : MonoBehaviour
{
    public GameObject HpPickupPrefab;
    public float hpPickupVelocity;

    public void OnUnitDeath()
    {
        GameObject hpRegen = Instantiate(HpPickupPrefab, transform.position, Quaternion.identity);
        hpRegen.SetActive(true);
        hpRegen.GetComponent<Rigidbody2D>().velocity = new Vector2(0, hpPickupVelocity);
    }
}
