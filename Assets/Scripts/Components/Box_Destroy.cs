using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box_Destroy : MonoBehaviour
{
    [SerializeField]
    public GameObject _itemForSpawn;
    private Health boxHealth;
    void Awake()
    {
        boxHealth = GetComponent<Health>();
    }

    void Update()
    {
        if(boxHealth._health <= 0)
        {
            GameObject item = Instantiate(_itemForSpawn);
            item.transform.position = transform.position;
            Destroy(gameObject);
        }
    }
}
