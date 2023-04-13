using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState<EnemyCtrl>
{
    public void OnEnter(EnemyCtrl t)
    {
        t.IsMoving= false;
        t.animator.SetBool("IsRun", false);
    }

    public void OnExecute(EnemyCtrl t)
    {
        t.ChangeState(new SeekingBrick());
    }

    public void OnExit(EnemyCtrl t)
    {

    }

}
