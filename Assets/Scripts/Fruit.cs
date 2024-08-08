using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameBasic
{
    public class Fruit : MonoBehaviour, IComponentCheck
    {
        public float moveSpeedBall = 100f;
        Rigidbody2D m_rbFruit;
       private GameManager m_vacham;
        private bool m_IsDead;



        public void Start()
        {
            m_rbFruit = GetComponent<Rigidbody2D>();
            m_vacham = GameManager.FindAnyObjectByType<GameManager>();
        }
        public bool IsComponentsNull()
        {
          return  m_rbFruit == null || m_vacham == null; 
        }
        private void FixedUpdate()
        {
            if (IsComponentsNull()) return;
            MoveBall();
        }
       
        void MoveBall()
        {
            m_rbFruit.velocity = Vector2.down * moveSpeedBall * Time.deltaTime;
        }

        public void Die()
        {
            if (m_IsDead ) return;

            m_IsDead = true;
            m_rbFruit.velocity = Vector2.zero;
           // gameObject.layer = LayerMask.NameToLayer(Const.DEAD_ANIM);
            if (m_vacham.auCtr)
                m_vacham.auCtr.PlaySound(m_vacham.auCtr.enemyDead);

            Destroy(gameObject, 2f);
        }
        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.CompareTag(Const.PLAYER_TAG))
            {
                //m_vacham.Score++;
                int scorebonus = 100;
                Pref.Score += scorebonus;
                if (m_vacham.guiMng)
                    m_vacham.guiMng.UpdateGamePlayScore();
                Destroy(gameObject);
                Debug.Log("Đa va cham voi hop");
            }
        }
            private void OnTriggerEnter2D(Collider2D col)
            {
            if (col.gameObject.CompareTag(Const.DEADZONE_TAG))
             {

                Die();
               
                Destroy(gameObject);

                Debug.Log("Da va cham voi DeadZone");
             }
            }
        


        


    }


}
