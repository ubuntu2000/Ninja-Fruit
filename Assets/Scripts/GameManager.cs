using System.Collections;
using UnityEngine;
using UnityEngine.UI;


namespace GameBasic
{


    public class GameManager : MonoBehaviour, IComponentCheck
    {

        public static GameManager Ins;

        public float time_Delay;
        float m_curTimeDelay;
        public Text timeCountingdownText;
        // private bool m_IsDemso = false;
        private Vector3 spawnPosFruitPrefab;
        
        // Khai báo biến thời gian Spawn( sau khi xảy ra va chạm,...thì đoi tuong tiep tuc spawn)
        public float spawnTime;
        // Khai báo biến thời gian m_spawnTime ( biến private)
        // m_spawnTime(biến nhớ) để thực thi truyền giá trị tham số mà không thay đổi biến của spawnTime
        private float m_curspawnTime;
       
        public GameObject BomPrefabs;
        [Range(0f,1f)]
        public float bomChance = 0.05f;
        public GameObject[] FruitPrefabs;
        public GUIManager guiMng;
        public AudioController auCtr;
        public ShopManager shopMng;
        private Player m_curPlayer;
        public GameObject m_deadzone;
        private bool m_IsGameOver;
        private int m_score;
        
        public int Score { get => m_score; set => m_score = value; }

        private void Awake()
        {
            Ins = this;
            
        }


        // Start is called before the first frame update
        void Start()
        {
           
           
           
            if (IsComponentsNull()) return;
            guiMng.ShowGameGUI(false);
            
        }
       
        public bool IsComponentsNull()
        {
            return guiMng == null || shopMng == null || auCtr == null;

        }
        public void PlayGameButton()
        {

            if (IsComponentsNull()) return;
            m_curTimeDelay = time_Delay;
            m_curspawnTime = spawnTime;
            StartCoroutine(CountingDown());
            StartCoroutine(SpawnObject());
            ActivePlayer();
            guiMng.ShowGameGUI(true);
            guiMng.UpdateGamePlayScore();
            auCtr.PlayBgm();


        }
       
                


        #region Tạo hieu ung dem so

        IEnumerator CountingDown()
        {
            while (m_curTimeDelay > 0)
            {
                yield return new WaitForSeconds(1f);
                m_curTimeDelay--;
                
                UpdateTimeCountDown(m_curTimeDelay);
                Debug.Log("game chạy");
            }
        }

        public void UpdateTimeCountDown(float time)
        {

            if (timeCountingdownText)
                timeCountingdownText.text = time.ToString("00");

            if (time <= 0)
            {
                if (timeCountingdownText)
                {
                    timeCountingdownText.gameObject.SetActive(false);
                    if (FruitPrefabs != null && m_curTimeDelay <= 0)
                    {
                        m_curspawnTime = 0;

                    }
                }

            }
        }



        #endregion

        #region Tạo hoa quả
        IEnumerator SpawnObject()
        {
            while (!m_IsGameOver)
            {
                if (m_curspawnTime > 0)
                {
                    yield return new WaitForSeconds(1f);
                    m_curspawnTime--;

                }
                else
                {
                    if (FruitPrefabs != null && BomPrefabs != null && FruitPrefabs.Length > 0)
                    {
                       
                            
                            int randIdx = Random.Range(0, FruitPrefabs.Length);
                            GameObject Prefab = FruitPrefabs[randIdx];
                             if (Random.value < bomChance)
                            {
                                Prefab = BomPrefabs;
                             }
                        spawnPosFruitPrefab = new Vector3((Random.Range(-2, 2)), 8, 0);
                           GameObject fruitPrefab = Instantiate(Prefab, spawnPosFruitPrefab, Quaternion.identity);
                            
                    }
                    
                        
                    m_curspawnTime = spawnTime;
                }
                   
                
                
            }
        }
       
        
        #endregion




        public void ActivePlayer()
        {
            if (IsComponentsNull()) return;
            if (m_curPlayer)
                Destroy(m_curPlayer.gameObject);

            var shopItems = shopMng.items;

            if (shopItems == null || shopItems.Length <= 0) return;

            var newPlayerPb = shopItems[Pref.curPlayerID].playerPrefab;


            if (newPlayerPb)
                m_curPlayer = Instantiate(newPlayerPb, new Vector3(-0.5f, -4.5f, 0f), Quaternion.identity);
            m_deadzone = Instantiate(m_deadzone, new Vector3(-0.9f, -4.7f, 0f), Quaternion.identity);
        }
        public void GameOver()
        {
            if (m_IsGameOver) return;

            m_IsGameOver = true;
            Pref.bestScore = m_score;
            if (guiMng.GameOverDialog)
                guiMng.GameOverDialog.Show(true);
            auCtr.PlaySound(auCtr.gameOver);
        }


       


    }

}