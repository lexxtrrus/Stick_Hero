using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defeat : State
{
    public Defeat(GameController gameController)
    {
        _gameController = gameController;
    }


    public override void ChangeState()
    {
        _gameController.Restart();
    }
}
