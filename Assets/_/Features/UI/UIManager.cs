using System.Collections.Generic;
using PlayerInteractions.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

namespace UI.Runtime
{
    public class UIManager : NetworkBehaviour
    {
        #region Public Members
        
        public List<Image> m_hearts;
        
        #endregion


        #region Unity API

        private void Start()
        {
            _playerAttack = FindObjectOfType<PlayerAttack>();
            _playerHealth = FindObjectOfType<PlayerHealth>();
        }

        private void Update()
        {
            _scoreDisplay.text = _playerAttack.m_killCount.ToString();

            if (_playerHealth.m_playerHealth <= m_hearts.Count - 1 && _playerHealth.m_playerHealth >= 0)
            {
                m_hearts[_playerHealth.m_playerHealth].enabled = false;
            }
            
            if (_playerHealth.m_playerHealth <= 0)
            {
                Debug.Log("u ded");
            }
            
        }

        #endregion


        #region Private and Protected

        private PlayerAttack _playerAttack;
        private PlayerHealth _playerHealth;
        
        [SerializeField] private TMP_Text _scoreDisplay;

        #endregion
    }
}
