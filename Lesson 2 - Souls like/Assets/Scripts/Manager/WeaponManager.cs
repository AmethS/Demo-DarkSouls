using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : IActorManagerInterface {

	private Collider weaponColL;
	private Collider weaponColR;

	public GameObject whL;
	public GameObject whR;

	public WeaponController wcL;
	public WeaponController wcR;

	void Start()
	{
		try
		{
			whL = transform.DeepFind("weaponHandleL").gameObject;
			wcL = BindWeaponController(whL);
			weaponColL = whL.GetComponentInChildren<Collider>();

		}
		catch (System.Exception)
		{
			//
			// If there is no "weaponHandleL" or related object.
			//
		}
		try
		{
			whR = transform.DeepFind("weaponHandleR").gameObject;
			wcR = BindWeaponController(whR);
			weaponColR = whR.GetComponentInChildren<Collider>();

		}
		catch (System.Exception)
		{
			//
			// If there is no "weaponHandleR" or related object.
			//
		}
	}

	public WeaponController BindWeaponController(GameObject targerObj)
	{
		WeaponController tempWc;
		tempWc = targerObj.GetComponent<WeaponController>();
		if (tempWc == null)
		{
			tempWc = targerObj.AddComponent<WeaponController>();
		}
		tempWc.wm = this;

		return tempWc;
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
	public void CounterBackEnable()
	{
		am.SetIsCounterBack(true);
	}
	public void CounterBackDisable()
	{
		am.SetIsCounterBack(false);
	}

}
