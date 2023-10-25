using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private GameObject coin;
    
    [SerializeField]
    private float moveSpeed = 10f;

    private float minY = -7f;
    public float hp = 1f;

    public void SetMoveSpeed(float moveSpeed) {
        this.moveSpeed = moveSpeed; //이름이 같을 때는 this.을 붙임
    }
    
    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.down * moveSpeed * Time.deltaTime;
        if (transform.position.y < minY) {
            Destroy(gameObject);

        }
    }

    private void OnTriggerEnter2D(Collider2D other) { //쓰레기가 충돌한 대상이 Wappon(미사일)일때는 현재 에너미의 체력으로부터 데미지를 뺴줌
        if (other.gameObject.tag == "Weapon") {
            Weapon weapon = other.gameObject.GetComponent<Weapon>();
            hp -= weapon.damage;
            if(hp <= 0) {
                if (gameObject.tag =="Boss") {
                    GameManager.instance.SetGameOver();
                }
               Destroy(gameObject);
               Instantiate(coin, transform.position, Quaternion.identity);
            }
            Destroy(other.gameObject); //충돌 대상인 미사일은 충돌되는 즉시 없어지도록 함
        }
    }
}
