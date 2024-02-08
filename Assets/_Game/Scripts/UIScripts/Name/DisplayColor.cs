using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayColor : MonoBehaviourPunCallbacks
{
    [SerializeField] private int[] buttonNumbers;
    [SerializeField] private int[] viewIds;
    [SerializeField] private Color32[] colors;
    [SerializeField] private PhotonView photonViewObj;
    [SerializeField] private Renderer rendererColor;
    [SerializeField] private AudioClip[] gunshotSounds;
    [SerializeField] private AudioSource audioSource;

    [Header("Animator")]
    [SerializeField] private Animator anim;

    [Header("Component When IsDead Run")]
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private WeaponChangerAdvanced weaponChangerAdvanced;
    [SerializeField] private AimLookAt aimLookAt;


    private GameObject namesObject;
    private GameObject wairForPlayers;
    private GameObject timeObject;

    public int[] ViewIds { get => viewIds; set => viewIds = value; }



    private void Start()
    {
        namesObject = GameObject.Find("NameBackground");
        timeObject = GameObject.Find("TimePanel");
        wairForPlayers = GameObject.Find("WaitingPanel");
        InvokeRepeating(nameof(CheckTime), 1, 1);

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (photonViewObj.IsMine && !wairForPlayers.activeInHierarchy)
            {
                RemoveData();
                RoomExit();
            }
        }
        if (anim.GetBool("Hit"))
        {
            StartCoroutine(BackToIdle());
        }
    }
    private void CheckTime()
    {
        if (timeObject.GetComponent<TimeCounter>().TimeStop)
        {
            playerMovement.IsDead = true;
            playerMovement.gameOver = true;
            weaponChangerAdvanced.IsDead = true;
            aimLookAt.IsDead = true;
            gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        }
    }
    IEnumerator BackToIdle()
    {
        yield return new WaitForSeconds(0.05f);
        anim.SetBool("Hit", false);
    }

    private void RoomExit()
    {
        StartCoroutine(GetReadyToleave());
    }
    public void PlayGunShot(string name, int weaponNumber)
    {
        photonViewObj.RPC(nameof(PlaySound), RpcTarget.All, name, weaponNumber);
    }
    [PunRPC]

    private void PlaySound(string name, int weaponNumber)
    {
        for (int i = 0; i < namesObject.GetComponent<NickName>().Names.Length; i++)
        {
            if (name == namesObject.GetComponent<NickName>().Names[i].text)
            {
                audioSource.clip = gunshotSounds[weaponNumber];
                audioSource.Play();
            }
        }
    }
    public void DamageDeal(string shooterName, string name, float damageAmt)
    {
        photonViewObj.RPC(nameof(DealDamage), RpcTarget.AllBuffered, shooterName, name, damageAmt);
    }


    private void RemoveData()
    {
        photonViewObj.RPC(nameof(RemoveCurrentPlayer), RpcTarget.AllBuffered);
    }



    public void ChooseColor()
    {
        photonViewObj.RPC(nameof(AssignColor), RpcTarget.AllBuffered);
    }
    public void Respawn(string name)
    {
        photonViewObj.RPC(nameof(ResetForReplay),RpcTarget.AllBuffered, name);
    }

    [PunRPC]
    private void ResetForReplay(string name)
    {
        for(int i = 0;i < namesObject.GetComponent<NickName>().Names.Length;i++)
        {
            if(name == namesObject.GetComponent<NickName>().Names[i].text)
            {
                anim.SetBool("Dead", false);
                playerMovement.IsDead = false;
                weaponChangerAdvanced.IsDead = false;
                aimLookAt.IsDead = false;
                gameObject.layer = LayerMask.NameToLayer("Default");
                namesObject.GetComponent<NickName>().HealthBars[i].GetComponent<Image>().fillAmount = 1;
            }
        }
    }

    [PunRPC]
    public void AssignColor()
    {
        for (int i = 0; i < ViewIds.Length; i++)
        {
            if (photonViewObj.ViewID == ViewIds[i])
            {
                rendererColor.material.color = colors[i];
                namesObject.GetComponent<NickName>().Names[i].gameObject.SetActive(true);
                namesObject.GetComponent<NickName>().HealthBars[i].gameObject.SetActive(true);
                namesObject.GetComponent<NickName>().Names[i].text = photonViewObj.Owner.NickName;

            }
        }
    }
    [PunRPC]
    private void RemoveCurrentPlayer()
    {
        for (int i = 0; i < namesObject.GetComponent<NickName>().Names.Length; i++)
        {
            if (photonViewObj.Owner.NickName == namesObject.GetComponent<NickName>().Names[i].text)
            {
                namesObject.GetComponent<NickName>().Names[i].gameObject.SetActive(false);
                namesObject.GetComponent<NickName>().HealthBars[i].gameObject.SetActive(false);

            }
        }

    }

    [PunRPC]

    private void DealDamage(string shooterName, string name, float dmgAmount)
    {
        for (int i = 0; i < namesObject.GetComponent<NickName>().Names.Length; i++)
        {
            if (name == namesObject.GetComponent<NickName>().Names[i].text)
            {
                if (namesObject.GetComponent<NickName>().HealthBars[i].GetComponent<Image>().fillAmount > 0.1f)
                {
                    anim.SetBool("Hit", true);
                    namesObject.GetComponent<NickName>().HealthBars[i].GetComponent<Image>().fillAmount -= dmgAmount;
                }
                else
                {
                    anim.SetBool("Dead", true);
                    namesObject.GetComponent<NickName>().HealthBars[i].GetComponent<Image>().fillAmount = 0;
                    playerMovement.IsDead = true;
                    weaponChangerAdvanced.IsDead = true;
                    aimLookAt.IsDead = true;
                    namesObject.GetComponent<NickName>().RunMessage(shooterName, name);
                    gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
                }

            }
        }
    }
    IEnumerator GetReadyToleave()
    {
        yield return new WaitForSeconds(1);
        namesObject.GetComponent<NickName>().Leaving();
        Cursor.visible = true;
        PhotonNetwork.LeaveRoom();
    }
}
