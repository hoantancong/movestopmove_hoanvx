using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    // Start is called before the first frame update

    private Joystick joystick;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(isAlive)
        MoveCharacter();

    }
    private void MoveCharacter()
    {
        //character controller
        if (joystick == null)
        {
            joystick = FindAnyObjectByType<Joystick>();

        }
        else
        {
            //move

            if (joystick.GetInput() != Vector2.zero)
            {
                Vector3 direction = new Vector3(joystick.GetInput().x, 0, joystick.GetInput().y).normalized;
                Move(direction);
            }
            else
            {

                Idle();
                Transform target = FireTarget();
                if (target != null)
                {
                    Fire(target.position);
                }


            }

        }

    }
}
