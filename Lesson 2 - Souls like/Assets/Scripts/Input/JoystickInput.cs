using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickInput : IUserInput {

    [Header("===== Joystick Settings =====")]

    //RealButton
    public string axisX = "AxisX";
    public string axisY = "AxisY";
    public string axisJright = "Axis4";
    public string axisJup = "Axis5";
    public string btnA = "Btn0";
    public string btnB = "Btn1";
    public string btnX = "Btn2";
    public string btnY = "Btn3";
    public string btnLB = "Btn4";
    public string btnRB = "Btn5";
    public string btnLS = "Btn8";
    public string btnRS = "Btn9";

    //VirtualButton
    public MyButton buttonA = new MyButton();
    public MyButton buttonB = new MyButton();
    public MyButton buttonX = new MyButton();
    public MyButton buttonY = new MyButton();
    public MyButton buttonLB = new MyButton();
    public MyButton buttonLT = new MyButton();
    public MyButton buttonRB = new MyButton();
    public MyButton buttonRT = new MyButton();
    public MyButton buttonLS = new MyButton();
    public MyButton buttonRS = new MyButton();



    // Update is called once per frame
    void Update() {
    

        buttonA.Tick(Input.GetButton(btnA));
        buttonB.Tick(Input.GetButton(btnB));
        buttonX.Tick(Input.GetButton(btnX));
        buttonY.Tick(Input.GetButton(btnY));
        buttonLB.Tick(Input.GetButton(btnLB));
        buttonRB.Tick(Input.GetButton(btnRB));
        buttonLS.Tick(Input.GetButton(btnLS));
        buttonRS.Tick(Input.GetButton(btnRS));

        jUp = -1 * Input.GetAxis(axisJup);
        jRight = Input.GetAxis(axisJright);

        targetDup = Input.GetAxis(axisY);
        targetDright = Input.GetAxis(axisX);

        // input on / off  (soft switch)
        if (inputEnabled == false)
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


        //action
        run = (buttonB.IsPressing && !buttonB.IsDelaying) || buttonB.IsExtending;
        jump = buttonB.IsExtending && buttonB.OnPressed;
        roll = (buttonB.OnReleased && buttonB.IsDelaying);// ||(buttonA.IsPressing && !buttonA.IsDelaying);
        defense = buttonLB.IsPressing;
        attack = buttonRB.OnPressed;
        lockon = buttonRS.OnPressed;
    }

}
