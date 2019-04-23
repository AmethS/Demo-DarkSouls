using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorManager : MonoBehaviour {

	public ActorController ac;

	[Header("=== Auto Generate if Null===")]
	public BattleManager bm;
	public WeaponManager wm;
	public StateManager sm;
	public DirectorManager dm;
	public InteractionManager im;

	// Use this for initialization
	void Awake()
	{
		ac = GetComponent<ActorController>();
		GameObject model = ac.model;
		GameObject sensor = transform.Find("sensor").gameObject;


		bm = Bind<BattleManager>(sensor);
		wm = Bind<WeaponManager>(model);
		sm = Bind<StateManager>(gameObject);
		dm = Bind<DirectorManager>(gameObject);
		im = Bind<InteractionManager>(sensor);

		ac.OnAction += DoAction;


	}

	public void DoAction()
	{
		if (im.overlapEcastms.Count != 0)
		{
			if (im.overlapEcastms[0].active == true)
			{
				if (im.overlapEcastms[0].eventName == "frontStab")
				{
					dm.PlayTimeline("frontStab", this, im.overlapEcastms[0].am);
				}
				else if (im.overlapEcastms[0].eventName == "openBox")
				{
					if (BattleManager.CheckAnglePlayer(ac.model, im.overlapEcastms[0].am.gameObject, 40))
					{
						//im.overlapEcastms[0].active = false;
						transform.position = im.overlapEcastms[0].am.gameObject.transform.position + im.overlapEcastms[0].am.transform.TransformVector(im.overlapEcastms[0].offset);
						ac.model.transform.forward = im.overlapEcastms[0].am.transform.forward * -1;
						dm.PlayTimeline("openBox", this, im.overlapEcastms[0].am);
					}
				}
			}
		}
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

	public void SetIsCounterBack(bool value)
	{
		sm.isCounterBackEnable = value;
	}

	public void TryDoDamage(WeaponController targetWc,bool attackVaild,bool counterVaild)
	{
		if (sm.isCounterBackSuccess)
		{
			if (counterVaild)
			{
				targetWc.wm.am.Stunned();
			}
		}
		else if (sm.isCounterBackFailure)
		{
			if (attackVaild)
			{
				HitOrDie(false);
			}
		}
		else if (sm.isImmortal)
		{
			//do nothing
		}
		else if (sm.isDefense)
		{
			Blocked();
		}
		else
		{
			if (attackVaild)
			{
				HitOrDie(true);
			}
		}
	}

	public void Stunned()
	{
		ac.IssueTrigger("stunned");
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
	public void HitOrDie(bool doHitAnimation)
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
				if (doHitAnimation)
				{
					Hit();
				}
				//do some VFX, like splatter blood...
			}
			else 
			{
				Die();
			}
		}
		
	}



	public void LockUnLockActorController(bool value)
	{
		ac.SetBool("lock", value);
	}
}
