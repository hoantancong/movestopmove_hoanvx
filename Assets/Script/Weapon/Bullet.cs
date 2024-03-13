using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : GameUnit
{
    [SerializeField] protected float speed = 6f;
    private Character character;
    private readonly float LIFE_TIME = 3f;
    protected bool isRunning;
    public virtual void OnInit(Character _character, Vector3 target)
    {
        TF.forward = (target - TF.position).normalized;
        character = _character;
        isRunning = true;
        StartCoroutine(SelfDestruction(LIFE_TIME));
    }

    private IEnumerator SelfDestruction(float lifeTime)
    {
        yield return new WaitForSeconds(lifeTime);
        OnDespawn();
    }

    public virtual void OnDespawn()
    {
        ObjectPool.Instance.DespawnToPool(Constants.TAG_WEAPON);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!isRunning) return;
        if (other.CompareTag(Constants.TAG_CHARACTER))
        {
            if (other.GetComponent<Character>() != character && other.GetComponent<Character>().isAlive)
            {
                //not hit itself
                other.GetComponent<Character>().GetHit();
                OnStop();
            } 

        }
        if (other.CompareTag(Constants.TAG_OBSTACLE))
        {
            //OnStop
            OnStop();
        }


    }
    protected void OnStop() {
        isRunning = false;
    }
}
