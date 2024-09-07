using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("References")]
    public GameObject bullet;
    public Transform firepoint;
    public float zoomAmount;
    
    [Header("Values")]
    public bool canAutoFire;
    public float fireRate;
    public int currentAmmo;
    public int pickupAmount;
    public string gunName;
    
     
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

    public void GetAmmo()
    {
        currentAmmo += pickupAmount;
        UIController.Instance.ammoText.text = "AMMO: " + currentAmmo;
    }
}
