using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NickName : MonoBehaviourPunCallbacks
{
    [SerializeField] private Text[] names;
    [SerializeField] private Image[] healthBars;
    [SerializeField] private GameObject waitingPanel;

    public Text[] Names { get => names; set => names = value; }
    public Image[] HealthBars { get => healthBars; set => healthBars = value; }

    private void Start()
    {
        for (int i = 0; i < names.Length; i++)
        {
            names[i].gameObject.SetActive(false);
            HealthBars[i].gameObject.SetActive(false);
        }
    }
    public void Leaving()
    {
        StartCoroutine(BacktoLobby());
    }

    IEnumerator BacktoLobby()
    {
        yield return new WaitForSeconds(1);
        PhotonNetwork.LoadLevel("Lobby");
    }
    public void ReturnToLobby()
    {
        waitingPanel.SetActive(false);
        RoomExit();
    }

    private void RoomExit()
    {
        StartCoroutine(ToLobby());
    }
    IEnumerator ToLobby()
    {
        yield return new WaitForSeconds(0.1f);
        Cursor.visible = true;
        PhotonNetwork.LeaveRoom();
    }
    public override void OnLeftRoom()
    {
        PhotonNetwork.LoadLevel("Lobby");

    }
}
