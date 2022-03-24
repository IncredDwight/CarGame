using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(RawImage))]
public class ColorBox : MonoBehaviour
{
    [SerializeField] private Slider _hSlider;

    private RawImage _image;

    private int _textureWidth = 128;
    private int _textureHeight = 128;

    private void Awake()
    {
        _image = GetComponent<RawImage>();
    }

    private void Update()
    {
        RegenerateSVTexture();
    }

    private void RegenerateSVTexture()
    {
        float h = _hSlider != null ? _hSlider.value * 1 : 0;

        if (_image.texture != null)
            DestroyImmediate(_image.texture);

        var texture = new Texture2D(_textureWidth, _textureHeight);
        texture.wrapMode = TextureWrapMode.Clamp;
        texture.hideFlags = HideFlags.DontSave;

        for (int s = 0; s < _textureWidth; s++)
        {
            Color[] colors = new Color[_textureHeight];
            for (int v = 0; v < _textureHeight; v++)
            {
                colors[v] = Color.HSVToRGB(h, (float)s / _textureWidth, (float)v / _textureHeight);
            }
            texture.SetPixels(s, 0, 1, _textureHeight, colors);
        }
        texture.Apply();

        _image.texture = texture;

    }

}
