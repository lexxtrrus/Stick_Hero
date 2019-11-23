using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State 
{
    protected GameController _gameController;

    public abstract void ChangeState();
}
