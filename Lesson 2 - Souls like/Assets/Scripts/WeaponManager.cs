using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour {

	public ActorManager am;
	private Collider weaponCol;

	public GameObject whL;
	public GameObject whR;

	void Start()
	{
		weaponCol = whR.GetComponentInChildren<Collider>();
		print(transform.DeepFind("weaponHandleR"));
	}

	public void WeaponEnable()
	{
		weaponCol.enabled = true;
	}
	public void WeaponDisable()
	{
		weaponCol.enabled = false;
	}
}
