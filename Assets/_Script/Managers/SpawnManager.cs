using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private List<Transform> _spownPointsList;
    [SerializeField] private CheckPointController _checkPoint;

    [SerializeField] private GameObject _spawnArea;

    private int count = 0;

    private void Awake()
    {
        InstatiateCheckPoint();
    }

    public void Spawn()
    {
        count++;
        if (count >= _spownPointsList.Count) count = 0;

        InstatiateCheckPoint();
    }

    public void RandomSpawn()
    {
        //var x = Random.RandomRange(_spawnArea.transform.position.x)
        //Vector3 pos = ()
    }

    private void InstatiateCheckPoint()
    {
        var newCheckPoint = Instantiate(_checkPoint, _spownPointsList[count].position, _spownPointsList[count].rotation);
        newCheckPoint.SpawnManager = this;
    }
}
