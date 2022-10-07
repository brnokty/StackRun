using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationController : MonoBehaviour
{
    [HideInInspector] public Animator _animator;

    private bool flip;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }


    private void SetTrigger(string parameter)
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName(parameter))
            return;

        _animator.SetTrigger(parameter);
    }

    private void SetBool(string parameter, bool value)
    {
        _animator.SetBool(parameter, value);
    }

    private void SetFloat(string parameter, float value)
    {
        _animator.SetFloat(parameter, value);
    }


    //Run
    public void SwitchToRunAnimation(bool value)
    {
        SetBool("Run", value);
    }

    //Fall
    public void SwitchToFallAnimation(bool value)
    {
        SetBool("Fall", value);
    }


    //Dance
    public void SwitchToDanceAnimation(bool value)
    {
        SetBool("Dance", value);
    }
}