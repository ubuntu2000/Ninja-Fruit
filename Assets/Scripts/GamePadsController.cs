using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameBasic
{
    public class GamePadsController : Singleton<GamePadsController>
    {
        // Bien kiem tra co tren mobile ko?
        public bool IsOnMobile;

        bool m_canMoveLeft;
        bool m_canMoveRight;

        public bool CanMoveLeft { get => m_canMoveLeft; set => m_canMoveLeft = value; }
        public bool CanMoveRight { get => m_canMoveRight; set => m_canMoveRight = value; }


        // Start is called before the first frame update
        public override void Awake()
        {
            MakeSingleton(false);
        }

        void PCInputHandles()
        {
            m_canMoveLeft = Input.GetAxisRaw("Horizontal") < 0 ? true : false;
            m_canMoveRight = Input.GetAxisRaw("Horizontal") > 0 ? true : false;
        }

        private void Update()
        {
            if (!IsOnMobile)
                PCInputHandles();
        }
    }
}