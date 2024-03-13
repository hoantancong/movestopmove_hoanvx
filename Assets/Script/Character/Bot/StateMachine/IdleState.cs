using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : MonoBehaviour, IState<Bot>
{
    public void OnEnter(Bot bot)
    {
        bot.Idle();
    }

    public void OnExit(Bot t)
    {

    }

    public void OnUpdate(Bot bot)
    {
        if (bot.FireTarget() != null&&Ultility.GetRandom5050())
        {
            bot.ChangeState(BotState.attack);
        }
        else
        {
            bot.ChangeState(BotState.patrol);
        }
    }

    // Start is called before the first frame update

}
