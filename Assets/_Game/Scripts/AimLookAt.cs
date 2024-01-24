using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimLookAt : MonoBehaviour
{
    private GameObject lookAtObject;
    [SerializeField] private PhotonView photonView;
    // Start is called before the first frame update
    void Start()
    {
        lookAtObject = GameObject.Find("AimRef");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (photonView.IsMine)
        {
            transform.position = lookAtObject.transform.position;
        }
    }
}
