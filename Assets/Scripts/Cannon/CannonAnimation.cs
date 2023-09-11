using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonAnimation : MonoBehaviour
{
    [SerializeField] Transform _cannonTransform;                                // The cannon cylinder
    [SerializeField] Vector3 _targetRecoilPosition = new Vector3(0, 0, -0.2f);  // The amount and direction of recoil.
    [SerializeField] float _recoilSpeed = 5f;                                   // The speed of the recoil.

    Vector3 _initialPosition;
    bool _recoil;                                                               // True when needs to move the Cannon backward
    bool _recoilBack;                                                           // True when needs to move the Cannon forward after the recoil
    private void Start()
    {
        _initialPosition = _cannonTransform.localPosition;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !_recoil)
        {
            _recoil = true;
        }

        if (_recoil)
        {
            Recoil();
        }
    }

    void Recoil()
    {
        if (!_recoilBack)
        {
            _cannonTransform.localPosition = Vector3.MoveTowards(_cannonTransform.localPosition, _targetRecoilPosition, _recoilSpeed * Time.deltaTime);
            if (_cannonTransform.localPosition == _targetRecoilPosition)
            {
                _recoilBack = true;
            }
        }
        else
        {
            // Return to the initial position.
            _cannonTransform.localPosition = Vector3.MoveTowards(_cannonTransform.localPosition, _initialPosition, _recoilSpeed * Time.deltaTime);
            if (_cannonTransform.localPosition == _initialPosition)
            {
                _recoilBack = false;
                _recoil = false;
            }
        }
    }
}
