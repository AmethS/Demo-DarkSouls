using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftArmAnimFix : MonoBehaviour {

    private Animator anim;
    public Vector3 TempVec;

     void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void OnAnimatorIK()
    {
        if (anim.GetBool("defense")==false)
        {
        Transform LeftLowerArm = anim.GetBoneTransform(HumanBodyBones.LeftLowerArm);
        LeftLowerArm.localEulerAngles += TempVec;
        anim.SetBoneLocalRotation(HumanBodyBones.LeftLowerArm, Quaternion.Euler(LeftLowerArm.localEulerAngles));

        }

    }
}
