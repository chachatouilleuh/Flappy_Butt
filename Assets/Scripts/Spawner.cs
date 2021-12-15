using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    float minDelayBetweenSpawn = 2;
    [SerializeField]
    float maxDelayBetweenSpawn = 3;
    [SerializeField]
    int minVerticalDelta = -5;
    [SerializeField]
    int maxVerticalDelta = 5;
    [SerializeField]
    int minVertical = -700;
    [SerializeField]
    int maxVertical = 700;
    [SerializeField]
    int ratio = 100;
    [SerializeField]
    Door doorPrefab = default;
    [SerializeField]
    int doorCountAtStart = 5;
    [SerializeField]
    int startDoorInterval = 150;

    List<Door> doorList;
    int verticalDelta;

    void Start()
    {
        doorList = new List<Door>();
        SpawnStartDoor();
    }

    void SpawnStartDoor()
    {
        verticalDelta = 0;
        for (int i = doorCountAtStart -2; i >= 0; i--){
            SpawnDoor(startDoorInterval * i);
        }
            
    }

    void DestroyDoorObject()
    {
        doorList.RemoveAll(door => door == null);
        List<Door> notPassedDoorList = doorList.FindAll(door => !door.Passed);
        for (int i=0;i< notPassedDoorList.Count;i++)
        {
                Destroy(notPassedDoorList[i].gameObject);
        }
        doorList.Clear();
        SpawnStartDoor();
    }

    IEnumerator Spawn()
    {
        SpawnDoor();
        yield return new WaitForSeconds(Time.timeScale * Random.Range(minDelayBetweenSpawn, maxDelayBetweenSpawn));
        if (GameManager.CurrentState == GameManager.GameState.flying){
            StartCoroutine(Spawn());
        }
            
    }

    void SpawnDoor(int offset = 0) {
        Door door = Instantiate(doorPrefab);
        door.name = "Door_" + (doorList.Count+1);
        verticalDelta += Random.Range(minVerticalDelta, maxVerticalDelta) * ratio;
        verticalDelta = Mathf.Clamp(verticalDelta, minVertical, maxVertical);
        door.transform.position = transform.position + Vector3.up * verticalDelta + Vector3.left * offset;
        doorList.Add(door);
    }

    private void OnEnable()
    {
        GameManager.OnInit += LaunchSpawning;
        GameManager.OnRetry += RelaunchSpawning;
        GameManager.OnGameOver += StopSpawning;
    }
    private void OnDisable()
    {
        GameManager.OnInit -= LaunchSpawning;
        GameManager.OnRetry -= RelaunchSpawning;
        GameManager.OnGameOver -= StopSpawning;
    }

    void LaunchSpawning()
    {
        StartCoroutine(Spawn());
    }

    void RelaunchSpawning()
    {
        SceneManager.LoadScene (sceneName:"FlappyButt");
    }

    void StopSpawning()
    {
        StopAllCoroutines();
    }
}
