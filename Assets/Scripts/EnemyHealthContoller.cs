using UnityEngine;

public class EnemyHealthContoller : MonoBehaviour
{
    [SerializeField] private int currentHealth;

    public void DamageEnemy(int damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            AudioManager.Instance.PlaySFX(2);
        }
    }
}
