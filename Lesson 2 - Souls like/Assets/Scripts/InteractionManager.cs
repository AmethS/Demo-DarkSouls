using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : IActorManagerInterface {

	public CapsuleCollider interCol;


	// Use this for initialization
	void Start ()
	{
		interCol = GetComponent<CapsuleCollider>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerStay(Collider col)
	{
		EventCasterManager[] ecastms = col.GetComponents<EventCasterManager>();
		foreach (var ecastm in ecastms)
		{
			print(ecastm.eventName);
		}

	}
}
