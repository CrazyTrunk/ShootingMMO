using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NickName : MonoBehaviour
{
    [SerializeField] private Text[] names;
    [SerializeField] private Image[] healthBars;

    public Text[] Names { get => names; set => names = value; }
}
