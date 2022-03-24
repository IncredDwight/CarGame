using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    [SerializeField] private GameObject _finishWindow;

    private void OnTriggerEnter(Collider collision)
    {
        VehicleController2017 vehicle = collision.GetComponent<VehicleController2017>();
        if(vehicle != null)
        {
            _finishWindow.SetActive(_finishWindow);
            vehicle.enginePower = 0;
        }
    }
}
