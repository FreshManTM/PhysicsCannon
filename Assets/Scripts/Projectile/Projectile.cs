using System;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Projectile : MonoBehaviour
{
    [SerializeField] LayerMask _layerMask;              // The collide layer          
    [SerializeField] GameObject _explosionEffect;       
    [SerializeField] GameObject _decal;                 // Mark decal

    ObjectPool _pool;                                   
    Vector3 _velocity;                                  // The projectile direction/velocity
    RaycastHit hit;                                     // Hit of the projectile and the surface

    int _collisionsCount;                               // Number of collisions
    float _power;                                       // Projectile power
    float _gravity = 9.81f;
    float _bounceMultiplier = .5f;
    private void FixedUpdate()
    {
        _velocity.y -= _gravity * Time.fixedDeltaTime;
        transform.position += _velocity * Time.fixedDeltaTime;

        CollisionDetect();
    }

    public void Init(float power)
    {
        _power = power;
        _velocity = transform.right * _power;
        _pool = ObjectPool.Instance;
    }

    void CollisionDetect()
    {
        float rayLength = Mathf.Max(_velocity.magnitude * Time.fixedDeltaTime, 0.1f);

        Ray ray = new Ray(transform.position, _velocity.normalized);

        if (Physics.Raycast(ray, out hit, rayLength))
        {
            LeaveMark();
            UpdateVelocity();
        }
    }

    void UpdateVelocity()
    {
        if (_collisionsCount > 0)
        {
            _collisionsCount = 0;
            _pool.Spawn(_explosionEffect, hit.point, Quaternion.identity);
            _pool.Despawn(gameObject);
        }
        else
        {
            _collisionsCount++;
            if (!hit.collider.CompareTag("Wall"))
            {
                Vector3 normal = hit.normal;
                _velocity = Vector3.Reflect(_velocity * _bounceMultiplier, normal);
            }
            else
            {
                _velocity = Vector3.down * _gravity * Time.fixedDeltaTime;
            }
            transform.position = hit.point - new Vector3(0, 0, .2f);
        }
    }
    
    void LeaveMark()
    {
        if(hit.transform.gameObject.layer == 6)
        {
            _pool.Spawn(_decal, hit.point, Quaternion.FromToRotation(Vector3.forward, hit.normal));
        }
    }
}