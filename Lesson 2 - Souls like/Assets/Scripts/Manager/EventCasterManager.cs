using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventCasterManager : IActorManagerInterface {

	public string eventName;
	public bool active;



	//// Use this for initialization
	void Start () {
		if (am == null)
		{
			am = GetComponentInParent<ActorManager>();
		}
		
	}
	
	//// Update is called once per frame
	//void Update () {
		
	//}
}
