using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRotate : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private PhotonView photonView;
    // Update is called once per frame
    void FixedUpdate()
    {
        if (photonView.IsMine)
        transform.Rotate(0, speed * Time.fixedDeltaTime, 0);
    }
}
