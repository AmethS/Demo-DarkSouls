using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInput : IUserInput
{

    //Variable

   

    [Header("===== Key setting =====")]
    public string KeyUp = "w";     // declara key
    public string KeyDown = "s";
    public string KeyLeft = "a";
    public string KeyRight = "d";

    public string keyA;
    public string keyB;
    public string keyC;
    public string keyD;

    public string KeyJup;
    public string KeyJdown;
    public string KeyJleft;
    public string KeyJright;
    [Header("===== Mouse setting =====")]
    public bool mouseEnable = false;
    public float mouseSensitivity = 1.0f;

    // Update is called once per frame
    void Update()
    {
        if (mouseEnable == true)
        {
            jUp = mouseSensitivity * Input.GetAxis("Mouse Y");
            jRight = mouseSensitivity * Input.GetAxis("Mouse X");
        }
        else
        {
        jUp = (Input.GetKey(KeyJup) ? 1.0f : 0) - (Input.GetKey(KeyJdown) ? 1.0f : 0);
        jRight = (Input.GetKey(KeyJright) ? 1.0f : 0) - (Input.GetKey(KeyJleft) ? 1.0f : 0);
        }


        targetDup = (Input.GetKey(KeyUp) ? 1.0f : 0) - (Input.GetKey(KeyDown) ? 1.0f : 0);
        targetDright = (Input.GetKey(KeyRight) ? 1.0f : 0) - (Input.GetKey(KeyLeft) ? 1.0f : 0);

        // input on / off  (soft switch)
        if (inputEnabled == false)   // soft switch
        {
            targetDup = 0;
            targetDright = 0;
        }

        // set dUp & dRight smoothly
        dUp = Mathf.SmoothDamp(dUp, targetDup, ref velocityDup, 0.1f);
        dRight = Mathf.SmoothDamp(dRight, targetDright, ref velocityDright, 0.1f);



        // Circle grid mapping
        Vector2 tempDaxis = SquareToCircle(new Vector2(dRight, dUp));
        float dRight2 = tempDaxis.x;
        float dUp2 = tempDaxis.y;

        //velocity & direction
        dMag = Mathf.Sqrt((dUp2 * dUp2) + (dRight2 * dRight2));
        dVec = (dRight2 * transform.right) + (dUp2 * transform.forward);   

        // run
        run = Input.GetKey(keyA);

        //defense
        defense = Input.GetKey(keyD);

        //jump
        bool newJump = Input.GetKey(keyB);
        if (newJump != lastJump && newJump == true)
        {
            jump = true;
           // print("jump triger!");
        }
        else
        {
            jump = false;
        }
        lastJump = newJump;

        //attack
        bool newAttack = Input.GetKey(keyC);
        if (newAttack != lastAttack && newAttack == true)
        {
            attack = true; 
        }
        else
        {
            attack = false;
        }
        lastAttack = newAttack;
    }

    
    //private Vector2 SquareToCircle(Vector2 input)
    //{
    //    Vector2 output = Vector2.zero;

    //    output.x = input.x * Mathf.Sqrt(1 - (input.y * input.y) / 2.0f);
    //    output.y = input.y * Mathf.Sqrt(1 - (input.x * input.x) / 2.0f);

    //    return output;
    //}
}
