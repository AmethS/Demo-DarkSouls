using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;


[RequireComponent(typeof(PlayableDirector))]
public class DirectorManager : IActorManagerInterface {

	public PlayableDirector pd;

	[Header("==== Timeline Assets ====")]
	public TimelineAsset frontStab;
	public TimelineAsset openBox;

	[Header("==== Assets Settings====")]
	public ActorManager attacker;
	public ActorManager victim;

	// Use this for initialization
	void Start () {
		pd = GetComponent<PlayableDirector>();
		pd.playOnAwake = false;
		//pd.playableAsset = frontStab;
		
	}
	
	// Update is called once per frame
	void Update ()
	{

	}

	public void PlayTimeline(string timelineName, ActorManager attacker, ActorManager victim)
	{
		//if (pd.playableAsset != null)
		//{
		//	return; //breck method. Will not execute the rest code.
		//}

		if (pd.state == PlayState.Playing)
		{
			return;
		}

		if (timelineName == "frontStab")
		{
			pd.playableAsset = Instantiate(frontStab);
			TimelineAsset timeline = (TimelineAsset)pd.playableAsset;
			foreach (var track in timeline.GetOutputTracks())
			{
				if (track.name == "Attacker Script")
				{
					pd.SetGenericBinding(track,attacker);
					foreach (var clip in track.GetClips())
					{
						MySuperPlayableClip myClip = (MySuperPlayableClip)clip.asset;
						MySuperPlayableBehaviour myBehav = myClip.template;
						myBehav.myFloat = 777f;
						Debug.Log(myBehav.myFloat);
						pd.SetReferenceValue(myClip.am.exposedName, attacker);
						
					}
				}
				else if (track.name == "Victim Script")
				{
					pd.SetGenericBinding(track, victim);
					foreach (var clip in track.GetClips())
					{
						MySuperPlayableClip myClip = (MySuperPlayableClip)clip.asset;
						MySuperPlayableBehaviour myBehav = myClip.template;
						myBehav.myFloat = 666f;
						Debug.Log(myBehav.myFloat);
						pd.SetReferenceValue(myClip.am.exposedName, victim);
						
					}
				}
				else if (track.name == "Attacker Animation")
				{
					pd.SetGenericBinding(track, attacker.ac.anim);
				}
				else if (track.name == "Victim Animation")
				{
					pd.SetGenericBinding(track, victim.ac.anim);
				}
			}
			pd.Evaluate();
			pd.Play();
		}

		else if (timelineName == "openBox")
		{
			pd.playableAsset = Instantiate(openBox);
			TimelineAsset timeline = (TimelineAsset)pd.playableAsset;
			foreach (var track in timeline.GetOutputTracks())
			{
				if (track.name == "Player Script")
				{
					pd.SetGenericBinding(track, attacker);
					foreach (var clip in track.GetClips())
					{
						MySuperPlayableClip myClip = (MySuperPlayableClip)clip.asset;
						MySuperPlayableBehaviour myBehav = myClip.template;
						pd.SetReferenceValue(myClip.am.exposedName, attacker);
					}
				}
				else if (track.name == "Box Script")
				{
					pd.SetGenericBinding(track, victim);
					foreach (var clip in track.GetClips())
					{
						MySuperPlayableClip myClip = (MySuperPlayableClip)clip.asset;
						MySuperPlayableBehaviour myBehav = myClip.template;
						pd.SetReferenceValue(myClip.am.exposedName, victim);
					}
				}
				else if (track.name == "Player Animation")
				{
					pd.SetGenericBinding(track, attacker.ac.anim);
				}
				else if (track.name == "Box Animation")
				{
					pd.SetGenericBinding(track, victim.ac.anim);
				}
			}
			pd.Evaluate();
			pd.Play();
		}
	}

}
