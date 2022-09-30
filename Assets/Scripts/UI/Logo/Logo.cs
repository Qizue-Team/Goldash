using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Logo : MonoBehaviour
{
    public static event Action OnLogoAnimationCompleted;

    [Header("References")]
    [SerializeField]
    private Image phoenixImage;
    [SerializeField]
    private Image dragonImage;
    [SerializeField]
    private TextMeshProUGUI logoText;

    [Header("Logo Settings")]
    [SerializeField]
    private float appearDuration = 2.0f;

    private Color _color;

    private void Start()
    {
        _color = Color.white;
        _color.a = 0.0f;
        ApplyColor();
        StartCoroutine(COFade());
    }

    private IEnumerator COFade()
    {
        while(_color.a < 1)
        {
            _color.a+=0.005f;
            ApplyColor();
            yield return null;
        }

        yield return new WaitForSeconds(appearDuration);

        while (_color.a > 0)
        {
            _color.a -= 0.01f;
            ApplyColor();
            yield return null;
        }

        OnLogoAnimationCompleted?.Invoke();
    }

    private void ApplyColor()
    {
        phoenixImage.color = _color;
        dragonImage.color = _color;
        logoText.color = _color;
    }
}
