using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ErrorView : MonoBehaviour
{
    [SerializeField] TMP_Text _description;
    public void Bind(Exception e)
    {
        _description.text = e.Message;
    }
}
