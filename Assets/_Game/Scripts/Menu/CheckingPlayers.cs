using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckingPlayers : MonoBehaviour
{
    [SerializeField] private int maxPlayersInRoom = 2;
    [SerializeField] private Text currentPlayers;
    [SerializeField] private GameObject enterButton;
    // Update is called once per frame
    void Update()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == maxPlayersInRoom)
        {
            //khoa room
            PhotonNetwork.CurrentRoom.IsOpen = false;
            enterButton.SetActive(true);
        }
        if (!enterButton.activeInHierarchy)
        {
            currentPlayers.text = $"{PhotonNetwork.CurrentRoom.PlayerCount} / {maxPlayersInRoom}";
        }
        else
        {
            currentPlayers.text = "";

        }

    }
    public void EnterArena()
    {
        gameObject.SetActive(false);
    }
}
