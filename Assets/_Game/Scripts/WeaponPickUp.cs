using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;
public class WeaponPickUp : MonoBehaviour
{
    [SerializeField] private AudioSource audioPlayer;
    [SerializeField] private PhotonView photonView;
    [SerializeField] private Renderer rendererObj;
    [SerializeField] private GameObject otherRenderChildGun;
    [SerializeField] private Collider colliderObj;
    [SerializeField] float respawnTime = 5f;
    [SerializeField] private WeaponType weaponType;
    [SerializeField] int ammoRefillAmt = 30;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tag.PLAYER) && photonView.IsMine)
        {
            photonView.RPC(nameof(PlayPickupAudio), RpcTarget.All);
            photonView.RPC(nameof(TurnOffObject), RpcTarget.All);
            other.GetComponent<WeaponChangerAdvanced>().AmmoAmounts[(int)weaponType] += ammoRefillAmt;
            other.GetComponent<WeaponChangerAdvanced>().UpdatePickUpWeapon();

        }
    }
    [PunRPC]
    public void TurnOffObject()
    {
        if (weaponType == WeaponType.GUN_1)
        {
            rendererObj.enabled = false;
        }
        else
        {
            otherRenderChildGun.SetActive(false);
        }
        colliderObj.enabled = false;

        StartCoroutine(WaitToRespawn());
    }
    [PunRPC]
    public void TurnOnObject()
    {
        if (weaponType == WeaponType.GUN_1)
        {
            rendererObj.enabled = true;

        }
        else
        {
            otherRenderChildGun.SetActive(true);
        }
        colliderObj.enabled = true;
    }
    public IEnumerator WaitToRespawn()
    {
        yield return new WaitForSeconds(respawnTime);
        photonView.RPC(nameof(TurnOnObject), RpcTarget.All);
    }

    //this is remote Procedure function https://doc.photonengine.com/pun/current/gameplay/rpcsandraiseevent
    [PunRPC]
    public void PlayPickupAudio()
    {
        audioPlayer.Play();
    }
}
