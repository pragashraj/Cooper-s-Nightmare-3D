using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    private enum State { IDLE, WALKING, CHASING, ATTACKING }

    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float walkingSpeed = 1f;
    [SerializeField] private float runningSpeed = 2f;
    [SerializeField] private GameObject player;

    private Vector3 startingPosition;
    private Vector3 roamPosition;
    private Vector3 targetPosition;
    private State state;

    private EnemyAI enemyAI;
    private Animator animator;

    private void Awake()
    {
        enemyAI = GetComponent<EnemyAI>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        state = State.WALKING;
        startingPosition = transform.position;
        roamPosition = enemyAI.GetRoamingPosition(startingPosition);
    }

    private void Update()
    {
        SwitchMovements();
        FindTarget();
        IsTargetNear();
        StopChasing();
    }

    private void SwitchMovements()
    {
        switch (state)
        {
            case State.WALKING: HandleWalk();
                break;
            case State.CHASING: HandleChase();
                break;
            case State.ATTACKING: HandleAttack();
                break;
            default: HandleIdle();
                return;
        }
    }

    private void HandleIdle()
    {
        animator.SetFloat("Movement", 0f, 0.1f, Time.deltaTime);
    }

    private void HandleWalk()
    {
        animator.SetFloat("Movement", 1f, 0.1f, Time.deltaTime);
        enemyAI.MoveTo(roamPosition);
        agent.speed = walkingSpeed;

        if (Vector3.Distance(transform.position, roamPosition) < 2f)
        {
            roamPosition = enemyAI.GetRoamingPosition(startingPosition);
        }
    }

    private void HandleChase()
    {
        animator.SetFloat("Movement", 1f, 0.1f, Time.deltaTime);
        transform.LookAt(player.transform);
        targetPosition = player.transform.position + offset;
        enemyAI.MoveTo(targetPosition);
        agent.speed = runningSpeed;
    }

    private void HandleAttack()
    {
        
    }

    IEnumerator ReduceHealth()
    {
        yield return new WaitForSeconds(2f);
        player.GetComponent<PlayerHealth>().DecreaseHealthValue(1f);
    }

    private void FindTarget()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < 20f)
        {
            state = State.CHASING;
        }
    }

    private void IsTargetNear()
    {
        if (Vector3.Distance(transform.position, player.transform.position) <= 3f)
        {
            state = State.ATTACKING;
            agent.isStopped = true;
        } 
        else
        {
            agent.isStopped = false;
        }
    }

    private void StopChasing()
    {
        if (state == State.CHASING)
        {
            if (Vector3.Distance(transform.position, player.transform.position) > 20f)
            {
                animator.SetFloat("Movement", 0f, 0.1f, Time.deltaTime);
                state = State.WALKING;
            }
        }
    }
}