﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorController : MonoBehaviour {

    //Let models act exactly with input.
    //So we need input parameters.
    //Then put them into animator blend tree.

    public GameObject model;
    public IUserInput pi;
    public CameraController camcon;
    public float walkspeed = 2.4f;
    public float runMultiplier = 2.0f;
    public float runVelovity;  // smoothdamp
    public float jumpVelocity = 4.0f;
    public float rollVelocity = 3.0f;
    public float jabMultiplier = 3.0f;
   

    [Space(10)]
    [Header("===== Friction Setting =====")]
    public PhysicMaterial frictionOne;
    public PhysicMaterial frictionZero;

    private Animator anim;
    private Rigidbody rigid;
    private Vector3 planarVec;
    private Vector3 thrustVec;
    private bool lockPlanar = false;
    private bool trackDirection = false;
    private bool canAttack = false;
    private CapsuleCollider col;
    private float lerpTarget;
    private Vector3 deltaPos;



    // Use this for initialization
    void Awake () {
        IUserInput[] inputs = GetComponents<IUserInput>();
        foreach (var input in inputs)
        {
            if (input.enabled == true)
            {
                pi = input;
                break;
            }
        }
        anim = model.GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();  //FixedUpdata
        col = GetComponent<CapsuleCollider>();
    }
	
	// Update is called once per frame
	void Update () {

		//defense
        anim.SetBool("defense", pi.defense);

		//roll trigger
        if (pi.roll || rigid.velocity.magnitude>7.0f)
        {
            anim.SetTrigger("roll");
            canAttack = false;
        }

		//jump trigger
        if (pi.jump == true && CheckState("Ground") && !CheckState("Attack1hA","Attack") && !CheckState("Attack1hB", "Attack") && !CheckState("Attack1hC", "Attack"))
        {
            anim.SetTrigger("jump");
            canAttack = false;
        }

		//attack trigger
        if(pi.attack == true && CheckState("Ground") == true && canAttack == true)
        {
            anim.SetTrigger("attack");
        }

		//lockMode & move
		if (pi.lockon == true)
		{
			camcon.LockUnlock();
		}
		if (camcon.lockStat == false)  //lockMode off
		{
			anim.SetFloat("forward", pi.dMag * (pi.run ? 2.0f : 1.0f));
			anim.SetFloat("right", 0);
		}
		else
		{
			Vector3 localDvec = transform.InverseTransformVector(pi.dVec);
			anim.SetFloat("forward", localDvec.z * (pi.run ? 2.0f : 1.0f));
			anim.SetFloat("right", localDvec.x * (pi.run ? 2.0f : 1.0f));
		}
		if (camcon.lockStat == false)  // Cant change forward in lockMode 
        {
            if (pi.dMag > 0.001f)  // avoid pi.dVec = 0
            {
                model.transform.forward = Vector3.Slerp(model.transform.forward, pi.dVec, 0.3f);
            }

            if (lockPlanar == false)
            {
                planarVec = pi.dMag * model.transform.forward * walkspeed * (pi.run ? runMultiplier : 1.0f);
            }

        }
        else
        {
            if (trackDirection == false)
            {
                model.transform.forward = transform.forward;
            }
            else
            {
                model.transform.forward = planarVec.normalized;
            }


            if (lockPlanar == false)
            {
                planarVec = pi.dVec  * walkspeed * (pi.run ? runMultiplier : 1.0f);
            }
        }

    }

    private void FixedUpdate()
    {
		//control rigibody
        rigid.position += deltaPos;
        rigid.velocity = new Vector3(planarVec.x, rigid.velocity.y, planarVec.z) + thrustVec;
        thrustVec = Vector3.zero;
        deltaPos = Vector3.zero;
    }

    private bool CheckState(string stateName,string layerName = "Base Layer")
    {
        int layerIndex = anim.GetLayerIndex(layerName);
        bool result = anim.GetCurrentAnimatorStateInfo(layerIndex).IsName(stateName);
        //访问 animator 数据类型的实例 anim下的GCASI方法返回的实例的方法IsName，并把返回值赋给result
        return result;
    }


            ///
            ///  Message processing block
            ///

    public void OnJumpEnter()
    {
        pi.inputEnabled = false;
        lockPlanar = true;
        thrustVec = new Vector3(0, jumpVelocity, 0);
        trackDirection = true;
    }

    public void IsGround()
    {
        anim.SetBool("isGround", true);
    }
    
    public void IsNotGround()
    {
        anim.SetBool("isGround", false);
    }

    public void OnGroundEnter()
    {
        pi.inputEnabled = true;
        lockPlanar = false;
        canAttack = true;
        col.material = frictionOne;
        trackDirection = false;
    }
    public void OnGroundExit()
    {
        col.material = frictionZero;
    }

    public void OnFallEnter()
    {
        pi.inputEnabled = false;
        lockPlanar = true;
    }
    
    public void OnRollEnter()
    {
        pi.inputEnabled = false;
        lockPlanar = true;
        thrustVec = new Vector3(0, rollVelocity, 0);
        trackDirection = true;
    }

    public void OnJabEnter()
    {
        pi.inputEnabled = false;
        lockPlanar = true;
    }

    public void OnJabUpdate()
    {
        thrustVec = model.transform.forward * anim.GetFloat("jabVelocity") * jabMultiplier ;
    }

    public void OnAttack1hAEnter()
    {
        pi.inputEnabled = false;
        lerpTarget = 1.0f;
        
    }
    public void OnAttack1hAUpdate()
    {
        thrustVec = model.transform.forward * anim.GetFloat("attack1hAVelocity");
        float currentWeight = anim.GetLayerWeight(anim.GetLayerIndex("Attack"));
        currentWeight = Mathf.Lerp(currentWeight, lerpTarget, 0.1f);
        anim.SetLayerWeight(anim.GetLayerIndex("Attack"), currentWeight);
    }
    public void OnAttackIdleEnter()
    {
        pi.inputEnabled = true;
        lerpTarget = 0f;
    }
    public void OnAttackIdleUpdate()
    {
        float currentWeight = anim.GetLayerWeight(anim.GetLayerIndex("Attack"));
        currentWeight = Mathf.Lerp(currentWeight, lerpTarget, 0.1f);
        anim.SetLayerWeight(anim.GetLayerIndex("Attack"), currentWeight);
    }

    public void OnUpdateRM(object _deltaPos)
    {
        if (CheckState("Attack1hC","Attack"))
        {
            deltaPos += (0.7f * deltaPos + 0.3f * (Vector3)_deltaPos) / 1.0f;
        }
    }
}

