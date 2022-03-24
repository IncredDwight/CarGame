using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateCar : MonoBehaviour
{
    [SerializeField] private string _carPrefabsFolderName = "Cars";
    [SerializeField] private string _carWheelsFolderPath = "Prefabs/WheelModels/NN Wheels";
    [SerializeField] private Transform _spawnTransform;

    private void Awake()
    {
        GameObject[] loadingCars = Resources.LoadAll<GameObject>("Prefabs/" + _carPrefabsFolderName);
        GameObject[] loadingWheels = Resources.LoadAll<GameObject>(_carWheelsFolderPath);
        int carIndex = (PlayerPrefs.HasKey("CarIndex")) ? PlayerPrefs.GetInt("CarIndex") : 0;
        int wheelIndex = (PlayerPrefs.HasKey("Wheels" + carIndex)) ? PlayerPrefs.GetInt("Wheels" + carIndex) : 0;

        GameObject car = Instantiate(loadingCars[carIndex], _spawnTransform.position, Quaternion.identity);
        for(int i = 0; i < car.transform.GetChild(4).childCount; i++)
        {
            GameObject wheel = Instantiate(loadingWheels[wheelIndex], car.transform.GetChild(4).GetChild(i));
            wheel.transform.localScale /= 2;
            if (i % 2 != 0)
                wheel.transform.rotation = new Quaternion(0, 0, -180, 0);

        }
        float tuningStance = PlayerPrefs.HasKey("TuningStance" + carIndex) ? PlayerPrefs.GetFloat("TuningStance" + carIndex) : 0;
        car.transform.GetChild(0).position = new Vector3(car.transform.GetChild(0).position.x, car.transform.GetChild(0).position.y + tuningStance, car.transform.GetChild(0).position.z);
    }
}
