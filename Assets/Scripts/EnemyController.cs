using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Animator anim;
    
    [Header("Values")]
    [SerializeField] private bool chasing;
    [SerializeField] private float distanceToChase = 10f;
    [SerializeField] private float distanceToLose = 15f;
    [SerializeField] private float keepChasingTime = 5f;
    [SerializeField] private float distanceToStop = 2f;
    [SerializeField] private float fireRate;
    [SerializeField] private float waitBetweenShots = 2f;
    [SerializeField] private float shotWaitCounter;
    [SerializeField] private float timeToShoot = 1f;
    [SerializeField] private float shootTimeCounter;

    private Vector3 _startPoint;
    private float _chaseCounter;
    private float _fireCount;
    
    private void Start()
    {
        _startPoint = transform.position;
        
        shootTimeCounter = timeToShoot;
        shotWaitCounter = waitBetweenShots;
    }

   private void Update()
    {
        Vector3 targetPoint = PlayerController.Instance.transform.position;
        targetPoint.y = transform.position.y;
        
        if (!chasing)
        {
            if (Vector3.Distance(transform.position, targetPoint) < distanceToChase)
            {
                chasing = true;
                shootTimeCounter = timeToShoot;
                shotWaitCounter = waitBetweenShots;
            }

            if (_chaseCounter > 0)
            {
                _chaseCounter -= Time.deltaTime;
                if (_chaseCounter <= 0)
                    agent.destination = _startPoint;
            }

            if (agent.remainingDistance < .25f)
            {
                anim.SetBool("isMoving", false);
            }
            else
            {
                anim.SetBool("isMoving", true);
            }
        }
        else
        {
            agent.destination = Vector3.Distance(transform.position, targetPoint) > distanceToStop ? targetPoint : transform.position;

            if (Vector3.Distance(transform.position, targetPoint) > distanceToLose)
            {
                chasing = false;
                _chaseCounter = keepChasingTime;
            }

            if (shotWaitCounter > 0)
            {
                shotWaitCounter -= Time.deltaTime;
                if (shotWaitCounter <= 0)
                    shootTimeCounter = timeToShoot;
                
                anim.SetBool("isMoving", true);
            }
            else
            {
                if (PlayerController.Instance.gameObject.activeInHierarchy)
                {
                    shootTimeCounter -= Time.deltaTime;
                    if (shootTimeCounter > 0)
                    {
                        _fireCount -= Time.deltaTime;
                        if (_fireCount <= 0)
                        {
                            _fireCount = fireRate;
                            firePoint.LookAt(targetPoint + new Vector3(0f, .5f, 0f));

                            Vector3 targetDirection = targetPoint - transform.position;
                            float angle = Vector3.SignedAngle(transform.forward, targetDirection, Vector3.up);

                            if (Math.Abs(angle) <= 30f)
                            {
                                Instantiate(bullet, firePoint.position, firePoint.rotation);
                                anim.SetTrigger("fireShot");
                            }
                            else
                            {
                                shotWaitCounter = waitBetweenShots;
                            }
                        }

                        agent.destination = transform.position;
                    }
                    else
                    {
                        shotWaitCounter = waitBetweenShots;

                    }

                    anim.SetBool("isMoving", false);
                }
            }
        }
    }
}
