using Cinemachine;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private MultiAimConstraint[] aimObjects;
    [SerializeField] private PhotonView photon;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private RigBuilder rig;

    private GameObject camObject;
    private Transform aimTarget;
    private CinemachineVirtualCamera cam;

    private void Start()
    {
        camObject = GameObject.Find("PlayerCam");
        //aimTarget = GameObject.Find("AimRef").transform;
        if (photon.IsMine)
        {
            cam = camObject.GetComponent<CinemachineVirtualCamera>();
            cam.Follow = gameObject.transform;
            cam.LookAt = gameObject.transform;
            //Invoke(nameof(SetLookAt), 0.1f);
        }
        else
        {
            playerMovement.enabled = false;
        }
    }
    //void SetLookAt()
    //{
    //    if (aimTarget != null)
    //    {
    //        for (int i = 0; i < aimObjects.Length; i++)
    //        {
    //            var target = aimObjects[i].data.sourceObjects;
    //            target.SetTransform(0, aimTarget.transform);
    //            aimObjects[i].data.sourceObjects = target;
    //        }
    //        rig.Build();
    //    }
    //}

    // Update is called once per frame
    void Update()
    {
        
    }
}
