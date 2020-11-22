using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Set In Inspector")]
    public float hp = 100;

    [Header("Set Dynamically")]
    private BoxCollider col;
    private Rigidbody2D rb;

    private void Start()
    {
        col = GetComponent<BoxCollider>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(float damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
