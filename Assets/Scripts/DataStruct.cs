using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameBasic
{
    public enum KeyPref
    {
        game_data_,
        IsFirstTime,
        SpriteOrder,
        CloudDataLoaded
    }
    public enum Direction
    {
        Left, Right, Top, Bottom, None
    }

    public enum GameState
    {
        Starting,
        Playing,
        Wining,
        Gameover
    }

    public enum PlayerState
    {
        Idle,
       MoveLeft,
       Moveright,
        Dead
    }
    public enum PlayerCollider
    {
        Normal,
        Dead
    }
    public enum FBLogEvent
    {
        hero_shop_open,
        iap_shop_open,
        gameover_ads,
        mission_completed_ads,
        revive_ads,
        iap_no_ads
    }
    [System.Serializable]

    public class ShopItem
    {
        public Player playerPrefab;
        public int price;
        public Sprite previewImg;

    }
    [System.Serializable]
    public class Goal
    {
        public int timeOneStar;
        public int timeTwoStar;
        public int timeThreeStar;

        public int GetStar(int time)
        {
            if (time < timeOneStar)
            {
                return 3;
            }
            else if (time < timeTwoStar)
            {
                return 2;
            }
            else
            {
                return 1;
            }
        }
    }

}