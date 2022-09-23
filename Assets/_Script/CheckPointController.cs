using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointController : MonoBehaviour
{
    private SpawnManager _spawnManager;

    [SerializeField] private float _addingTime;

    public SpawnManager SpawnManager { get => _spawnManager; set => _spawnManager = value; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerMovement player))
        {
            player.TimeManager.AddTime(_addingTime);
            _spawnManager.RandomSpawn();
            Destroy(gameObject);
        }
    }
}
