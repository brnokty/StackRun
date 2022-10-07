using System;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using Random = UnityEngine.Random;

public enum Direction
{
    Left,
    Right,
    Front,
    Back
}

public class PieceController : MonoBehaviour
{
    [SerializeField] private ColorController colorData;
    [SerializeField] private CameraHandler cameraHandler;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Transform reference;
    [SerializeField] private MeshRenderer referenceMesh;

    [SerializeField] private GameObject fallingPrefab;
    [SerializeField] private GameObject standPrefab;

    [SerializeField] private Transform last;

    [SerializeField] [Range(1, 5)] private float speed;
    [SerializeField] [Range(1, 2)] private float limit;

    private bool _isForward;

    private bool _isStop;

    private int _score;


    private void UpdateText()
    {
        UIManager.Instance.gamePanel.SetPointText(_score);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            Click();
    }

    private void LateUpdate()
    {
        if (_isStop) return;

        var position = transform.position;
        var direction = _isForward ? 1 : -1;
        var move = speed * Time.deltaTime * direction;


        position.x += move;


        //Limit And Turn

        if (position.x < -limit || position.x > limit)
        {
            position.x = Mathf.Clamp(position.x, -limit, limit);
            _isForward = !_isForward;
        }


        transform.position = position;
    }

    private void DivideObject(float value)
    {
        print("Value : " + value);

        if (Mathf.Abs(value) < 0.1f)
            AudioManager.Instance.PlayAudio(0);
        bool isFirstFalling = value > 0;

        var falling = Instantiate(fallingPrefab).transform;
        var stand = Instantiate(standPrefab).transform;

        //Size
        var fallingSize = reference.localScale;
        fallingSize.x = Math.Abs(value);

        falling.localScale = fallingSize;

        var standSize = reference.localScale;
        standSize.x = reference.localScale.x - Math.Abs(value);

        stand.localScale = standSize;

        var minDirection = Direction.Left;
        var maxDirection = Direction.Right;

        //Position
        var fallingPosition = GetPositionEdge(referenceMesh, isFirstFalling ? minDirection : maxDirection);
        var fallingMultiply = (isFirstFalling ? 1 : -1);
        fallingPosition.x += (fallingSize.x / 2) * fallingMultiply;

        falling.position = fallingPosition;

        var standPosition = GetPositionEdge(referenceMesh, !isFirstFalling ? minDirection : maxDirection);
        var standMultiply = (!isFirstFalling ? 1 : -1);
        standPosition.x += (standSize.x / 2) * standMultiply;

        stand.position = standPosition;

        //Color
        var color = colorData.GetColor(_score);
        stand.GetComponent<MeshRenderer>().material.color = color;
        falling.GetComponent<MeshRenderer>().material.color = color;
        referenceMesh.material.color = color;

        last = stand;
    }

    private Vector3 GetPositionEdge(MeshRenderer mesh, Direction direction)
    {
        var extents = mesh.bounds.extents;
        var position = mesh.transform.position;

        switch (direction)
        {
            case Direction.Left:
                position.x += -extents.x;
                break;
            case Direction.Right:
                position.x += extents.x;
                break;
        }

        return position;
    }


    public void Click()
    {
        _isStop = true;

        var distance = last.position - transform.position;

        if (IsFail(distance))
        {
            Debug.Log("game over");
            playerController.Fail(transform);
            gameObject.AddComponent<Rigidbody>();
            return;
        }

        DivideObject(distance.x);
        playerController.GoNextStack(last);


        var newPosition = last.position;
        newPosition.z += transform.localScale.z;

        newPosition.x = (Random.Range(0, 2) > 0 ? 1 : -1) * limit;
        transform.position = newPosition;

        transform.localScale = last.localScale;

        _isStop = false;

        _score++;
        UpdateText();
    }

    private bool IsFail(Vector3 distance)
    {
        var origin = transform.localScale.x;
        var current = Mathf.Abs(distance.x);

        return current >= origin;
    }

    public void Win()
    {
        gameObject.SetActive(false);
    }
}