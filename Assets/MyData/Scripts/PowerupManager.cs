using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PowerupManager : MonoBehaviour
{
    public static PowerupManager Instance { get; private set; }

    [SerializeField] private float spawnDelay;
    [SerializeField] private int maxSpawnCount;

    [SerializeField] private List<GameObject> powerupsList;
    [SerializeField] private List<Transform> spawnpointsList;

    private float currentSpawnTime;
    private int currentSpawnCount = 0;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        currentSpawnTime = 0;
        currentSpawnCount = 0;
    }

    private void Update()
    {
        currentSpawnTime += Time.deltaTime;

        if (currentSpawnTime >= spawnDelay && currentSpawnCount < maxSpawnCount)
        {
            SpawnPowerup();
            currentSpawnTime = 0;
        }
    }

    private void SpawnPowerup()
    {
        currentSpawnCount++;

        int randomSpawnIndex = Random.Range(0, spawnpointsList.Count);
        int randomPowerupIndex = Random.Range(0, powerupsList.Count);
        var spawnPoint = spawnpointsList[randomSpawnIndex];
        var powerup = powerupsList[randomPowerupIndex];

        var spawnedPowerup = Instantiate(powerup, spawnPoint.position, spawnPoint.rotation).GetComponent<PowerupController>();
        spawnedPowerup.SetSpawnPos(spawnPoint);
        spawnpointsList.Remove(spawnPoint);
        Debug.Log(spawnedPowerup.name + "Spawned Power-up");
    }

    public void ApplyPowerupToPlayers(PaddleController player, PowerupController powerupController)
    {
        if (player == null)
        {
            Debug.Log("Somthing is Wrong!");
            return;
        }

        currentSpawnCount--;
        currentSpawnTime = 0;
        spawnpointsList.Add(powerupController.GetSpawnPos());

        switch (powerupController.GetPowerupType())
        {
            case PowerupType.FastMove:
                GameManager.GetOpponents(player).ForEach(x => x.SpeedUp(duration: 10f));
                break;

            case PowerupType.Freeze:
                GameManager.GetOpponents(player).ForEach(x => x.FreezePaddle(duration: 3f));
                break;

            case PowerupType.Resize:
                player.ResizePaddle(duration: 10f);
                break;
        }
    }
}
