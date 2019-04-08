using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftArmAnimFix : MonoBehaviour {

	private ActorController ac;
    private Animator anim;
    public Vector3 TempVec;

     void Awake()
    {
        anim = GetComponent<Animator>();
		ac = GetComponentInParent<ActorController>();
    }

    void OnAnimatorIK()
    {
		if (ac.leftIsShiel)
		{
			if (anim.GetBool("defense") == false)
			{
				Transform LeftLowerArm = anim.GetBoneTransform(HumanBodyBones.LeftLowerArm);
				LeftLowerArm.localEulerAngles += TempVec;
				anim.SetBoneLocalRotation(HumanBodyBones.LeftLowerArm, Quaternion.Euler(LeftLowerArm.localEulerAngles));

			}
		}

    }
}
