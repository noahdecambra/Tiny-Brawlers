using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDamge : MonoBehaviour
{
    public int damage;
    
    void start()
    {
        
    }

    public Transform explosionPrefab;
    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint contact = collision.contacts[0];

        // Rotate the object so that the y-axis faces along the normal of the surface
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 pos = contact.point;
        Instantiate(explosionPrefab, pos, rot);
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<CharHealth>().TakeDamage(damage);
        }
        else if (other.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<CharHealth>().TakeDamage(damage);
        }
        else
        {
            other.gameObject.GetComponent<Destructible>().TakeDamage(damage);
        }
    }
}
