using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField]
    public float _delay = 3f;
    [SerializeField]
    public GameObject _bulletSpawnPoint;
    [SerializeField]
    public GameObject _bulletPrefab;
    [SerializeField]
    public GameObject _cameraAnhor;
    [SerializeField]
    public float _power = 1f;

    private PlayerInputActions inputActions;



    private void Start()
    {
        inputActions = PlayerController.playerInputActions;
    }
    private float time;
    private void Update()
    {
        if ((time += Time.deltaTime) > _delay)
        {
            if (inputActions.Player.Fire.ReadValue<float>() != 0)
            {
                time = 0.0f;
                GameObject bullet = Instantiate(_bulletPrefab);
                bullet.transform.position = _bulletSpawnPoint.transform.position;
                bullet.GetComponent<Rigidbody>().velocity = (_cameraAnhor.transform.position - _bulletSpawnPoint.transform.position) * _power;
            }
        }
    }
}
