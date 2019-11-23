using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wait : State
{
    public Wait(GameController gameController)
    {
        _gameController = gameController;
    }

    public override void ChangeState()
    {
        if (!Player.Instance.IsMoving && !Stick.Instance.IsRotating)
        {
            _gameController.CurrentState = new Scaling(_gameController);
            Stick.Instance.IsScaling = true;
        }        
    }    
}
