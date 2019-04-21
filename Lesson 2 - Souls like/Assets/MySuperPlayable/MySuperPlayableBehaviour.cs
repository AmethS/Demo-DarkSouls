using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class MySuperPlayableBehaviour : PlayableBehaviour
{
    public GameObject MyCamera1;
    public float MyFloat;

	PlayableDirector pd;

	public override void OnGraphStart(Playable playable)
	{
		pd = (PlayableDirector)playable.GetGraph().GetResolver();
		foreach (var track in pd.playableAsset.outputs)
		{
			if (track.streamName == "Attacker Script" || track.streamName == "Victim Script")
			{
				ActorManager am = (ActorManager)pd.GetGenericBinding(track.sourceObject);
				am.LockUnLockActorController(true);
			}
		}
	}

	public override void OnGraphStop(Playable playable)
	{
		foreach (var track in pd.playableAsset.outputs)
		{
			if (track.streamName == "Victim Script")
			{
				ActorManager am = (ActorManager)pd.GetGenericBinding(track.sourceObject);
				am.LockUnLockActorController(false);
			}
		}
	}

	public override void OnBehaviourPause(Playable playable, FrameData info)
	{
		foreach (var track in pd.playableAsset.outputs)
		{
			if (track.streamName == "Attacker Script")
			{
				ActorManager am = (ActorManager)pd.GetGenericBinding(track.sourceObject);
				am.LockUnLockActorController(false);
			}
		}
	}






}
