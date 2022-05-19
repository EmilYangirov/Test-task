using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AliedCharacter : Character
{
    public void ChangeStats(int _strength)
    {
        strength = _strength;
        SetStrengthText();
    }
}
