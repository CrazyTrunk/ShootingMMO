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
    [SerializeField] private GameObject messagePanel;
    [SerializeField] private Text messageText;
    [SerializeField] private PhotonView photonViewObj;
    [SerializeField] private int[] kills;

    public Text[] Names { get => names; set => names = value; }
    public Image[] HealthBars { get => healthBars; set => healthBars = value; }
    public int[] Kills { get => kills; set => kills = value; }

    private void Start()
    {
        messagePanel.SetActive(false);
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
    public void RunMessage(string win , string lose)
    {
        photonViewObj.RPC(nameof(DisplayMessage), RpcTarget.All, win, lose);
        UpdateKills(win);
    }

    private void UpdateKills(string win)
    {
        for (int i =0; i< names.Length; i++)
        {
            if(win == names[i].text)
            {
                Kills[i]++;
            }
        }
    }

    [PunRPC]
    private void DisplayMessage(string win, string lose)
    {
        messagePanel.SetActive(true);
        messageText.text = $"{win} killed {lose}";
        StartCoroutine(SwitchOffMessage());
    }

    IEnumerator SwitchOffMessage()
    {
        yield return new WaitForSeconds(3f);
        photonViewObj.RPC(nameof(MessageOff), RpcTarget.All);
    }
    [PunRPC]
    private void MessageOff()
    {
        messagePanel.SetActive(false);
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
