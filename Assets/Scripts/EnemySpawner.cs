using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
   [SerializeField]
   private GameObject[] enemies;

   [SerializeField]
   private GameObject boss;
   
    private float[] arrPosX = {-2.2f, -1.1f, 0f, 1.1f, 2.2f};

    [SerializeField]
    private float spawnInterval = 1.5f;
    
    // Start is called before the first frame update
    void Start()
    {
        StartEnemyRoutine();
    }

    void StartEnemyRoutine() {
        StartCoroutine("EnemyRoutine");
    }

    public void StopEnemyRoutine() {
        StopCoroutine("EnemyRoutine");
    }

    IEnumerator EnemyRoutine() {
        yield return new WaitForSeconds(3f);

        float moveSpeed = 5f;
        int spawnCount = 0;
        int enemyIndex = 0;
        while (true) {
            foreach (float posX in arrPosX) {
                SpawnEnemy(posX, enemyIndex, moveSpeed); 
            }

            spawnCount += 1;
            if (spawnCount % 10 == 0) { //10, 20, 30,... 10씩 커짐. 처음에는 Enemy1~Enemy7 순서대로 10번씩 나옴
                enemyIndex += 1;
                moveSpeed += 2;
            }

            if (enemyIndex >= enemies.Length) {
                SpawnBoss();
                enemyIndex = 0; //보스 등장 시점에는 Enemy를 초기화한다.
                moveSpeed = 5f; //보스 등장 시점에는 속도를 낮추어준다.
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnEnemy(float posX, int index, float moveSpeed) {
        Vector3 spawnPos = new Vector3(posX, transform.position.y, transform.position.z);
        
        //중간에 랜덤으로 한단계 더 체력(레벨이 높은)이 강한 Enemy를 나오게 하기
        if (Random.Range(0, 5) == 0) { // 0, 1, 2, 3, 4 -> 0 (20%). 20%의 확률로 다음번째 index Enemy가 나타나게 됨
            index += 1;
        }

        if (index >= enemies.Length) {
            index = enemies.Length - 1;
        }
        
        GameObject enemyObject = Instantiate(enemies[index], spawnPos, Quaternion.identity);
        Enemy enemy = enemyObject.GetComponent<Enemy>();
        enemy.SetMoveSpeed(moveSpeed);
    }

    void SpawnBoss() {
        Instantiate(boss, transform.position, Quaternion.identity);
    }
  }

