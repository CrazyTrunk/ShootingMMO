using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SurvivalCount : MonoBehaviour
{
    public List<KillScore> scoreList = new List<KillScore>();
    public Text[] names;
    public Text[] killAmounts;
    [SerializeField] private GameObject killCountPanel;
    [SerializeField] private NickName namesObject;
    [SerializeField] private GameObject winnerPanel;
    [SerializeField] private Text winnerText;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
