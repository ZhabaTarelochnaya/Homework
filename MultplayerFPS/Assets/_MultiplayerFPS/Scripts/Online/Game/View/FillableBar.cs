using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[ExecuteAlways]
public class FillableBar : MonoBehaviour
{
    [SerializeField, Range(0f, 1f)] float _fillAmount = 1;
    Image _filler;

    public float FillAmount
    {
        get => _fillAmount;
        set
        {
            _fillAmount = Mathf.Clamp01(value);
            _filler.fillAmount = _fillAmount;
        }
    }
    void Awake()
    {
        _filler = transform.GetChild(0).GetComponent<Image>();
        _filler.fillAmount = _fillAmount;
    }
    #if UNITY_EDITOR
    void Update()
    {
        _filler.fillAmount = _fillAmount;
    }
    #endif
}
