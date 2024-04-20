using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterGun : MonoBehaviour
{
    // public Transform shootingPoint; // 指定從哪個點發射水滴
    public GameObject holdingPlayer; // 玩家位置
    public GameObject waterDropPrefab; // 水滴預製物
    public float shootingForce = 10f; // 射擊力度
    public float shootingRate = 5f; // 射擊頻率，每秒發射次數

    private float timeUntilNextShot = 0f;

    void Update(){
        // Press space to shoot
        if( timeUntilNextShot <= 0f && Input.GetKeyDown(KeyCode.Space)){
            // Shoot(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));
            timeUntilNextShot = 1f / shootingRate;
        }
        timeUntilNextShot -= Time.deltaTime;
    }



    void Shoot(Vector2 direction)
    {
        if (direction == Vector2.zero) return; // 如果方向為零，則不射擊（避免射擊方向為零）
        // Debug.Log("Shooting in direction: " + direction);
        GameObject drop = Instantiate(waterDropPrefab, holdingPlayer.transform.position, Quaternion.identity);
        Rigidbody2D rb = drop.GetComponent<Rigidbody2D>();
        rb.velocity = direction.normalized * shootingForce;
    }
}