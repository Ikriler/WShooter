using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField]
    public float _damage = 5;
    [SerializeField]
    public float _attackRadius = 5f;
    public void DealDamage(GameObject target)
    {
        Health targetHealth = target.GetComponent<Health>();
        if (targetHealth == null) return;
        if(Vector3.Distance(transform.position, target.transform.position) < _attackRadius)
        {
            targetHealth.HealthDown(_damage);
        }
    }

    public bool GetAttackState(GameObject target)
    {
        return Vector3.Distance(transform.position, target.transform.position) < _attackRadius;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, _attackRadius);
    }
}
