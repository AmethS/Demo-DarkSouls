using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CapsuleCollider))]
public class BattleManager : IActorManagerInterface {

	private CapsuleCollider defCol;

	void Start()
	{
		defCol = GetComponent<CapsuleCollider>();
		defCol.center = new Vector3(0, 1, 0);
		defCol.height = 2.0f;
		defCol.radius = 0.4f;
		defCol.isTrigger = true;
	}
	void OnTriggerEnter(Collider col)
	{
		WeaponController targetWc = col.GetComponentInParent<WeaponController>();

		GameObject attacker = targetWc.wm.am.gameObject;
		GameObject receiver = am.gameObject;

		Vector3 attackingDir = receiver.transform.position - attacker.transform.position;
		Vector3 counterDir = attacker.transform.position - receiver.transform.position;

		float attackingAngle1 = Vector3.Angle(attacker.transform.forward, attackingDir); // attack range
		float counterAngle1 = Vector3.Angle(receiver.transform.forward, counterDir);// counter  range
		float counterAngle2 = Vector3.Angle(attacker.transform.forward, receiver.transform.forward);// closed to 180

		bool attackVaild = (attackingAngle1 < 45);
		bool counterVaild = (counterAngle1 < 30 && (Mathf.Abs(counterAngle2 - 180) < 30));


		if (col.tag=="Weapon")
		{
			am.TryDoDamage(targetWc,attackVaild,counterVaild);
			//print("Collider impact");
		}
	}



}
