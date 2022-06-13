using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFinder : MonoBehaviour
{
    [SerializeField]
    public float _findRadius = 10f;

    private bool playerIsFind = false;

    private GameObject player;

    private void Start()
    {
        player = PlayerController.S.gameObject;
    }

    private void Update()
    {
        if(Vector3.Distance(transform.position, player.transform.position) < _findRadius)
        {
            playerIsFind = true;
        }
        else
        {
            playerIsFind = false;
        }
    }

    public bool GetFindState()
    {
        return playerIsFind;
    }

    public Vector3 GetPostitionTarget()
    {
        return player.transform.position;
    }

    public GameObject GetTarget()
    {
        return player;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, _findRadius);
    }
}
