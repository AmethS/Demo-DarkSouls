using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorManager : MonoBehaviour {

	public ActorController ac;

	[Header("=== Auto Generate if Null===")]
	public BattleManager bm;
	public WeaponManager wm;
	public StateManager sm;

	// Use this for initialization
	void Awake()
	{
		ac = GetComponent<ActorController>();
		GameObject model = ac.model;
		GameObject sensor = transform.Find("sensor").gameObject;


		bm = Bind<BattleManager>(sensor);
		wm = Bind<WeaponManager>(model);
		sm = Bind<StateManager>(gameObject);
		sm.Test();
	}

	//Generic  for connect AM and Other Mananger
	private T Bind<T>(GameObject go) where T : IActorManagerInterface 
	{
		T tempInstance;
		tempInstance = go.GetComponent<T>();
		if (tempInstance == null)
		{
			tempInstance = go.AddComponent<T>();
		}
		tempInstance.am = this;
		return tempInstance;
	}

	// Update is called once per frame
	void Update () {
		 
	}

	public void TryDoDamage()
	{
		if (sm.isImmortal)
		{
			//do nothing
		}
		else if (sm.isDefense)
		{
			Blocked();
		}
		else
		{
			if (sm.HP <= 0)
			{
				//Already dead.
			}
			else
			{
				sm.AddHP(-5);
				if (sm.HP > 0)
				{
					Hit();
				}
				else
				{
					Die();
				}
			}
			
		}
	}

	public void Blocked()
	{
		ac.IssueTrigger("blocked");
	}


	public void Hit()
	{
		ac.IssueTrigger("hit");
	}
	public void Die()
	{
		ac.IssueTrigger("die");
		ac.pi.inputEnabled = false;
		if (ac.camcon.lockStat == true)
		{
			ac.camcon.LockUnlock();
		}
			ac.camcon.enabled = false;
	}
}
