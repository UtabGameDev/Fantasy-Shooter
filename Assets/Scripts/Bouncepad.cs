using System;
using UnityEngine;

public class Bouncepad : MonoBehaviour
{
    [SerializeField] private float bounceForce;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerController.Instance.Bounce(bounceForce);
            AudioManager.Instance.PlaySFX(0);
        }
    }
}
