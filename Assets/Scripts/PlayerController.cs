using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private CharacterAnimationController _animationController;
    [SerializeField] private Transform _chibiTransform;
    [SerializeField] private PieceController pieceController;

    private Tween moveTween;
    private Tween lookAtTween;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            Success(other.transform);
        }
    }

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

    public void Fail(Transform fallPos)
    {
        moveTween?.Kill();
        lookAtTween?.Kill();

        _animationController.SwitchToRunAnimation(true);

        var pos = fallPos.position;
        pos.y = 0;
        lookAtTween = _chibiTransform.DOLookAt(pos, 0.5f);
        _chibiTransform.gameObject.AddComponent<Rigidbody>();
        moveTween = transform.DOMove(pos, 1f).OnComplete(() =>
        {
            _animationController.SwitchToRunAnimation(false);
            _animationController.SwitchToFallAnimation(true);
            UIManager.Instance.Fail();
        });
    }


    public void Success(Transform finish)
    {
        moveTween?.Kill();
        lookAtTween?.Kill();
        pieceController.Win();
        _animationController.SwitchToRunAnimation(true);

        var pos = finish.position;
        pos.y = 0;
        lookAtTween = _chibiTransform.DOLookAt(pos, 0.5f);
        moveTween = transform.DOMove(pos, 1f).OnComplete(() =>
        {
            _animationController.SwitchToRunAnimation(false);
            _animationController.SwitchToDanceAnimation(true);
            _chibiTransform.SetParent(null);
            _chibiTransform.DORotate(new Vector3(0, 180, 0), 0.1f);
            StartCoroutine(RotateCoroutine());
            UIManager.Instance.Win();
        });
    }


    private IEnumerator RotateCoroutine()
    {
        while (true)
        {
            transform.Rotate(0, 10f * Time.deltaTime, 0);
            yield return null;
        }
    }
}