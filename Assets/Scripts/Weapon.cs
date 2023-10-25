using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
   [SerializeField]
    private float moveSpeed = 10f;
    public float damage = 1f;
    
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 1f); //시작하자마자 이 게임의 오브젝트를 1초 뒤에 없애라   
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.up * moveSpeed * Time.deltaTime;
    }
}
