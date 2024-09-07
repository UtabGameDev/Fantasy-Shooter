using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public string theGun;
    private bool _collected;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !_collected)
        {
            PlayerController.Instance.AddGun(theGun);
            Destroy(gameObject);
            _collected = true;
            
            AudioManager.Instance.PlaySFX(4);
        }
    }
}
