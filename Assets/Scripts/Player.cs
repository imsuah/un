using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private GameObject [] weapons;
    private int weaponIndex = 0;

    [SerializeField]
    private Transform shootTransForm;

    [SerializeField]
    private float shootInterval =0.05f;
    private float lastShotTime = 0f;

    
    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        //float verticalInput = Input.GetAxisRaw("Vertical");
        Vector3 moveTo = new Vector3(horizontalInput, 0f, 0f);
        transform.position += moveTo * moveSpeed * Time.deltaTime;

       if (GameManager.instance.isGameOver == false) {
            Shoot();
       }
    }

    void Shoot() {
       
        if(Time.time - lastShotTime > shootInterval) {
            Instantiate(weapons[weaponIndex], shootTransForm.position, Quaternion.identity);
            lastShotTime = Time.time;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Boss") { //만약 Enemy에 닿으면 Game Over
            //Debug.Log("game Over");
            GameManager.instance.SetGameOver();
            Destroy(gameObject);
        } else if (other.gameObject.tag == "Coin") { //만약 충돌한 대상의 태그 값이 코인이라하면
            //Debug.Log("Coin +1");
            GameManager.instance.IncreaseCoin();
            Destroy(other.gameObject); //Player가 코인에 닿으면 코인은 사라짐
        }
    }
    
    public void Upgrade() {
        weaponIndex += 1;
        if (weaponIndex >= weapons.Length) {
            weaponIndex = weapons.Length - 1; //현재 weapons의 Length는 3개 있기떄문에 3. weapons를 3개 만 쓸수있도록 하기
        }
    }
}
