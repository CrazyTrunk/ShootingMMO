using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayColor : MonoBehaviour
{
    [SerializeField] private int[] buttonNumbers;
    [SerializeField] private int[] viewIds;
    [SerializeField] private Color32[] colors;
    [SerializeField] private PhotonView photonView;
    [SerializeField] private Renderer rendererColor;
     private GameObject namesObject;
    public int[] ViewIds { get => viewIds; set => viewIds = value; }
    private void Start()
    {
        namesObject = GameObject.Find("NameBackground");
    }
    public void ChooseColor()
    {
        photonView.RPC(nameof(AssignColor), RpcTarget.AllBuffered);
    }
    [PunRPC]
    public void AssignColor()
    {
        for(int i = 0; i <ViewIds.Length; i++)
        {
            if(photonView.ViewID == ViewIds[i])
            {
                rendererColor.material.color = colors[i];
                namesObject.GetComponent<NickName>().Names[i].gameObject.SetActive(true);
                namesObject.GetComponent<NickName>().HealthBars[i].gameObject.SetActive(true);
                namesObject.GetComponent<NickName>().Names[i].text = photonView.Owner.NickName;

            }
        }
    }
}
