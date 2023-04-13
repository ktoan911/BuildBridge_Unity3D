using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekingBrick : IState<EnemyCtrl>
{
    public void OnEnter(EnemyCtrl t)
    {
        t.IsMoving= true;
        t.animator.SetBool("IsRun", true);
    }

    public void OnExecute(EnemyCtrl t)
    {
        t.MoveToBrick();
        if(t.listBrickCharacter.Count >= 4)
        {
            t.ChangeState(new BuildStair());
            t.IsFoundBrick = false;
        }
    }

    public void OnExit(EnemyCtrl t)
    {

    }

}
