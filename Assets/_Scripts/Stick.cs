using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stick : MonoBehaviour
{
    [SerializeField] private GameController _gameController;

    private Stick() { }
    private static Stick _instance;
    public static Stick Instance
    {
        get
        {
            return _instance;
        }
    }

    private float _zAngle = 0f;

    private bool _isScaling = false; // наверное лучше использовать автосвойства
    public bool IsScaling
    {
        get { return _isScaling; }
        set { _isScaling = value; }
    }

    private bool _isRotating = false; // наверное лучше использовать автосвойства
    public bool IsRotating
    {
        get { return _isRotating; }
        set { _isRotating = value; }
    }

    private void Awake()
    {
        _instance = this;
    }

    void Update()
    {
        if (_isScaling)
        {
            if (this.transform.localScale.y < 6f)
            {
                this.transform.localScale += new Vector3(0f, 4f, 0f) * Time.deltaTime;

                if (Input.GetMouseButtonUp(0))
                {
                    _isScaling = false;
                    _gameController.CurrentState.ChangeState();
                    _isRotating = true;
                    return;
                }
            }

            if (this.transform.localScale.y >= 6f)
            {
                _isScaling = false;
                _gameController.CurrentState.ChangeState();
                _isRotating = true;
                return;
            }
        }

        if (_isRotating)
        {
            if(_zAngle <= -90f)
            {
                _gameController.CurrentState.ChangeState();
                _isRotating = false;
                transform.localEulerAngles = new Vector3(0f, 0f, -90f);
                _zAngle = 0f;
                _gameController.StartMovement();
                return;
            }
            _zAngle -= Time.deltaTime * 90f * 3f;
            transform.localEulerAngles = new Vector3(0f, 0f, _zAngle);
        }
    }
}
