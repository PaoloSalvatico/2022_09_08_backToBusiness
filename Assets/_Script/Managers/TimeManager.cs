using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timer;
    [SerializeField] private float _startingTime;
    [SerializeField] private Color _greenColor;
    [SerializeField] private Color _redColor;
    [SerializeField] private GameObject _losePanel;

    private float _actualTime;

    public float ActualTime { get => _actualTime; set => _actualTime = value; }

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        _losePanel.SetActive(false);
        _actualTime = _startingTime;
        _timer.text = _actualTime.ToString();
        _timer.color = _greenColor;
        StartCoroutine(Timer());
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(1);
        _actualTime--;
        _timer.text = _actualTime.ToString();
        if (_actualTime < 6) _timer.color = _redColor;
        if(_actualTime > 0)
        {
            StartCoroutine(Timer());
        }
        else
        {
            _losePanel.SetActive(true);
        }
    }

    public void AddTime(float add)
    {
        _actualTime += add;
    }
}
