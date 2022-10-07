using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuccessPanel : Panel
{
    public void NextButton()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
}