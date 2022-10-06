using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private CharacterAnimationController _animationController;
    [SerializeField] private Transform _chibiTransform;

    private Tween moveTween;
    private Tween lookAtTween;

    public void GoNextStack(Transform stackPosition)
    {
        moveTween?.Kill();
        lookAtTween?.Kill();

        _animationController.SwitchToRunAnimation(true);

        var pos = stackPosition.position;
        pos.y = 0;
        lookAtTween = _chibiTransform.DOLookAt(pos, 0.5f);
        moveTween = transform.DOMove(pos, 1f).OnComplete(() =>
        {
            _animationController.SwitchToRunAnimation(false);
            _chibiTransform.DORotate(Vector3.zero, 0.1f);
        });
    }
}