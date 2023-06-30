using Cinemachine;
using Mirror;
using UnityEngine;

namespace PlayerMovement.Runtime
{
    public class PlayerMove : NetworkBehaviour
    {
        #region Public Members

       // public InputActionAsset m_playerInputs;

        #endregion


        #region UnityAPI

        private void FixedUpdate()
        {
            if (_playerCam != null) return;
            if (!isLocalPlayer && !Application.isFocused) return;
            
            _virtualCamera = Instantiate(_virtualCamera);
            _virtualCamera.m_Follow = transform.GetChild(1);
            _playerCam = Camera.main!.transform;
        }

        private void Update()
        {
            if (!isLocalPlayer && !Application.isFocused) return;

           _transform = transform;

           float x = Input.GetAxis("Horizontal");
           float y = Input.GetAxis("Vertical");
           
            var dir = y * _playerCam.forward + x * _playerCam.right;
            var normalizedDir = Vector3.Scale(dir, new Vector3(1, 0, 1)).normalized;
            
            dir.Normalize();
            
            _transform.position += normalizedDir * _moveSpeed;
            _transform.rotation = _playerCam.rotation;
            
        }

        #endregion
        
        #region Private and Protected

        private float _moveX;
        private float _moveY;
        private Transform _transform;
        
        [SerializeField] private Transform _playerCam;
        [SerializeField] private CinemachineVirtualCamera _virtualCamera;
        
        [Space]
        [SerializeField] private float _moveSpeed;

        #endregion
    }
}
