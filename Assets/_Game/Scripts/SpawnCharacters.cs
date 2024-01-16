using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCharacters : MonoBehaviour
{
    [SerializeField] private GameObject character;
    [SerializeField] private Transform[] spawnPoints;

    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.IsConnected)
        {
            StartCoroutine(SpawnPlayer());
            //PhotonNetwork.Instantiate(character.name, spawnPoints[PhotonNetwork.CountOfPlayers -1].position, spawnPoints[PhotonNetwork.CountOfPlayers - 1].rotation);
        }
    }
    private IEnumerator SpawnPlayer()
    {
        yield return new WaitForSeconds(2);
        PhotonNetwork.Instantiate(character.name, spawnPoints[PhotonNetwork.CurrentRoom.PlayerCount - 1].position, spawnPoints[PhotonNetwork.CurrentRoom.PlayerCount - 1].rotation);

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
