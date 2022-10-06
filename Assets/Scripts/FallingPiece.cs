using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPiece : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 2f);
    }
}