using Cinemachine;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class WeaponChangerAdvanced : MonoBehaviour
{
    [SerializeField] private TwoBoneIKConstraint leftHand;
    [SerializeField] private TwoBoneIKConstraint rightHand;
    [SerializeField] private RigBuilder rig;
    [SerializeField] private Transform[] leftTargets;
    [SerializeField] private Transform[] rightTargers;
    [SerializeField] private GameObject[] weapons;
    [SerializeField] private MultiAimConstraint[] aimObjects;
    [SerializeField] private PhotonView photon;

    private Transform aimTarget;

    private CinemachineVirtualCamera cam;
    private GameObject camObject;

    private void Start()
    {
        camObject = GameObject.Find("PlayerCam");
        aimTarget = GameObject.Find("AimRef").transform;
        if (photon.IsMine)
        {
            cam = camObject.GetComponent<CinemachineVirtualCamera>();
            cam.Follow = gameObject.transform;
            cam.LookAt = gameObject.transform;
            Invoke(nameof(SetLookAt), 0.1f);
        }
    }
    void SetLookAt()
    {
        if(aimTarget != null)
        {
            for(int i = 0; i < aimObjects.Length; i++)
            {
                var target = aimObjects[i].data.sourceObjects;
                target.SetTransform(0, aimTarget.transform);
                aimObjects[i].data.sourceObjects = target;
            }
            rig.Build();
        }
    }
    private int weaponNumber = 0;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            weaponNumber++;
            if (weaponNumber > weapons.Length - 1)
            {
                weaponNumber = 0;
            }
            for (int i = 0; i < weapons.Length; i++)
            {
                weapons[i].SetActive(false);
            }
            weapons[weaponNumber].SetActive(true);
            leftHand.data.target = leftTargets[weaponNumber];
            rightHand.data.target = rightTargers[weaponNumber];
            rig.Build();
        }
    }
}
