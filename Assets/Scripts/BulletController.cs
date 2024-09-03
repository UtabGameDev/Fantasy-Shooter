using System;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private GameObject impactEffect;
    
    
    [Header("Values")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float lifeTime;
    [SerializeField] private int damageAmount;
    [SerializeField] private bool damageEnemy;
    [SerializeField] private bool damagePlayer;
    
    void Start()
    {
        
    }

    void Update()
    {
        rb.velocity = transform.forward * moveSpeed;
        
        lifeTime -= Time.deltaTime;
        if(lifeTime <= 0)
            Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" && damageEnemy)
        {
            if(other.gameObject.GetComponent<EnemyHealthContoller>() != null)
                other.gameObject.GetComponent<EnemyHealthContoller>().DamageEnemy(damageAmount );
            else
            {
                Destroy(other.gameObject);
            }
        }

        if (other.gameObject.tag == "Player" && damagePlayer)
        {
            PlayerHealthController.Instance.DamagePlayer(damageAmount);
        }
        
        Destroy(gameObject);
        Instantiate(impactEffect, transform.position + (transform.forward * (-moveSpeed  * Time.deltaTime)), transform.rotation);
    }
}
