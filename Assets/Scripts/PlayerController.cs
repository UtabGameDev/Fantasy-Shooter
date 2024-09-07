using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    
    [Header("References")]
    [SerializeField] private CharacterController characterController;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Animator anim;
    [SerializeField] private Transform adsPoint;
    [SerializeField] private Transform gunHolder;
    [SerializeField] private GameObject muzzleFlash;
    
    public Gun activeGun;
    public int currentGun;
    public List<Gun> allGuns = new List<Gun>();
    public List<Gun> unlockableGuns = new List<Gun>();
    
    [Header("Layers")]
    [SerializeField] private LayerMask whatIsGround;
    
    [Header("Values")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float gravityModifier;
    [SerializeField] private float mouseSensivity;
    [SerializeField] private float jumpPower;
    [SerializeField] private float runSpeed = 12f;
    [SerializeField] private float adsSpeed = 2f;
    [SerializeField] private bool invertX;
    [SerializeField] private bool invertY;

    private Vector3 _moveInput;
    private bool _canJump;
    private bool _canDoubleJump;
    private Vector3 _gunStartPosition;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        activeGun = allGuns[currentGun];
        activeGun.gameObject.SetActive(true);
        
        UIController.Instance.ammoText.text = "AMMO: " + activeGun.currentAmmo;

        _gunStartPosition = gunHolder.localPosition;
    }

    private void Update()
    {
        if (!UIController.Instance.pauseScreen.activeInHierarchy)
        {
            Movement();
        }
    }

    private void Movement()
    {
        // Store y velocity.
        var yStore = _moveInput.y;
        
        var verticalMove = transform.forward * Input.GetAxis("Vertical");
        var horizontalMove = transform.right * Input.GetAxis("Horizontal");

        _moveInput =  horizontalMove + verticalMove ;
        _moveInput.Normalize();

        HandleSpeed();

        _moveInput.y = yStore;

        _moveInput.y += Physics.gravity.y * gravityModifier * Time.deltaTime;
        
        if (characterController.isGrounded)
            _moveInput.y = Physics.gravity.y * gravityModifier * Time.deltaTime;
        
        HandleJumping();
        
        characterController.Move(_moveInput * Time.deltaTime);

        var mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")) * mouseSensivity;
        if (invertX)
            mouseInput.x = -mouseInput.x;
        if (invertY)
            mouseInput.y = -mouseInput.y;
        
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + mouseInput.x, transform.rotation.eulerAngles.z);
        cameraTransform.rotation = Quaternion.Euler(cameraTransform.rotation.eulerAngles + new Vector3(-mouseInput.y, 0f, 0f));

        muzzleFlash.SetActive(false);

        Shooting();

        if (Input.GetMouseButton(0) && activeGun.canAutoFire) 
        {
            if (activeGun.fireCounter <= 0)
            {
                FireShot();
            }
        }
        
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SwitchGun();
        }

        if (Input.GetMouseButtonDown(1))
        {
            CameraController.Instance.ZoomIn(activeGun.zoomAmount);
        }

        if (Input.GetMouseButton(1))
        {
            gunHolder.position = Vector3.MoveTowards(gunHolder.position, adsPoint.position, adsSpeed * Time.deltaTime);
        }
        else
        {
            gunHolder.localPosition = Vector3.MoveTowards(gunHolder.localPosition, _gunStartPosition, adsSpeed * Time.deltaTime);
        }
        
        if (Input.GetMouseButtonUp(1))
        {
            CameraController.Instance.ZoomOut();
        }
        
        anim.SetFloat("MoveSpeed", _moveInput.magnitude);
        anim.SetBool("onGround", _canJump);
    }

    private void Shooting()
    {
        //single shot
        if (Input.GetMouseButtonDown(0) && activeGun.fireCounter <= 0)
        {
            RaycastHit hit;
            if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, 50f))
            {
                if(Vector3.Distance(cameraTransform.position, hit.point) > 2f)
                    firePoint.LookAt(hit.point);
            }
            else
            {
                firePoint.LookAt(cameraTransform.position + (cameraTransform.forward * 30f));
            }
            
            FireShot();
        }
    }

    public void FireShot()
    {
        if (activeGun.currentAmmo > 0)
        {
            activeGun.currentAmmo--;
            Instantiate(activeGun.bullet, firePoint.position, firePoint.rotation);
            activeGun.fireCounter = activeGun.fireRate;
            
            UIController.Instance.ammoText.text = "AMMO: " + activeGun.currentAmmo;
            
            muzzleFlash.SetActive(true);
        }
    }

    private void HandleSpeed()
    {
        if (Input.GetKey(KeyCode.LeftShift))
            _moveInput *= runSpeed;
        else
            _moveInput *= moveSpeed;
    }

    private void HandleJumping()
    {
        _canJump = Physics.OverlapSphere(groundCheck.position, .25f, whatIsGround).Length > 0;
    
        if (_canJump && _moveInput.y < 0)
        {
            _canDoubleJump = true;
        }

        // repeat shots 
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_canJump)
            {
                _moveInput.y = jumpPower;
                _canDoubleJump = true;
                AudioManager.Instance.PlaySFX(8);
            }
            else if (_canDoubleJump)
            {
                _moveInput.y = jumpPower;
                _canDoubleJump = false;
                AudioManager.Instance.PlaySFX(8);
            }
        }
    }

    private void SwitchGun()
    {
        activeGun.gameObject.SetActive(false);
        currentGun++;

        if (currentGun >= allGuns.Count)
            currentGun = 0;

        activeGun = allGuns[currentGun];
        activeGun.gameObject.SetActive(true);
        
        UIController.Instance.ammoText.text = "AMMO: " + activeGun.currentAmmo;

        firePoint.position = activeGun.firepoint.position;
    }

    public void AddGun(string gunToAdd)
    {
        bool gunUnlocked = false;

        if (unlockableGuns.Count > 0)
        {
            for (int i = 0; i < unlockableGuns.Count; i++)
            {
                if (unlockableGuns[i].gunName == gunToAdd)
                {
                    gunUnlocked = true;
                    
                    allGuns.Add(unlockableGuns[i]);
                    
                    unlockableGuns.RemoveAt(i);

                    i = unlockableGuns.Count;
                }
            }
        } 
        if (gunUnlocked)
        {
            currentGun = allGuns.Count - 2;
            SwitchGun();
        }
    }
}
