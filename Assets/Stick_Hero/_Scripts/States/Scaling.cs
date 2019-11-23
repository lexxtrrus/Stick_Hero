using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scaling : State
{
    public Scaling(GameController gameController)
    {
        _gameController = gameController;
    }


    public override void ChangeState()
    {
        _gameController.CurrentState = new Rotate(_gameController);                
    }
}
