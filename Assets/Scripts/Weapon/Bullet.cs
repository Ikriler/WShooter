using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    public float _damage = 34f;
    [SerializeField]
    public int _secondLifeTime = 10;

    private void Awake()
    {
        StartCoroutine(StartLifeTimeCounter());   
    }


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        Health targetHealth = other.GetComponent<Health>();
        if (other.tag == "Bullet") return;
        if (other.tag == "Player") return;
        if (targetHealth == null)
        {
            Destroy(gameObject);
            return;
        }
        targetHealth.HealthDown(_damage);
        Destroy(gameObject);
    }

    private IEnumerator StartLifeTimeCounter()
    {
        for(int i = 0; i <= _secondLifeTime; i++)
        {
            if (i == _secondLifeTime) Destroy(gameObject);
            yield return new WaitForSeconds(1);
        }
    }
}
