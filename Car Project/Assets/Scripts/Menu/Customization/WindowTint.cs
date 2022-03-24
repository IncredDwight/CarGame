using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowTint : MonoBehaviour
{
    [SerializeField] private CarSelection _carSelection;
    [SerializeField] private Material[] _glasses;

    [SerializeField] private Slider _slider;

    private const string NameId = "_Metallic";

    private void OnEnable()
    {
        _slider.value = _glasses[_carSelection.GetCarIndex()].GetFloat(NameId);
    }

    private void Awake()
    {
        _slider.onValueChanged.AddListener(delegate
        {
            UpdateTint();
        });
    }

    private void UpdateTint()
    {
        _glasses[_carSelection.GetCarIndex()].SetFloat(NameId, _slider.value);
    }
}
