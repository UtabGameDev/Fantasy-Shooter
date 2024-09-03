using System;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] private int healAmount;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerHealthController.Instance.HealPlayer(healAmount);
            Destroy(gameObject);
        }
    }
}
 