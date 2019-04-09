﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IUserInput : MonoBehaviour {

    [Header("===== Output singals =====")]
    public float dUp;        // control forward and back
    public float dRight;     // control left and right
    public float dMag;
    public Vector3 dVec;
    public float jUp;
    public float jRight;


      //1. pressing signal
    public bool run;
    public bool defense;
      //2. trigger once singal
    public bool jump;
    protected bool lastJump;
    //public bool attack;
    protected bool lastAttack;
    public bool roll;
    public bool lockon;
	public bool lb;
	public bool lt;
	public bool rb;
	public bool rt;
      //3. double trigger


    [Header("===== Others =====")]
    public bool inputEnabled = true;    //input on / off

    protected float targetDup;    //SmoothDamp parameter
    protected float targetDright;
    protected float velocityDup;
    protected float velocityDright;


    protected Vector2 SquareToCircle(Vector2 input)
    {
        Vector2 output = Vector2.zero;

        output.x = input.x * Mathf.Sqrt(1 - (input.y * input.y) / 2.0f);
        output.y = input.y * Mathf.Sqrt(1 - (input.x * input.x) / 2.0f);

        return output;
    }

	protected void UpdateDmagDvec(float dUp,float dRight)
	{
		dMag = Mathf.Sqrt((dUp * dUp) + (dRight * dRight));
		dVec = (dRight * transform.right) + (dUp * transform.forward);

	}

}
