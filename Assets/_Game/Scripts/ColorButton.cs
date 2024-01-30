using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorButton : MonoBehaviour
{
    [SerializeField] private GameObject colorPanel;
    [SerializeField] private PhotonView buttonPhotonView;

    private GameObject[] players;
    private int myId;
    private PhotonView photonView;
    private void Start()
    {
        Cursor.visible = true;
    }
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
        buttonPhotonView.RPC(nameof(SelectedColor), RpcTarget.AllBuffered, buttonId, myId);
        Cursor.visible = false;
        colorPanel.SetActive(false);

    }

    [PunRPC]
    public void SelectedColor(int buttonId, int myId)
    {
        players = GameObject.FindGameObjectsWithTag(Tag.PLAYER);
        for (int i = 0; i < players.Length; i++)
        {
            players[i].GetComponent<DisplayColor>().ViewIds[buttonId] = myId;
            players[i].GetComponent<DisplayColor>().ChooseColor();
        }
        this.transform.gameObject.SetActive(false);
    }


}
