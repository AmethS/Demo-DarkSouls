using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : IActorManagerInterface {

	private Collider weaponColL;
	private Collider weaponColR;

	public GameObject whL;
	public GameObject whR;

	void Start()
	{
		whL = transform.DeepFind("weaponHandleL").gameObject;
		whR = transform.DeepFind("weaponHandleR").gameObject;

		weaponColL = whL.GetComponentInChildren<Collider>();
		weaponColR = whR.GetComponentInChildren<Collider>();



		//weaponCol = whR.GetComponentInChildren<Collider>();
		//print(transform.DeepFind("Cylinder"));
	}

	public void WeaponEnable()
	{
		if (am.ac.CheckStateTag("AttackL"))
		{
			weaponColL.enabled = true;
		}
		else
		{
			weaponColR.enabled = true;
		}
	}
	public void WeaponDisable()
	{
			weaponColL.enabled = false;
			weaponColR.enabled = false;
	}
}
