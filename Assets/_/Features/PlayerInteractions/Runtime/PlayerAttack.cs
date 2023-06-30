using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace PlayerInteractions.Runtime
{
    public class PlayerAttack : NetworkBehaviour
    {
        #region Public Members

        public List<GameObject> m_enemies;
        public Animation m_bonkAnim;

        public int m_killCount;
        
        #endregion


        #region Unity API

        // private void OnDrawGizmos()
        // {
        //     Handles.color = Color.green;
        //     Handles.DrawWireDisc(transform.position, Vector3.up, _meleeRange);
        // }

        #endregion


        #region Main Methods

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Joystick1Button0) && isLocalPlayer)
            {
                Debug.Log("yay");
                m_bonkAnim.Play();
                Attack();
            }
        }
        
        private void Attack()
        {
            foreach (GameObject enemy in m_enemies)
            {
                var enemyPosition = enemy.transform.position;
                var playerPosition = transform.position;
                
                var enemyDir = playerPosition - enemyPosition;
                var distance = Mathf.Abs(Vector3.Distance(playerPosition, enemyPosition));
                var angle = Mathf.Abs(Vector3.Angle(transform.forward, enemyDir));
                
                if (distance < _meleeRange && angle > _maxAngle)
                {
                    Destroy(enemy);
                    m_killCount++; 
                }
            }
        }
        #endregion

        #region Private and Protected

        [SerializeField] private float _meleeRange;
        [SerializeField] private float _maxAngle;
        

        #endregion
    }
}
