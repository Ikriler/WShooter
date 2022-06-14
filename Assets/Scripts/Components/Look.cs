using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Look : MonoBehaviour
{
    [SerializeField]
    public GameObject _target;

    void Update()
    {
        gameObject.transform.LookAt(_target.transform);
    }
}
