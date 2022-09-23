using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Manager")]
    [SerializeField] private bool _isRandomSpawn;

    [Header("Ordinated Spawn")]
    [SerializeField] private List<Transform> _spawnPointsList;
    [SerializeField] private CheckPointController _checkPoint;

    [Header("Random Spawn")]
    [SerializeField] private GameObject _spawnAreaCenter;
    [SerializeField] private float _offsetX;
    [SerializeField] private float _offsetZ;

    private int count = 0;

    private void Awake()
    {
        if(_isRandomSpawn)
        {
            RandomSpawn();
            return;
        }

        InstatiateCheckPoint(_spawnPointsList[count].position, _spawnPointsList[count].rotation);
    }

    public void Spawn()
    {
        count++;
        if (count >= _spawnPointsList.Count) count = 0;

        InstatiateCheckPoint(_spawnPointsList[count].position, _spawnPointsList[count].rotation);
    }

    public void RandomSpawn()
    {
        var x = Random.Range(_spawnAreaCenter.transform.position.x - _offsetX, _spawnAreaCenter.transform.position.x + _offsetX);
        var z = Random.Range(_spawnAreaCenter.transform.position.z - _offsetZ, _spawnAreaCenter.transform.position.z + _offsetZ);
        Vector3 pos = new Vector3(x, 1, z);

        InstatiateCheckPoint(pos, Quaternion.identity);
    }

    private void InstatiateCheckPoint(Vector3 pos, Quaternion rot)
    {
        var newCheckPoint = Instantiate(_checkPoint, pos, rot);
        newCheckPoint.SpawnManager = this;
    }
}
