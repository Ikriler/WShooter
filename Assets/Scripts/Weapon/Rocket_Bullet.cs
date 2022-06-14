using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket_Bullet : MonoBehaviour
{
    [SerializeField]
    public float _damage = 34f;
    [SerializeField]
    public int _secondLifeTime = 10;
    [SerializeField]
    private ParticleSystem explode;

    private bool exploded = false;

    private void Awake()
    {
        StartCoroutine(StartLifeTimeCounter());
        explode = GetComponent<ParticleSystem>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (exploded) return;
        Health targetHealth = other.GetComponent<Health>();
        if (other.tag == "Bullet") return;
        if (other.tag == "Player") return;
        if (targetHealth == null)
        {
            StartCoroutine(Explode());
            return;
        }
        targetHealth.HealthDown(_damage);
        StartCoroutine(Explode());
    }

    private IEnumerator StartLifeTimeCounter()
    {
        for (int i = 0; i <= _secondLifeTime; i++)
        {
            if (i == _secondLifeTime) 
            {
                if(!exploded) StartCoroutine(Explode());
            }
            yield return new WaitForSeconds(1);
        }
    }

    private IEnumerator Explode()
    {
        exploded = true;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        explode.Play();
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
