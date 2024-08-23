using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace GameBasic {
    public class GameData : Singleton<GameData>
    {
        public int coin = 100;
        public int health;
        public int maxhealth;

        public int score;
        public int curLevelId;
        public int curPlayerId;
        public float musicVol;
        public float sfxVol;
        public bool isNoAds;
        public List<bool> levelAchievementUnlockeds;
        public List<bool> levelAchievementPasseds;
        public List<bool> levelUnlockeds;
        public List<bool> levelPasseds;
        public List<bool> playerUnlockeds;
        public List<int> levelStars;
        public List<string> playerStats;
        public List<int> completedScore;
        private string m_data;

        public UnityEvent OnInit;
        public UnityEvent OnLocalLoaded;
        public UnityEvent OnCloudLoaded;
        public UnityEvent OnDataLoaded;

        public override void Awake()
        {
            base.Awake();

            levelUnlockeds= new List<bool>();
            levelPasseds= new List<bool>();
            playerUnlockeds= new List<bool>();
            levelStars = new List<int>();
            playerStats= new List<string>();
            completedScore = new List<int>();
            m_data = string.Empty;
        }

        private void Start()
        {
            Init();
        }

        private void Init()
        {
            if (OnInit != null)
            {
                OnInit.Invoke();
            }

            SaveData();
        }

        private void HandleData(string data)
        {
            if(string.IsNullOrEmpty(data)) return;

            JsonUtility.FromJsonOverwrite(data, this);
        }

        public void SaveData()
        {
            Pref.GameData = JsonUtility.ToJson(this);
        }

        public void LoadData(string data)
        {
            HandleData(data);

            if(OnDataLoaded != null)
            {
                OnDataLoaded.Invoke();
            }
        }

        private T GetValue<T>(List<T> dataList, int idx)
        {
            if(dataList == null || dataList.Count == 0 || dataList[idx] == null) return default;

            return dataList[idx];
        }

        private void UpdateValue<T>(ref List<T> dataList, int idx, T value)
        {
            if(dataList == null) return;

            if(dataList.Count <= 0 || (dataList.Count > 0 && idx >= dataList.Count))
            {
                dataList.Add(value);
            }else
            {
                dataList[idx] = value;
            }
        }

        public bool GetLevelUnlocked(int id)
        {
            return GetValue<bool>(levelUnlockeds, id);
        }

        public void UpdateLevelUnlocked(int id, bool isUnlocked)
        {
            UpdateValue<bool>(ref levelUnlockeds, id, isUnlocked);
        }

        public bool GetLevelPassed(int id)
        {
            return GetValue<bool>(levelPasseds, id);
        }

        public void UpdateLevelPassed(int id, bool isUnlocked)
        {
            UpdateValue<bool>(ref levelPasseds, id, isUnlocked);
        }
        public bool GetLevelAchievementUnlocked(int id)
        {
            return GetValue<bool>(levelAchievementUnlockeds, id);
        }

        public void UpdateLevelAchievementUnlocked(int id, bool isUnlocked)
        {
            UpdateValue<bool>(ref levelAchievementUnlockeds, id, isUnlocked);
        }

        public bool GetLevelAchievementPassed(int id)
        {
            return GetValue<bool>(levelAchievementPasseds, id);
        }

        public void UpdateLevelAchievementPassed(int id, bool isUnlocked)
        {
            UpdateValue<bool>(ref levelAchievementPasseds, id, isUnlocked);
        }

        public bool GetPlayerUnlocked(int id)
        {
            return GetValue<bool>(playerUnlockeds, id);
        }

        public void UpdatePlayerUnlocked(int id, bool isUnlocked)
        {
            UpdateValue<bool>(ref playerUnlockeds, id, isUnlocked);
        }

        /*public int GetLevelStars(int levelId)
        {
            return GetValue<int>(levelStars, levelId);
        }

        public void UpdateLevelStars(int levelId, int stars)
        {
            UpdateValue<int>(ref levelStars, levelId, stars);
        }

        public string GetPlayerStat(int id)
        {
            return GetValue<string>(playerStats, id);
        }

        public void UpdatePlayerStat(int id, string stat)
        {
            UpdateValue<string>(ref playerStats, id, stat);
        } */

        public float GetLevelScore(int levelId)
        {
            return GetValue<int>(completedScore, levelId);
        }

        public void UpdateLevelScoreNoneCheck(int levelId, int m_curScore)
        {
            UpdateValue<int>(ref completedScore, levelId, m_curScore);
        }

        public void UpdateLevelScore(int levelId, int m_curScore)
        {
            int oldScore = GetValue<int>(completedScore, levelId);

            if(m_curScore >= oldScore || oldScore == 0)
            {
                UpdateLevelScoreNoneCheck(levelId,m_curScore);
            }
        }

        private bool IsItemUnlocked(List<bool> dataList, int idx)
        {
            if (dataList == null || dataList.Count <= 0) return false;

            return dataList[idx];
        }

        public bool IsLevelUnlocked(int id)
        {
            return IsItemUnlocked(levelUnlockeds, id);
        }

        public bool IsLevelPassed(int id)
        {
            return IsItemUnlocked(levelPasseds, id);
        }

        public bool IsPlayerUnlocked(int id)
        {
            return IsItemUnlocked(playerUnlockeds, id);
        }

        public void LoadLocal()
        {
            if(Pref.IsFirstTime)
            {
                Init();
            }else
            {
                LoadData(Pref.GameData);
                SaveData();

                if (OnLocalLoaded != null)
                {
                    OnLocalLoaded.Invoke();
                }
            }
        }
    }
}
