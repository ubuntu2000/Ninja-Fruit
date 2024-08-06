using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameBasic
{
    public class Player : MonoBehaviour
    {

        public float moveSpeedSticker = 400f;
        Rigidbody2D m_rbSticker;

        private void Awake()
        {
            m_rbSticker = GetComponent<Rigidbody2D>();
        }
        private void FixedUpdate()
        {
            Move();
        }
        private void Update()
        {
            LimitPos();
        }
        void Move()
        {
            if (!m_rbSticker) return;

            if (GamePadsController.Ins.CanMoveLeft)
            {
                m_rbSticker.velocity = Vector2.left * moveSpeedSticker * Time.deltaTime;
            }
            else if (GamePadsController.Ins.CanMoveRight)

            {
                m_rbSticker.velocity = Vector2.right * moveSpeedSticker * Time.deltaTime;
            }
            else

            {
                m_rbSticker.velocity = Vector2.zero;
            }
        }
        void LimitPos()
        {
            if (transform.position.x >= 2.2f)
            {
                transform.position = new Vector3(2.2f, transform.position.y, transform.position.z);

            }
            else if (transform.position.x <= -2.2f)
            {
                transform.position = new Vector3(-2.2f, transform.position.y, transform.position.z);
            }
        }
    }
}




