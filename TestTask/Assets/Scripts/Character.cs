using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class Character : MonoBehaviour
{
    public int strength;

    [SerializeField]
    protected TextMeshPro strengthText;


    protected virtual void Start()
    {
        SetStrengthText();
    }   

    protected void SetStrengthText()
    {
        strengthText.text = strength.ToString();
    }    

}
