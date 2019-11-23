using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : State
{
    public Rotate(GameController gameController)
    {
        _gameController = gameController;
    }


    public override void ChangeState()
    {
        if (Stick.Instance.IsRotating)
        {
            _gameController.CurrentState = new Movement(_gameController);
        }
    }
}
