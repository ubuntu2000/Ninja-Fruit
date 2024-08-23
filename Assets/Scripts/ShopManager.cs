using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameBasic
{


    public class ShopManager : MonoBehaviour
    {
        public ShopItem[] items;

        private void Start()
        {
            Init();
        }
        // Khoi tao cac du lieu ban dau dc luu xuong may nguoi dung trong shop
        public void Init()
        {
            if (items == null || items.Length <= 0) return;

            for (int i = 0; i < items.Length; i++)
            {
                var item = items[i];

                if (item == null) continue;

                if (i == 0)
                {
                    GameData.Ins.UpdatePlayerUnlocked(i, true);
                    GameData.Ins.curPlayerId = i;
                }
                else
                {
                    GameData.Ins.UpdatePlayerUnlocked(i, false);
                }
               
            }
            GameData.Ins.SaveData();
        }
    }
}
