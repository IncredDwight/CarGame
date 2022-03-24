using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CarSelection : MonoBehaviour
{
    [SerializeField] private Transform _carsTransform;
    [SerializeField] private string _carPrefabsFolderName = "MenuCars";

    [HideInInspector]
    public List<GameObject> Cars = new List<GameObject>();
    private int _carIndex;
    private int _maxCarIndex;

    public UnityEvent OnCarChanged;

    private void Awake()
    {
        GameObject[] loadingCars = Resources.LoadAll<GameObject>("Prefabs/" + _carPrefabsFolderName);
        for(int i = 0; i < loadingCars.Length; i++)
        {
            Cars.Add(Instantiate(loadingCars[i], _carsTransform));
            Cars[i].transform.position = _carsTransform.position;
            Cars[i].SetActive(false);
        }

        _carIndex = (PlayerPrefs.HasKey("CarIndex")) ? PlayerPrefs.GetInt("CarIndex") : 0;
        Cars[_carIndex].SetActive(true);
        _maxCarIndex = Cars.Count - 1;

        for(int i = 0; i < Cars.Count; i++)
        {
            Vector3 carPosition = Cars[i].transform.position;
            float tuningStance = PlayerPrefs.HasKey("TuningStance" + i) ? PlayerPrefs.GetFloat("TuningStance" + i) : 0;
            Cars[i].transform.GetChild(0).position = new Vector3(carPosition.x, carPosition.y + tuningStance, carPosition.z);
        }
    }

    public void SelectRight()
    {
        AdjustCarIndex(1);
    }

    public void SelectLeft()
    {
        AdjustCarIndex(-1);
    }

    public int GetCarIndex()
    {
        return _carIndex;
    }

    private void AdjustCarIndex(int amount)
    {
        _carIndex += amount;

        if (_carIndex > _maxCarIndex)
            _carIndex = 0;
        else if (_carIndex < 0)
            _carIndex = _maxCarIndex;

        PlayerPrefs.SetInt("CarIndex", _carIndex);
        OnCarChanged?.Invoke();

        for (int i = 0; i < Cars.Count; i++)
            Cars[i].SetActive(false);
        Cars[_carIndex].SetActive(true);
    }

}
