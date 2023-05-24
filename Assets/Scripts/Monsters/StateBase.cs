using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//public interface IState
//{

//}

public abstract class StateBase
{
    public abstract void Enter();
    public abstract void Update();
    public abstract void Exit();

}
