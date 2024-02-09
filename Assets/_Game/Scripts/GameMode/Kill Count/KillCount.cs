using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillCount : MonoBehaviour
{
    public List<KillScore> scoreList = new List<KillScore>();
    public Text[] names;
    public Text[] killAmounts;
    [SerializeField] private GameObject killCountPanel;
    [SerializeField] private NickName namesObject;
    [SerializeField] private GameObject winnerPanel;
    [SerializeField] private Text winnerText;

    // Start is called before the first frame update
    public bool countDown;
    void Start()
    {
        killCountPanel.SetActive(false);
        winnerPanel.SetActive(false);
        countDown = true;
    }

    // Update is called once per frame
    void Update()
    {
        scoreList.Clear();
        if (Input.GetKeyDown(KeyCode.Tab) && countDown)
        {

            killCountPanel.SetActive(true);
            //update score tu list
            for (int i = 0; i < names.Length; i++)
            {
                scoreList.Add(new KillScore(namesObject.Names[i].text, namesObject.Kills[i]));
            }
            scoreList.Sort();
            //sau khi sort
            for (int i = 0; i < names.Length; i++)
            {
                names[i].text = scoreList[i].playerName;
                killAmounts[i].text = scoreList[i].playerKills.ToString();
            }
        }
        else if (Input.GetKeyUp(KeyCode.Tab))
        {
            killCountPanel.SetActive(false);

        }
    }
    public void TimeOver()
    {
        killCountPanel.SetActive(true);
        winnerPanel.SetActive(true);
        //update score tu list
        scoreList.Clear();
        for (int i = 0; i < names.Length; i++)
        {
            scoreList.Add(new KillScore(namesObject.Names[i].text, namesObject.Kills[i]));
        }
        scoreList.Sort();
        winnerText.text = scoreList[0].playerName;
        //sau khi sort
        for (int i = 0; i < names.Length; i++)
        {
            names[i].text = scoreList[i].playerName;
            killAmounts[i].text = scoreList[i].playerKills.ToString();
        }
    }
}
