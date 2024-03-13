using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : MonoBehaviour, IState<Bot>
{
    public void OnEnter(Bot bot)
    {
        //fire
        bot.Fire(bot.aimedTarget.position);
    }

    public void OnExit(Bot bot)
    {
     
    }

    public void OnUpdate(Bot bot)
    {
        bot.OnWaitWeaponReady();
    }


}
