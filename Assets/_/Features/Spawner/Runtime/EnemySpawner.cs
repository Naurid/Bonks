using System.Collections;
using System.Collections.Generic;
using PlayerInteractions.Runtime;
using UnityEngine;
using Mirror;

namespace Spawner.Runtime
{
    public class EnemySpawner : NetworkBehaviour
    {

        #region Public Members
        #endregion


        #region Unity API

        // private void Awake()
        // {
        //     _playerAttack = FindObjectOfType<PlayerAttack>();
        // }

        private void OnGUI()
        {
            if (GUILayout.Button("Spawn a Wave"))
            {
                _playerAttack = FindObjectOfType<PlayerAttack>();
                CmdSpawnEnemies();
            }
        }

        private IEnumerator SpawnEnemies()
        {
            int randInt = Random.Range(0, _spawnPoints.Count);
            Instantiate(_enemy, _spawnPoints[randInt].position, Quaternion.identity, _enemyParent);
            if (_playerAttack.m_enemies.Count < _maxEnemyNumber)
            {
                StartCoroutine(SpawnEnemies());
            }
            yield return new WaitForSeconds(0.5f);
        }
        #endregion


        #region Networking

        [Command]
        private void CmdSpawnEnemies()
        {
            StartCoroutine(SpawnEnemies());
        }
        }

        #endregion


        #region Private and Protected

        private PlayerAttack _playerAttack;
        
        [SerializeField] private GameObject _enemy;
        [SerializeField] private Transform _enemyParent;
        [SerializeField] private List<Transform> _spawnPoints;

        [SerializeField] private int _maxEnemyNumber;
        #endregion
    }
}
