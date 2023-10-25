using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private float minY = -7f; //-7위치보다 작아지면 coin은 사라짐

    // Start is called before the first frame update
    void Start()
    {
        Jump();
    }

    void Jump() {
        Rigidbody2D rigidBody = GetComponent<Rigidbody2D>();

        float randomJumpForce = Random.Range(4f, 8f);
        Vector2 jumpVelocity = Vector2.up * randomJumpForce;
        jumpVelocity.x = Random.Range(-2f, 2f); //왼쪽 또는 오른 쪽으로 코인이 이동
        rigidBody.AddForce(jumpVelocity, ForceMode2D.Impulse); //코인이 위쪽 방향으로 점프. 코인의 높이는 랜덤
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < minY) { //-7위치보다 작아지면 coin은 사라짐
            Destroy(gameObject);

        }
    }
}
