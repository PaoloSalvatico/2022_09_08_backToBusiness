using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private List<Transform> _spownPointsList;
    [SerializeField] private CheckPointController _checkPoint;

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

    private void InstatiateCheckPoint()
    {
        Instantiate(_checkPoint, _spownPointsList[count].position, _spownPointsList[count].rotation);
        _checkPoint.SpawnManager = this;
    }
}
