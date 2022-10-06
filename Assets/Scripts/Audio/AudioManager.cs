using System.Collections.Generic;
using UnityEngine;
using System;


public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    [SerializeField] private ScriptableAudio scriptableAudio;
    private List<AudioData> soundDict = new List<AudioData>();


    private bool isInitlialize;

    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        if (!isInitlialize)
        {
            isInitlialize = true;
            for (int i = 0; i < scriptableAudio.AudioData.Length; i++)
            {
                soundDict.Add(scriptableAudio.AudioData[i]);
                scriptableAudio.AudioData[i].Initialize(gameObject.AddComponent<AudioSource>());
            }
        }
    }

    public void PlayAudio(int index)
    {
        soundDict[index].PlaySound();
    }
}