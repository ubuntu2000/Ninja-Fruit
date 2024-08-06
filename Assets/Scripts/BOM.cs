using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameBasic
{
    public class BOM : MonoBehaviour
    {
        public float moveSeepBom = 150f;
        Rigidbody2D m_rbBom;


        GameManager m_vacham;
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
            m_rbBom.velocity = Vector2.down * moveSeepBom * Time.deltaTime;
        }
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.CompareTag("DeadZone"))
            {

                
                Destroy(gameObject);

                Debug.Log("Da va cham voi DeadZone");
            }
        }
        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.CompareTag("Sticker"))
            {
               
                Destroy(gameObject);
                Debug.Log("Đa va cham voi hop");
            }


        }
    }
}