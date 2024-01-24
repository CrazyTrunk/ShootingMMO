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
    //private Transform aimTarget;
    private CinemachineVirtualCamera cam;
    private GameObject testObjectWeapons;
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
        testObjectWeapons = GameObject.Find("Weapon1pickup(Clone)");
        if(testObjectWeapons == null)
        {
            var spawners = GameObject.Find("SpawnScript");
            spawners.GetComponent<SpawnCharacters>().SpawnWeapons();
        }
    }
}
