using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace GameBasic
{


    public class GameManager : MonoBehaviour,IComponentCheck
    {

        public static GameManager Ins;

       public float m_spawnTime;
        public Fruit[] FruitPrefabs;
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
             guiMng.UpdateMainCoin();
        }

       

          public bool IsComponentsNull()
            {
                  return guiMng == null || shopMng == null || auCtr == null ;
                
            }
        
        public void PlayGameButton()
            {
               if (IsComponentsNull()) return;
                ActivePlayer();
                StartCoroutine(SpawnFruit());
                guiMng.ShowGameGUI(true);
                guiMng.UpdateGamePlayCoin();
                auCtr.PlayBgm();
            }

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

        IEnumerator SpawnFruit()
        {
            while(!m_IsGameOver)
            {
                if(FruitPrefabs != null && FruitPrefabs.Length >0 )
                {
                    int randIdx = Random.Range(0, FruitPrefabs.Length);
                    Fruit fruitPrefab = FruitPrefabs[randIdx];
                  
                    if(fruitPrefab)
                    {
                        Instantiate(fruitPrefab, new Vector3((Random.Range(-2,2)), 8, 0), Quaternion.identity);
                    }
                   
                }
                yield return new WaitForSeconds(m_spawnTime);
           }
            
        }
    }
}
