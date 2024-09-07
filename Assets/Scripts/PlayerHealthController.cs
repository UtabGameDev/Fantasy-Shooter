using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController Instance;
    [SerializeField] private int maxHealth;
    [SerializeField] private float invincibleLenght;
    
    
    private int _currentHealth;
    private float _invincibleCounter;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        _currentHealth = maxHealth;
        
        UIController.Instance.slider.maxValue = maxHealth;
        UIController.Instance.slider.value = _currentHealth;
        UIController.Instance.healthText.text = "HEALTH: " + _currentHealth + "/" + maxHealth;
    }
    void Update()
    {
        if (_invincibleCounter > 0)
        {
            _invincibleCounter -= Time.deltaTime;
        }
    }

    public void DamagePlayer(int _damageAmount)
    {

        if (_invincibleCounter <= 0)
        {
            AudioManager.Instance.PlaySFX(7);
            
            _currentHealth -= _damageAmount;
            
            UIController.Instance.ShowDamage();
            
            if (_currentHealth <= 0)
            {
                gameObject.SetActive(false);
                _currentHealth = 0;
                GameManager.Instance.PlayerDied();
                
                AudioManager.Instance.StopBgMusic();
                AudioManager.Instance.PlaySFX(6);
                AudioManager.Instance.StopSFX(7);
            }

            _invincibleCounter = invincibleLenght;
            
            
            UIController.Instance.slider.value = _currentHealth;
            UIController.Instance.healthText.text = "HEALTH: " + _currentHealth + "/" + maxHealth;
        }
    }

    public void HealPlayer(int _healAmount)
    {
        _currentHealth += _healAmount;
        if (_currentHealth > maxHealth)
        {
            _currentHealth = maxHealth;
        }
        
        UIController.Instance.slider.value = _currentHealth;
        UIController.Instance.healthText.text = "HEALTH: " + _currentHealth + "/" + maxHealth;
        
    }
}
