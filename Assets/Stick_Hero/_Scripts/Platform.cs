using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private GameObject _stickPoint;

    public Vector3 GetStickPointPosition()
    {
        return _stickPoint.transform.position;
    }
}
