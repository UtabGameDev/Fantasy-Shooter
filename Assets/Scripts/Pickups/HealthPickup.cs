using System;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] private int healAmount;

    private bool _isCollected;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !_isCollected)
        {
            PlayerHealthController.Instance.HealPlayer(healAmount);
            Destroy(gameObject);
            
            AudioManager.Instance.PlaySFX(5);
        }
    }
}
 