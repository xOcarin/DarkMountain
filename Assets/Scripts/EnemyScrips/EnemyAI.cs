using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    public bool alreadyAttacked;

    public float timeBetweenAttacks;
    
    public float LungeSpeed; 
    public float JumpForce; 
    
    //patroling
    public Vector3 walkPoint;
    private bool walkPointSet;
    public float walkPointRange;
    
    //Attacking
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    public Animator wolfAnimator;
    public GameObject wolfModel;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        PlayerMovement.canMove = true;
        wolfAnimator = wolfModel.GetComponent<Animator>();
    }

    private void Update()
    {
        //check for sight or attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer); //syntax is (center, radius/range, layermask)
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer); //syntax is (center, radius/range, layermask)
        if(!playerInSightRange && !playerInAttackRange){Patroling();}
        //Debug.Log("In sight range?: " + playerInSightRange);
        if(playerInSightRange && !playerInAttackRange){ChasePlayer();}
        if(playerInSightRange && playerInAttackRange){AttackPlayer();}
    }


    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();
        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }

    private void SearchWalkPoint()
    {
        //calc random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }
    }
    
    private void ChasePlayer()
    {
        //Debug.Log("IS CHASING!!!!!");
        agent.SetDestination(player.position);
    }
    
    private void AttackPlayer()
    {
        agent.SetDestination(player.position);
        transform.LookAt(player);
        if (!alreadyAttacked)
        {
            // Move the enemy towards the player with a sudden burst of speed
            agent.velocity = transform.forward * LungeSpeed;
            
            // Add a vertical force to make the enemy jump
            Rigidbody enemyRigidbody = GetComponent<Rigidbody>();
            enemyRigidbody.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
            StartCoroutine(PlayAttackAnim());
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }


    private IEnumerator PlayAttackAnim()
    {
        wolfAnimator.SetBool("isAttacking", true);
        yield return new WaitForSeconds(2);
        wolfAnimator.SetBool("isAttacking", false);

        
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
    
}
