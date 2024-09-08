using UnityEngine;

public class Turret : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform gun;
    [SerializeField] private Transform firepoint;
    
    [Header("Values")]
    [SerializeField] private float rangeToTargetPlayer;
    [SerializeField] private float timeBetweenShots;
    [SerializeField] private float rotationSpeed;
    
    private float _shotCounter;
    private void Start()
    {
        _shotCounter = timeBetweenShots;
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, PlayerController.Instance.transform.position) < rangeToTargetPlayer)
        {
            gun.LookAt(PlayerController.Instance.transform.position);
            _shotCounter -= Time.deltaTime;

            if (_shotCounter <= 0)
            {
                Instantiate(bullet, firepoint.position, firepoint.rotation);
                _shotCounter = timeBetweenShots;
            }
        }
        else
        {
            _shotCounter = timeBetweenShots;
            gun.rotation = Quaternion.Lerp(gun.rotation, Quaternion.Euler(0, gun.rotation.eulerAngles.y + 10f, 0), rotationSpeed * Time.deltaTime);
            
        }
    }
}
