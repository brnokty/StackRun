using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GamePanel : Panel
{
    [SerializeField] private TextMeshProUGUI pointTXT;


    public void SetPointText(int value)
    {
        pointTXT.text = value.ToString();
    }
}