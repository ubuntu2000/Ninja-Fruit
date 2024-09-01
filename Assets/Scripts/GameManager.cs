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
        [Range(0f, 1f)]
        public float bomChance = 0.05f;
        public GameObject[] FruitPrefabs;
        public GUIManager guiMng;
        public AudioController auCtr;
        public ShopManager shopMng;
        private Player m_curPlayer;
        public GameObject m_deadzone;
        private bool m_IsGameOver ;
        private int m_score;

        public float timeLimit;
        private float m_curtimeLimit;
        private int m_curScore;
        private int m_newScore;
        private Fruit m_Fruit;
        [Range(0f, 1f)]
        public float rate;


        public GameObject Heart_01;
        public GameObject Heart_02;
        public GameObject Heart_03;
        private int heaths;
        bool m_IsHeath01;
        bool m_IsHeath02;
        bool m_IsHeath03;
        public Sprite heartFull;
        public Sprite heartEmpty;
        public int maxheaths = 3;



        public int Score { get => m_score; set => m_score = value; }
        public int Heaths { get => heaths; set => heaths = value; }
       

        private void Awake()
        {
            Ins = this;

        }


        // Start is called before the first frame update
        void Start()
        {
            m_curTimeDelay = time_Delay;
            m_curspawnTime = spawnTime;
            m_IsGameOver = false;
            heaths = maxheaths;
            m_Fruit = GetComponent<Fruit>();
            m_curtimeLimit = timeLimit;
            if (IsComponentsNull()) return;
            guiMng.ShowGameGUI(false);
           
        }

        void Update()
        {
            if (m_curTimeDelay <= 0 && FruitPrefabs != null && BomPrefabs != null && FruitPrefabs.Length > 0 && m_IsGameOver == false)
            {
                m_curspawnTime = m_curspawnTime - Time.deltaTime;
            }
            else
            {
                return;
            }
            
            if (heaths <= 0 )
                {
                
                    m_IsGameOver = true;
                    m_curspawnTime = 0;
                if (guiMng.GameOverDialog)
                    guiMng.GameOverDialog.Show(true);
                return;
                }
                if(m_curspawnTime <=0)
                 {
                int randIdx = Random.Range(0, FruitPrefabs.Length);
                GameObject Prefab = FruitPrefabs[randIdx];
                if (Random.value < bomChance)
                {
                    Prefab = BomPrefabs;
                }
                spawnPosFruitPrefab = new Vector3((Random.Range(-2, 2)), 8, 0);
                GameObject fruitPrefab = Instantiate(Prefab, spawnPosFruitPrefab, Quaternion.identity);
                m_curspawnTime = spawnTime;
            }

        }
         
       
        public bool IsComponentsNull()
        {
            return guiMng == null || shopMng == null || auCtr == null;

        }
        public void PlayGameButton()
        {
            
            if (IsComponentsNull()) return;
            
            StartCoroutine(CountingDown());
            StartCoroutine(CountingUpdateHeart());
            ActivePlayer();
            guiMng.ShowGameGUI(true);
            guiMng.UpdateGamePlayScore();
            auCtr.PlayBgm();
            //StartCoroutine(SpawnObject());

        }
     
        #region GameOver



        public void GameOver()
        {

            if (m_IsGameOver) return;

            m_IsGameOver = true;
           // StopCoroutine(SpawnObject());
            // Pref.bestScore = m_score;

            
            // auCtr.PlaySound(auCtr.gameOver);
        }

        #endregion

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
                        StartCoroutine(CountingDownTime());
                    }
                }

            }
        }
        #endregion


        #region Hieu ung chay thoi gian 
        // Hieu ung thoi gian 1 man choi 
        string IntToTime(float time)
        {
            float minutes = Mathf.Floor(time / 60);
            float seconds = Mathf.Floor(time % 60);
            return minutes.ToString("00") + " : " + seconds.ToString("00");
        }


        IEnumerator CountingDownTime()
        {
            while (m_curtimeLimit > 0)
            {
                yield return new WaitForSeconds(1f);
                m_curtimeLimit--;
                if (m_curtimeLimit <= 0)
                {
                    // Không tạo thêm bóng.
                    m_IsGameOver = false;
                    // Hiển thị GameWinDialog.

                }
                guiMng.UpdateTimer(IntToTime(m_curtimeLimit));
            }
        }

        #endregion


        #region Update Image Heath 
        IEnumerator CountingUpdateHeart()
        {
            while (!m_IsGameOver)
            {
                yield return new WaitForSeconds(Time.deltaTime);

                UpdateHeart();
                IsHeart_01();
                IsHeart_02();
                IsHeart_03();
                UpdateHeart();



            }
        }


        void IsHeart_01()
        {
            if (m_IsHeath01)
            {
                Heart_01.GetComponent<SpriteRenderer>().sprite = heartFull;
            }
            else
            {
                Heart_01.GetComponent<SpriteRenderer>().sprite = heartEmpty;
            }
        }
        void IsHeart_02()
        {
            if (m_IsHeath02)
            {
                Heart_02.GetComponent<SpriteRenderer>().sprite = heartFull;
            }
            else
            {
                Heart_02.GetComponent<SpriteRenderer>().sprite = heartEmpty;
            }
        }
        void IsHeart_03()
        {
            if (m_IsHeath03)
            {
                Heart_03.GetComponent<SpriteRenderer>().sprite = heartFull;
            }
            else
            {
                Heart_03.GetComponent<SpriteRenderer>().sprite = heartEmpty;
            }
        }


        void UpdateHeart()
        {
            switch (Heaths)
            {

                case 3:
                    m_IsHeath01 = true;
                    m_IsHeath02 = true;
                    m_IsHeath03 = true;
                    break;


                case 2:
                    m_IsHeath01 = true;
                    m_IsHeath02 = true;
                    m_IsHeath03 = false;
                    break;
                case 1:
                    m_IsHeath01 = true;
                    m_IsHeath02 = false;
                    m_IsHeath03 = false;
                    break;
                case 0:
                    m_IsHeath01 = false;
                    m_IsHeath02 = false;
                    m_IsHeath03 = false;

                    break;
            }
        }

        public void IncrementHeaths(int bonus)
        {
            heaths = heaths + bonus;
        }



        #endregion


        



      /*  #region Tạo hoa quả
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
                if (FruitPrefabs != null && BomPrefabs != null && FruitPrefabs.Length > 0 )
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


    #endregion */


        #region ActivePlayer
    public void ActivePlayer()
    {
        if (IsComponentsNull()) return;
        if (m_curPlayer)
            Destroy(m_curPlayer.gameObject);

        var shopItems = shopMng.items;

        if (shopItems == null || shopItems.Length <= 0) return;

        var newPlayerPb = shopItems[GameData.Ins.curPlayerId].playerPrefab;


        if (newPlayerPb)
            m_curPlayer = Instantiate(newPlayerPb, new Vector3(-0.5f, -4.5f, 0f), Quaternion.identity);
            m_deadzone = Instantiate(m_deadzone, new Vector3(-0.9f, -4.7f, 0f), Quaternion.identity);
    }
        #endregion


        #region Hieu ung chay tien
        // Hieu ung chay tien
        public void AddScore()
        {
            int scorebonus = 100;
            m_newScore = m_curScore + scorebonus;
            StartCoroutine(CountingAnim(m_curScore, m_newScore, true));
        }


        IEnumerator CountingAnim(int m_curNum, int newNum, bool isUp)
        {
            int count = m_curNum;

            if (isUp)
            {
                while (count < newNum)
                {
                    count++;
                    if (guiMng.gameplayScoreTxt)
                        guiMng.gameplayScoreTxt.text = "Score: \n" + count.ToString("00000");
                    yield return new WaitForSeconds(rate);
                }
            }
            else
            {
                while (count > newNum)
                {
                    count--;
                    if (guiMng.gameplayScoreTxt)
                        guiMng.gameplayScoreTxt.text = "Score: \n" + count.ToString("00000");
                    yield return new WaitForSeconds(rate);


                }

            }
            guiMng.gameplayScoreTxt.text = "Score: \n" + newNum.ToString("00000");
            m_curScore = m_newScore;
        }
        #endregion





    }
}