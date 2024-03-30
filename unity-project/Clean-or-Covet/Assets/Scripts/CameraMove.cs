using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public GameObject player;
    public GameObject camera;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(player);
        Debug.Log(camera);
    }

    // Update is called once per frame
    void Update()
    {
        camera.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);
    }
}
