using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorButton : MonoBehaviour
{
    private GameObject[] players;
    private int myId;
    private PhotonView photonView;
    public void SelectButton(int buttonId)
    {
        players = GameObject.FindGameObjectsWithTag(Tag.PLAYER);
        for (int i = 0; i < players.Length; i++)
        {
            photonView = players[i].GetComponent<PhotonView>();
            if (photonView.IsMine)
            {
                myId = photonView.ViewID;
                break;
            }
        }
        if (photonView != null)
        {
            photonView.RPC(nameof(SelectedColor), RpcTarget.AllBuffered, myId);
        }
    }
    [PunRPC]

    private void SelectedColor()
    {
        players = GameObject.FindGameObjectsWithTag(Tag.PLAYER);
        for (int i = 0; i < players.Length; i++)
        {

        }
        this.transform.gameObject.SetActive(false);
    }


}
