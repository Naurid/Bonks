using System.Collections;
using PlayerInteractions.Runtime;
using UnityEngine;
using UnityEngine.AI;

namespace EnemyBehaviour.Runtime
{
    public class Enemy : MonoBehaviour
    {
        #region Public Members
        #endregion


        #region Unity API

        private void OnEnable()
        {
            _playerAttack = FindObjectOfType<PlayerAttack>();
            _playerHealth = FindObjectOfType<PlayerHealth>();
            _playerAttack.m_enemies.Add(gameObject);
        }

        private void Awake()
        {
            _meshAgent = GetComponent<NavMeshAgent>();
            _canAttack = true;
        }

        private void FixedUpdate()
        {
            SetAgentDestination();
        }

        private void SetAgentDestination()
        {
            _meshAgent.SetDestination(_playerAttack.transform.position);
        }

        private void Update()
        {
            var distance = Vector3.Distance(_meshAgent.destination,transform.position);
            if (distance < _meleeRange)
            {
                if (!_canAttack) return;
                
                StartCoroutine(AttackPlayer());
            }
            else
            {
                SetAgentDestination();
            }
        }

        private IEnumerator AttackPlayer()
        {
            _canAttack = false;
            yield return new WaitForSeconds(_attackRate);
            _playerHealth.m_playerHealth--;
            _canAttack = true;
        }
        
        private void OnDestroy()
        {
            _playerAttack.m_enemies.Remove(gameObject);
        }

        #endregion


        #region Private and Protected

        private PlayerAttack _playerAttack;
        private PlayerHealth _playerHealth;
        private NavMeshAgent _meshAgent;

        private bool _canAttack;

        [SerializeField] private float _meleeRange;
        [SerializeField] private float _attackRate;

        #endregion
    }
}
