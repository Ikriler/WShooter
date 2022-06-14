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
    [SerializeField]
    public bool _gunIsInfinity = false;

    public AudioSource _currentShootSound; 

    private PlayerInputActions inputActions;
    private Ammo _ammo;


    private void Start()
    {
        _ammo = GetComponent<Ammo>();
        inputActions = PlayerController.playerInputActions;
    }
    private float time;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            GoReload();
        }

        if ((time += Time.deltaTime) > _delay)
        {
            if (inputActions.Player.Fire.ReadValue<float>() != 0)
            {
                time = 0.0f;
                if (_ammo.GetCurrentAmmoCountInMagazine() == 0) return;
                _ammo.AmmoDown();
                GameObject bullet = Instantiate(_bulletPrefab);
                bullet.transform.position = _bulletSpawnPoint.transform.position;
                _currentShootSound.PlayOneShot(_currentShootSound.clip);
                bullet.GetComponent<Rigidbody>().velocity = (_cameraAnhor.transform.position - _bulletSpawnPoint.transform.position) * _power;
            }
        }
    }

    private void GoReload()
    {
        _ammo.Reload();
        if(_gunIsInfinity)
        {
            _ammo.FillAmmo();
        }
        GetComponent<SoundController>().AudioReload();
    }
}
