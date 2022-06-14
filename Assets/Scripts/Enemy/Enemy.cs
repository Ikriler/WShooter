using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent navMeshAgent;
    private PlayerFinder playerFinder;
    private EnemyAttack enemyAttack;

    private bool attackState = false;
    private EnemyAnimatorController enemyAnimatorController;

    private AnimationState currentAnimationState = AnimationState.Idle;

    private enum AnimationState
    {
        Move = 0,
        Idle = 1,
        Attack = 2
    }

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        playerFinder = GetComponent<PlayerFinder>();
        enemyAttack = GetComponent<EnemyAttack>();

        enemyAnimatorController = GetComponent<EnemyAnimatorController>();
    }

    private void Update()
    {
        if (playerFinder == null)
        {
            Debug.Log(gameObject.name + ": playerFinder not found.");
            return;
        }
        if (navMeshAgent == null)
        {
            Debug.Log(gameObject.name + ": navMeshAgent not found.");
            return;
        }
        if (enemyAttack == null)
        {
            Debug.Log(gameObject.name + ": enemyAttack not found.");
            return;
        }



        if (playerFinder.GetFindState() && Vector3.Distance(playerFinder.GetPostitionTarget(), transform.position) > 3f)
        {
            currentAnimationState = AnimationState.Move;
            navMeshAgent.SetDestination(playerFinder.GetPostitionTarget());
        }
        //else if(Vector3.Distance(navMeshAgent.destination, transform.position) > 0.1f && !playerFinder.GetFindState())
        else if(navMeshAgent.transform.position == navMeshAgent.pathEndPosition)
        {
            currentAnimationState = AnimationState.Move;
            navMeshAgent.SetDestination(GetRandomPoint());
        }
        else
        {
            currentAnimationState = AnimationState.Idle;
        }
        if (enemyAttack.GetAttackState(playerFinder.GetTarget()) && !attackState)
        {
            //currentAnimationState = AnimationState.Attack;
            StartCoroutine(Attack());
        }
        SetAnimation();
    }

    private Vector3 GetRandomPoint()
    {
        return transform.position + Random.onUnitSphere * 2;
    }

    private IEnumerator Attack()
    {
        attackState = true;
        enemyAttack.DealDamage(playerFinder.GetTarget());
        yield return new WaitForSeconds(1);
        attackState = false;
    }

    private void SetAnimation()
    {
        if (enemyAnimatorController == null) return;
        switch (currentAnimationState)
        {
            case AnimationState.Attack:
                enemyAnimatorController.AttackAnimation();
                break;
            case AnimationState.Move:
                enemyAnimatorController.MoveAnimation();
                break;
            case AnimationState.Idle:
                enemyAnimatorController.IdleAnimation();
                break;
        }
    }

}
