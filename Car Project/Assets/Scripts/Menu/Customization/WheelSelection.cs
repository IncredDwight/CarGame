using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelSelection : MonoBehaviour
{
    [SerializeField] private CarSelection _carSelection;
    [SerializeField] private string _carWheelsFolderPath = "Prefabs/WheelModels/NN Wheels";

    private GameObject[,] _wheels;
    private int _currentWheelIndex;
    private int _maxWheelIndex;

    private void Start()
    {
        GameObject[] loadedWheels = Resources.LoadAll<GameObject>(_carWheelsFolderPath);
        Transform currentCar = _carSelection.Cars[_carSelection.GetCarIndex()].transform;

        _wheels = new GameObject[loadedWheels.Length, currentCar.GetChild(4).childCount];

        for (int i = 0; i < loadedWheels.Length; i++)
        {
            for(int j = 0; j < currentCar.GetChild(4).childCount; j++)
            {
                GameObject wheel = Instantiate(loadedWheels[i], currentCar.GetChild(4).GetChild(j));

                wheel.transform.localScale /= 2;
                if (j % 2 != 0)
                    wheel.transform.rotation = new Quaternion(0, 0, -180, 0);
                wheel.SetActive(false);

                _wheels[i, j] = wheel;
            }
        }

        _maxWheelIndex = _wheels.GetLength(0) - 1;
        ChangeWheelIndex(PlayerPrefs.HasKey("Wheels" + _carSelection.GetCarIndex()) ? PlayerPrefs.GetInt("Wheels" + _carSelection.GetCarIndex()) : 0);

        _carSelection.OnCarChanged.AddListener(delegate
        {
            SetWheelsParent(_carSelection.Cars[_carSelection.GetCarIndex()].transform);
            ChangeWheelIndex(PlayerPrefs.HasKey("Wheels" + _carSelection.GetCarIndex()) ? PlayerPrefs.GetInt("Wheels" + _carSelection.GetCarIndex()) : 0);
        });

        SetActiveWheelGroup(_currentWheelIndex, true);
    }

    public void SelectRight()
    {
        ChangeWheelIndex(_currentWheelIndex + 1);
    }

    public void SelectLeft()
    {
        ChangeWheelIndex(_currentWheelIndex - 1);
    }

    private void ChangeWheelIndex(int value)
    {
        _currentWheelIndex = value;
        if (_currentWheelIndex > _maxWheelIndex)
            _currentWheelIndex = 0;
        else if (_currentWheelIndex < 0)
            _currentWheelIndex = _maxWheelIndex;

        for (int i = 0; i < _wheels.GetLength(0); i++)
            SetActiveWheelGroup(i, false);
        SetActiveWheelGroup(_currentWheelIndex, true);

        PlayerPrefs.SetInt("Wheels" + _carSelection.GetCarIndex(), _currentWheelIndex);
    }

    private void SetActiveWheelGroup(int wheelIndex, bool isActive = false)
    {
        for (int j = 0; j < _wheels.GetLength(1); j++)
            _wheels[wheelIndex, j].SetActive(isActive);
    }
    
    private void SetWheelsParent(Transform car)
    {
        for(int i = 0; i < _wheels.GetLength(0); i++)
            for(int j = 0; j < _wheels.GetLength(1); j++)
                _wheels[i, j].transform.SetParent(car.GetChild(4).GetChild(j));
        
    }


}
