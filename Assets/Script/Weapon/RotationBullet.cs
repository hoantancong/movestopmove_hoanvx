using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationBullet : Bullet {
    private Transform child;
    public override void OnInit(Character character,Vector3 target)
    {
        base.OnInit(character,target);
        TF.forward = character.TF.forward;
        if (child==null)
        {
            child = transform.Find("child").transform;
        }
        //do something
    }
    private void Update()
    {
        if (isRunning)
        {
            TF.Translate(TF.forward * speed * Time.deltaTime, Space.World);
            transform.GetChild(0).Rotate(Vector3.up * -6, Space.Self);
        }

    }

}
