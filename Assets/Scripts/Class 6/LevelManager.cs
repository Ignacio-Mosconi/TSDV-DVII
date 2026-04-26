using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviourSingleton<LevelManager>
{
    [SerializeField] private LevelSetupData[] levels;

    private Dictionary<string, List<GameObject>> spawnedObjects;


    void Awake ()
    {
        spawnedObjects = new Dictionary<string, List<GameObject>>()
        {
            ["Enemies"] = new List<GameObject>(),
            ["HealthPacks"] = new List<GameObject>(),
            ["AmmoPacks"] = new List<GameObject>()
        };
    }

    
    private void CleanLevel ()
    {
        foreach (string key in spawnedObjects.Keys)
        {
            foreach (GameObject obj in spawnedObjects[key])
                Destroy(obj);

            spawnedObjects[key].Clear();
        }
    }

    private Vector3 GetRandomSpawnPoint (float radius, float offsetY)
    {
        Vector2 randomInsideCircle = UnityEngine.Random.insideUnitCircle * radius;
        return transform.position + new Vector3(randomInsideCircle.x, offsetY, randomInsideCircle.y);
    }

    private void SpawnObjects (string typeKey, GameObject prefab, int count, float radius)
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 spawnPoint = GetRandomSpawnPoint(radius, (typeKey == "Enemy") ? 1f : 0.5f);
            spawnedObjects[typeKey].Add(Instantiate(prefab, spawnPoint, Quaternion.identity));
        }
    }

    public void LoadLevel (int level)
    {
        try
        {
            CleanLevel();

            LevelSetupData levelSetupData = Array.Find(levels, l => l.LevelNumber == level);

            if (levelSetupData == null)
                throw new Exception($"Level {level} not found for loading!");

            SpawnObjects("Enemies", levelSetupData.EnemyPrefab, levelSetupData.EnemiesCount, levelSetupData.SpawnRadius);
            SpawnObjects("HealthPacks", levelSetupData.HealthPackPrefab, levelSetupData.HealthPacksCount, levelSetupData.SpawnRadius);
            SpawnObjects("AmmoPacks", levelSetupData.HealthPackPrefab, levelSetupData.HealthPacksCount, levelSetupData.SpawnRadius);

            Debug.Log($"Level {level} loaded!");
        }
        catch (Exception exception)
        {
            Debug.LogError(exception.Message);
            Application.Quit();
        }
    }
}