using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameBasic
{
    public class BOM : MonoBehaviour
    {
       
        Rigidbody2D m_rbBom;
        private GameManager m_vacham;
        public float moveSpeedBom = 150f;

        public void Start()
        {
            m_rbBom = GetComponent<Rigidbody2D>();
            m_vacham = GameManager.FindAnyObjectByType<GameManager>();
        }
        private void FixedUpdate()
        {
            MoveBom();
        }
        void MoveBom()
        {
            m_rbBom.velocity = Vector2.down * moveSpeedBom * Time.deltaTime;
        }
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.CompareTag(Const.DEADZONE_TAG))
            {
              //  m_vacham.AddScore();
                if (m_vacham.guiMng)
                    m_vacham.guiMng.UpdateGamePlayScore();
                Destroy(gameObject);

                Destroy(gameObject);

                Debug.Log("Da va cham voi DeadZone");
            }
        }
        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.CompareTag(Const.PLAYER_TAG))
            {
                //if (m_vacham.heaths <= 0)
                //{
                //    m_vacham.Die();
                //     m_vacham.IncrementGameOver(true);
                //    Destroy(gameObject);
                //}
                //else
                //{
                //    m_vacham.IncrementHeaths(1);
                //}
                Destroy(gameObject);
                Debug.Log("Đa va cham voi hop");
            }


        }
    }
}