using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("References")]
    public GameObject bullet;
    
    [Header("Values")]
    public bool canAutoFire;
    public float fireRate;
    public int currentAmmo;
    
    
    [HideInInspector] public float fireCounter;
    void Start()
    {
        
    }
    
    void Update()
    {
        if (fireCounter > 0)
        {
            fireCounter -= Time.deltaTime;
        }
    }
}
