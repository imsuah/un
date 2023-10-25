using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
   public static GameManager instance = null;

   [SerializeField]
   private TextMeshProUGUI text;
   
   [SerializeField]
   private GameObject gameOverPanel;
  
   private int coin = 0; //코인을 처음에는 0으로 값을 초기화

   [HideInInspector]
   public bool isGameOver = false;

   void Awake() {
    if (instance == null) {
        instance = this;
    }
    
   }

   public void IncreaseCoin() {
       coin += 1;
       text.SetText(coin.ToString()); //코인 증가할떄마다 코인 값을 text에다가 업데이트

       if (coin % 15 == 0) { //코인을 지정한 값의 갯수만큼 먹으면 다음 레벨 weapon으로 업그레이드 ex) 15, 30,35, ...
           Player player = FindObjectOfType<Player>();
           if (player != null) {
               player.Upgrade(); 
           }
        }
    }

    public void SetGameOver() {
        isGameOver = true;

        EnemySpawner enemySpawner = FindObjectOfType<EnemySpawner>();
        if(enemySpawner != null) {
            enemySpawner.StopEnemyRoutine();
        }

        Invoke("ShowGameOverPanel", 1f);
    }

    void ShowGameOverPanel() {
        gameOverPanel.SetActive(true);
    }

    public void PlayAgain() {
        SceneManager.LoadScene("SampleScene");
    }
}
