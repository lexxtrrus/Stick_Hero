using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    #region Singleton

    private GameController() { }
    private static GameController _instance;
    public static GameController Instance
    {
        get
        {
            if (_instance == null) _instance = new GameController();
            return _instance;
        }
    }
    #endregion


    [Header("Set In Inspector")]
    [SerializeField] private Player _player;
    [SerializeField] private Stick _stick;
    [SerializeField] private GameObject _platformPrefab;
    [SerializeField] private Text _textRestart;
    [SerializeField] private Text _textScore;


    [Header("Set Dynamically")]
    [SerializeField] private GameObject _currentPlatform;    
    [SerializeField] private GameObject _nextPlatform;
    

    private State _currentState;
    private int _score = 0;


    #region Properties
    public State CurrentState
    {
        get { return _currentState; }
        set { _currentState = value; }
    }

    public GameObject NextPlatform => _nextPlatform;
    public GameObject CurrentPlatform => _currentPlatform;
    public Text TextRestart => _textRestart;
    #endregion

    void Awake()
    {
        _instance = this;
        CurrentState = new Wait(this);
        _currentPlatform = Instantiate<GameObject>(_platformPrefab);
        _currentPlatform.transform.position = new Vector3(0, -2.25f, 0f);
        _nextPlatform = Instantiate<GameObject>(_platformPrefab);
        _nextPlatform.transform.position = new Vector3(3f, -2.25f, 0f);
        _textRestart.enabled = false;
        _textScore.text = _score.ToString();
    }
    void Update()
    {
        if (!Input.GetMouseButton(0)) return;
        CurrentState.ChangeState();
    }

    public void StartMovement()
    {
        float _curPosX = _currentPlatform.transform.position.x + _currentPlatform.transform.localScale.x * 0.5f; // правый край текущей платформы
        float _nextPosX = _nextPlatform.transform.position.x - _nextPlatform.transform.localScale.x * 0.5f; // левый край следующей платформы

        float _minDist = _nextPosX - _curPosX - _player.transform.localScale.x * 0.5f; // мин расстрояние для успеха
        float _maxDist = _nextPosX + _nextPlatform.transform.localScale.x * 0.5f + 0.05f; // макс расстрояние для успеха

        if (_stick.transform.localScale.y < _minDist || _stick.transform.localScale.y > _maxDist)
        {
            _player.IsMoving = true;
            _player.IsFalling = true;
            _player.targerPosition = _stick.transform.localScale.y + 0.5f + _player.transform.localScale.x * 0.5f;
        }
        else
        {
            _player.IsMoving = true;
            _player.IsFalling = false;
            _player.targerPosition = _nextPlatform.transform.position.x;
        }
    }

    public void CreateNewPlatform()
    {
        Destroy(_currentPlatform);
        _currentPlatform = _nextPlatform;
        _nextPlatform =  Instantiate<GameObject>(_platformPrefab); ;

        float _xRandomPos = Random.Range(1.5f, 4.5f);
        float _xrandomScale = Random.Range(0.5f, 1.2f);

        _nextPlatform.transform.position = new Vector3(_xRandomPos, -2.25f, 0f);
        _nextPlatform.transform.localScale = new Vector3(_xrandomScale, 4f,0f);

        _stick.transform.position = _currentPlatform.GetComponent<Platform>().GetStickPointPosition();
        _stick.transform.localScale = new Vector3(0.2f, 0f, 1f);
        _stick.transform.localEulerAngles = Vector3.zero;

        _score++;
        _textScore.text = _score.ToString();

        _currentState.ChangeState();
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
