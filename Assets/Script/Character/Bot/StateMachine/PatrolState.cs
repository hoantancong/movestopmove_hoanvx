using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PatrolState : IState<Bot>
{
    public void OnEnter(Bot bot)
    {
        bot.agent.isStopped = false;
        bot.animator.SetBool("IsRun", true);
    }

    public void OnExit(Bot bot)
    {
        bot.agent.isStopped = true;

    }

    public void OnUpdate(Bot bot)
    {
        bot.MoveToNextPatrolPoint();
    }


}
