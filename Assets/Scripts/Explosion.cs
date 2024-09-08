using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private int damage = 25;
    [SerializeField] private bool damageEnemy;
    [SerializeField] private bool damagePlayer;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" && damageEnemy)
        {
            if(other.gameObject.GetComponent<EnemyHealthContoller>() != null)
                other.gameObject.GetComponent<EnemyHealthContoller>().DamageEnemy(damage);
            else
            {
                Destroy(other.gameObject);
            }
        }

        if (other.gameObject.tag == "Player" && damagePlayer)
        {
            PlayerHealthController.Instance.DamagePlayer(damage);
        }
    }
}
