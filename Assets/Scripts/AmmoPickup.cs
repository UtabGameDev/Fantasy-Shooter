using System;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{

    private bool _collected;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !_collected)
        {
            PlayerController.Instance.activeGun.GetAmmo();
            Destroy(gameObject);
            _collected = true;
            
            AudioManager.Instance.PlaySFX(3);
        }
    }
}
