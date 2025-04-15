using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnAndShake : MonoBehaviour
{
    public GameObject prefab;
    public Vector3 mousePos;
    public Vector3 randomPos;
    public Vector3 screenInWorld;
    public CinemachineImpulseSource impulseSource;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        transform.position = mousePos;

        Vector2 screenSize = new Vector2(Screen.width, Screen.height);
        screenInWorld = Camera.main.ScreenToWorldPoint(screenSize);
    }

    public void SpawnDiamond()
    {
        randomPos.x = Random.Range(-(screenInWorld.x / 2), screenInWorld.x / 2);
        randomPos.y = Random.Range(-(screenInWorld.y / 2), screenInWorld.y / 2);
        Instantiate(prefab, randomPos, Quaternion.identity);
        impulseSource.GenerateImpulse();
    }
}
