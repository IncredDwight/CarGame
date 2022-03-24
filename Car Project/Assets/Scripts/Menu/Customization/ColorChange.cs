using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorChange : MonoBehaviour
{
    [SerializeField] private CarSelection _carSelection;

    [SerializeField] private Material[] _materials;

    [SerializeField] private Slider _sliderH;
    [SerializeField] private BoxSlider _boxSlider;

    private void OnEnable()
    {
        Vector3 hsv;
        Color.RGBToHSV(_materials[_carSelection.GetCarIndex()].color, out hsv.x, out hsv.y, out hsv.z);

        _sliderH.value = hsv.x;
        _boxSlider.SetValue(hsv.y, hsv.z);
    }

    private void Awake()
    {
        _sliderH.onValueChanged.AddListener(delegate { UpdateColor(); });
        _boxSlider.OnValueChanged.AddListener(delegate { UpdateColor(); });
    }

    private void UpdateColor()
    {
        _materials[_carSelection.GetCarIndex()].color = Color.HSVToRGB(_sliderH.value, _boxSlider.GetValue().x, _boxSlider.GetValue().y);
    }

}
