using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
public class GameManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private InputField nickNamePlayer;
    [SerializeField] private GameObject connecting;

    private string setName = "";
    // Start is called before the first frame update
    void Start()
    {
        connecting.SetActive(false);
    }

    public void UpdateText()
    {
        setName = nickNamePlayer.text;
        PhotonNetwork.LocalPlayer.NickName = setName;
    }
    public void EnterButton()
    {
        if (setName != null)
        {
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.ConnectUsingSettings();
            connecting.SetActive(true);
        }
    }
    public void ExitButton()
    {
        Application.Quit();
    }
    public override void OnConnectedToMaster()
    {
        Debug.Log("I'm connect to server");
        PhotonNetwork.JoinRandomRoom();
    }
    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Floor layout");
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        PhotonNetwork.CreateRoom("Arena1");
    }
}
