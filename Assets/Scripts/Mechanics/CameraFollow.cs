using System;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    [SerializeField] private float minXPos;
    [SerializeField] private float maxXPos;

    [SerializeField] private Transform target;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameManager.Instance.OnPlayerSpawned += OnPlayerSpawnedCallback;
    }

    private void OnPlayerSpawnedCallback(PlayerController player)
    {
        target = player.transform;
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        if (!target) return;
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(target.position.x, minXPos,maxXPos);
        transform.position = pos;
    }
}
