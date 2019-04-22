using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : IActorManagerInterface {


	public float HPMax = 15.0f;
	public float HP = 15.0f;

	[Header("1st order state flags")]
	public bool isGround;
	public bool isJump;
	public bool isFall;
	public bool isRoll;
	public bool isJab;
	public bool isAttack;
	public bool isHit;
	public bool isDie;
	public bool isBlocked;
	public bool isDefense;
	public bool isCounterBack ;  //related to state
	public bool isCounterBackEnable; // related to animation events

	[Header("2st order state flags")]
	public bool isAllowDefense;
	public bool isImmortal;
	public bool isCounterBackSuccess;
	public bool isCounterBackFailure;



	void Start()
	{
		HP = HPMax;
	}
	void Update()
	{
		isGround = am.ac.CheckState("Ground");
		isJump = am.ac.CheckState("Jump");
		isFall = am.ac.CheckState("Fall");
		isRoll = am.ac.CheckState("Roll");
		isJab = am.ac.CheckState("Jab");
		isAttack = am.ac.CheckStateTag("AttackL") || am.ac.CheckStateTag("AttackR");
		isHit = am.ac.CheckState("Hit");
		isDie = am.ac.CheckState("Die");
		isBlocked = am.ac.CheckState("Blocked");
		//isDefense = am.ac.CheckState("Defense1h", "Defense");
		isCounterBack = am.ac.CheckState("CounterBack");
		isCounterBackSuccess = isCounterBackEnable;
		isCounterBackFailure = isCounterBack && !isCounterBackEnable;

		isAllowDefense = isGround || isBlocked;
		isDefense = isAllowDefense && am.ac.CheckState("Defense1h", "Defense");
		isImmortal = isRoll || isJab;
	}
	public void AddHP(float value)
	{
		HP += value;
		HP = Mathf.Clamp(HP, 0, HPMax);

	}

	public void Test()
	{
		print("Hp is : "+ HP);
	}

}
