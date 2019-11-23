using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] GameController _gameController;
    [SerializeField] Stick _stick;
    
    public float targerPosition { get; set; }    
    public bool IsMoving { get; set; }
    public bool IsFalling { get; set; }

    void Awake()
    {
        targerPosition = 0f;
        IsMoving = false;
        IsFalling = false;
    }



    void Update()
    {
        if (IsMoving)
        {
            _stick.transform.transform.Translate(new Vector3(0f, -9f, 0f) * Time.deltaTime);
            _gameController.CurrentPlatform.transform.Translate(new Vector3(-9f, 0f, 0f) * Time.deltaTime);
            _gameController.NextPlatform.transform.Translate(new Vector3(-9f, 0f, 0f) * Time.deltaTime);            
        }

        if (_gameController.CurrentPlatform.transform.position.x >= -targerPosition) return;

        IsMoving = false;
        
        if (IsFalling)
        {
            _stick.GetComponent<Stick>().enabled = false;
            transform.Translate(Vector3.down * Time.deltaTime * 10f);
            
            if(transform.position.y < -4f)
            {
                IsFalling = false;                
                _gameController.CurrentState = new Defeat(_gameController);
                _gameController.TextRestart.enabled = true;
                this.gameObject.SetActive(false);
            }
        }
        else
        {
            _gameController.CreateNewPlatform();
        }
    }
}
