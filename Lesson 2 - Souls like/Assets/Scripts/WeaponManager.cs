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
		whL = transform.DeepFind("weaponHandleL").gameObject;
		whR = transform.DeepFind("weaponHandleR").gameObject;

		wcL = BindWeaponController(whL);
		wcR = BindWeaponController(whR);


		weaponColL = whL.GetComponentInChildren<Collider>();
		weaponColR = whR.GetComponentInChildren<Collider>();

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
}
