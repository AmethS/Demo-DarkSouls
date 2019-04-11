using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{

    public float horizontalSpeed = 20.0f;
    public float verticalSpeed = 12.0f;
    public Image lockDot;
    public bool lockStat;
    public IUserInput pi;
	public bool isAI = false;

    private GameObject playerHandle;
    private GameObject cameraHandle;
    private float tempEulerX;
    private Vector3 cameraDampVelocity;
    private GameObject model;
    private GameObject myCamera;
    [SerializeField]
    private LockTarget lockTarget;

    // Use this for initialization
    void Start() {
        cameraHandle = transform.parent.gameObject;
        playerHandle = cameraHandle.transform.parent.gameObject;
        ActorController ac = playerHandle.GetComponent<ActorController>();
        model = ac.model;
        pi = ac.pi;

		if (!isAI)
		{
		myCamera = Camera.main.gameObject;
        lockDot.enabled = false;
		}
        //Cursor.lockState = CursorLockMode.Locked;


        tempEulerX = -3;
        lockStat = false;
    }

    public void Update()
    {
        if (lockTarget != null)
        {
			if (!isAI)
			{
            lockDot.rectTransform.position = Camera.main.WorldToScreenPoint(lockTarget.obj.transform.position);
			}
            if (Vector3.Distance(model.transform.position, lockTarget.obj.transform.position) > 10.0f) //release lockMode when distance over 10
            {
				LockProcessA(null, false, false, isAI);
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate() {

        if (lockTarget == null)
        {
            Vector3 tempModelEuler = model.transform.eulerAngles;

            playerHandle.transform.Rotate(Vector3.up, pi.jRight * 10.0f * Time.fixedDeltaTime * horizontalSpeed);
            //cameraHandle.transform.Rotate(new Vector3 (0,1,0), pi.jRight * 10.0f * Time.deltaTime * horizontalSpeed);
            tempEulerX -= pi.jUp * verticalSpeed * Time.deltaTime;
            tempEulerX = Mathf.Clamp(tempEulerX, -10, 25);
            cameraHandle.transform.localEulerAngles = new Vector3(tempEulerX, 0, 0);

            model.transform.eulerAngles = tempModelEuler;
            lockStat = false;
        }
        else
        {
            Vector3 tempForward = lockTarget.obj.transform.position - model.transform.position;
            tempForward.y = 0;
            playerHandle.transform.forward = tempForward;
            cameraHandle.transform.LookAt(lockTarget.obj.transform.position - new Vector3(0, lockTarget.halfHeight, 0));
        }

		if (!isAI)  //AI dont have lockOnMode
		{
        myCamera.transform.position = Vector3.SmoothDamp(myCamera.transform.position, transform.position, ref cameraDampVelocity, 0.08f);
        myCamera.transform.LookAt(cameraHandle.transform);
        //myCamera.transform.eulerAngles = transform.eulerAngles;
		}
    }
	private void LockProcessA(LockTarget _lockTarget, bool _lockDotEnable, bool _lockState,bool _isAI)
	{
		lockTarget = _lockTarget;
		lockStat = _lockState;
		if (!_isAI)
		{
		lockDot.enabled = _lockDotEnable;
		}
	}


	public void LockUnlock()
    {


        Vector3 modelOrigin1 = model.transform.position;
        Vector3 modelOrigin2 = modelOrigin1 + new Vector3(0, 1, 0);
        Vector3 boxCenter = modelOrigin2 + model.transform.forward * 5.0f;
        Collider[] cols = Physics.OverlapBox(boxCenter, new Vector3(3.5f, 0.5f, 5f), model.transform.rotation, LayerMask.GetMask(isAI? "Player":"Enemy"));


        if (cols.Length == 0)
        {
			LockProcessA(null, false, false, isAI);
        }
        else
        {
            foreach (var col in cols)
            {
                if (lockTarget !=null && lockTarget.obj == col.gameObject)
                {
					LockProcessA(null, false, false, isAI);
                    break;
                }
				LockProcessA(new LockTarget(col.gameObject, col.bounds.extents.y), true, true, isAI);
                break;
            }
        }

    }

    private class LockTarget
    {
        public GameObject obj;
        public float halfHeight;

        public LockTarget(GameObject _obj,float _halfHeight)
        {
            obj = _obj;
            halfHeight = _halfHeight;
        }
    }



}
