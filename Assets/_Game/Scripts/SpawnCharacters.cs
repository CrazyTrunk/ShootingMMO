using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCharacters : MonoBehaviour
{
    [SerializeField] private GameObject character;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject[] weapons;

    [SerializeField] private Transform[] spawnWeaponPoints;
    [SerializeField] private float respawnWeaponTime = 10f;
    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.Instantiate(character.name, spawnPoints[PhotonNetwork.CurrentRoom.PlayerCount - 1].position, spawnPoints[PhotonNetwork.CurrentRoom.PlayerCount - 1].rotation);
        }
    }

    public void SpawnWeapons()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            PhotonNetwork.Instantiate(weapons[i].name, spawnWeaponPoints[i].position, spawnWeaponPoints[i].rotation);
        }
    }
}
