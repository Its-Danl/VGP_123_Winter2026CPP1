using UnityEngine;

public class LevelStart : MonoBehaviour
{
    public Transform spawnPoint;
    void Start() => GameManager.Instance.SpawnPlayer(spawnPoint.position);
}
