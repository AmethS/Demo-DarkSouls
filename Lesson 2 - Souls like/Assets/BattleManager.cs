using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CapsuleCollider))]
public class BattleManager : MonoBehaviour {



	void OnTriggerEnter(Collider col)
	{
		print(col.name);
	}









}
