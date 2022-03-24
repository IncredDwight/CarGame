using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TuningStance : MonoBehaviour
{
    [SerializeField] private CarSelection _carSelection;
    [SerializeField] private Slider _slider;

    private void OnEnable()
    {
        _slider.value = PlayerPrefs.HasKey("TuningStance" + _carSelection.GetCarIndex()) ? PlayerPrefs.GetFloat("TuningStance" + _carSelection.GetCarIndex()) : 0;
    }

    private void Awake()
    {
        _slider.onValueChanged.AddListener(delegate
        {
            UpdateTiningStance();
        });
    }

    private void UpdateTiningStance()
    {
        PlayerPrefs.SetFloat("TuningStance" + _carSelection.GetCarIndex(), _slider.value);
        Transform _currentCarTransform = _carSelection.Cars[_carSelection.GetCarIndex()].transform;
        _currentCarTransform.GetChild(0).position = new Vector3(_currentCarTransform.position.x, _currentCarTransform.position.y + _slider.value, _currentCarTransform.position.z);
    }

}
