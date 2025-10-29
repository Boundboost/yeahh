using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private Audio_handler ah;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collided");
        if (collision.collider.CompareTag("Player"))
        {
            Die();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collided");
        if (collision.CompareTag("Player"))
        {
            ah = collision.GetComponent<Audio_handler>();
            ah.Playclip(ah.coin);
            Die();
            

        }
    }

    void Die()
    {
        Destroy(gameObject);
        
    }
}
