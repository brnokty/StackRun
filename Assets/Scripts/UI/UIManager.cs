using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GamePanel gamePanel;
    [SerializeField] private Panel successPanel;
    [SerializeField] private Panel failPanel;

    public static UIManager Instance;

    private void Awake()
    {
        Instance = this;
    }


    public void Win()
    {
        gamePanel.Disappear();
        successPanel.Appear();
    }

    public void Fail()
    {
        gamePanel.Disappear();
        failPanel.Appear();
    }
}