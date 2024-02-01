using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NickName : MonoBehaviour
{
    [SerializeField] private Text[] names;
    [SerializeField] private Image[] healthBars;

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
}
