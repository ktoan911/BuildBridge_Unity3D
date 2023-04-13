using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildStair : IState<EnemyCtrl>
{
    public void OnEnter(EnemyCtrl t)
    {
        t.animator.SetBool("IsRun", true);
    }

    public void OnExecute(EnemyCtrl t)
    {
        
        t.SetGoalPickStair();

        if (t.CheckOnGroundActive() && t.listBrickCharacter.Count < 4)
        {
            t.ChangeState(new SeekingBrick());
        }

    }

    public void OnExit(EnemyCtrl t)
    {

    }

}
