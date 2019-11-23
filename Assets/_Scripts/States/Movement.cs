using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : State
{
    public Movement(GameController gameController)
    {
        _gameController = gameController;
    }

    public override void ChangeState()
    {
        _gameController.CurrentState = new Wait(_gameController);
    }
}