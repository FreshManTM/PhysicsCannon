using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CannonController : MonoBehaviour
{
    [Header("Scriptable")]
    [SerializeField] GameObject _projectilePrefab;  // Projectile of the cannon
    [SerializeField] Transform _gun;                // Cannon Transform

    [Header("Movement")]
    [SerializeField] float _speed;                  // Cannon movement/rotation speed
    [SerializeField] Vector2 _powerLimit;           // Min and max power value   
    [SerializeField] Vector2 _lookLimitX;           // Min and Max look vertical value
    [SerializeField] Vector2 _lookLimitY;           // Min and Max look horizontal value

    public float Power;                             // Cannon power
    public Transform Muzzle;                        // Transform where the projectile will spawn

    Projectile _projectile;                         // Spawned projectile
    Vector2 _rotation;                              // Controlls the cannon rotation

    private void Start()
    {
        Cursor.visible = false;
        ObjectPool.Instance.PreLoad(_projectilePrefab, 10);
    }

    void Update()
    {
        CannonMovement();
        SetPower();
        Shoot();
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Time.timeScale -= 0.1f;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Time.timeScale += 0.1f;
        }
    }

    void CannonMovement()
    {
        _rotation.x += -Input.GetAxis("Mouse Y") * _speed * 3 * Time.deltaTime;
        _rotation.x = Mathf.Clamp(_rotation.x, _lookLimitX.y, _lookLimitX.x);

        _rotation.y += Input.GetAxis("Horizontal") * _speed * Time.deltaTime;
        _rotation.y = Mathf.Clamp(_rotation.y, _lookLimitY.x, _lookLimitY.y);

        transform.localRotation = Quaternion.Euler(0, _rotation.y, 0);
        _gun.localRotation = Quaternion.Euler(_rotation.x, 0, 0);
    }

    void SetPower()
    {
        Power = Power + Input.GetAxis("Vertical") / 4;
        Power = Mathf.Clamp(Power, _powerLimit.x, _powerLimit.y);
    }

    void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _projectile = ObjectPool.Instance.Spawn(_projectilePrefab, Muzzle.position, Muzzle.rotation).GetComponent<Projectile>();
            _projectile.Init(Power);
            Screenshake.Shake = true;
        }
    }

}
