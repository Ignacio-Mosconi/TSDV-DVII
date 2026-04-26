using UnityEngine;

[CreateAssetMenu(fileName = "Level Setup Data", menuName = "Scriptable Objects/Level Set Up Data")]
public class LevelSetupData : ScriptableObject
{
    [field: Header("General Settings")]
    [field: SerializeField] public int LevelNumber { get; private set; }
    [field: SerializeField, Range(0, 100)] public int EnemiesCount { get; private set; }
    [field: SerializeField, Range(0, 100)] public int HealthPacksCount { get; private set; }
    [field: SerializeField, Range(0, 100)] public int AmmoPacksCount { get; private set; }
    [field: SerializeField, Range(0f, 10f)] public float SpawnRadius { get; private set; }

    [field: Header("Prefabs")]
    [field: SerializeField] public GameObject EnemyPrefab { get; private set; }
    [field: SerializeField] public GameObject HealthPackPrefab { get; private set; }
    [field: SerializeField] public GameObject AmmoPackPrefab { get; private set; }
}