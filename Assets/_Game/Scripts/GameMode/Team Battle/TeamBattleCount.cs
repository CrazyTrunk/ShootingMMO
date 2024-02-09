using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeamBattleCount : MonoBehaviour
{
    public List<KillScore> scoreList = new List<KillScore>();
    public Text[] names;
    public Text[] killAmounts;
    [SerializeField] private GameObject killCountPanel;
    [SerializeField] private NickName namesObject;
    [SerializeField] private GameObject winnerPanel;
    [SerializeField] private Text winnerText;

    // Start is called before the first frame update
    private int RedTeamKills;
    private int BlueTeamKills;

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
            //teambattle maximum is 6 players
            for (int i = 0; i < 6; i++)
            {
                scoreList.Add(new KillScore(namesObject.Names[i].text, namesObject.Kills[i]));
            }
            RedTeamKills = scoreList[0].playerKills + scoreList[1].playerKills + scoreList[2].playerKills;
            BlueTeamKills = scoreList[3].playerKills + scoreList[4].playerKills + scoreList[5].playerKills;
            killAmounts[0].text = RedTeamKills.ToString();
            killAmounts[1].text = BlueTeamKills.ToString();

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
        scoreList.Clear();
        for (int i = 0; i < 6; i++)
        {
            scoreList.Add(new KillScore(namesObject.Names[i].text, namesObject.Kills[i]));
        }
        RedTeamKills = scoreList[0].playerKills + scoreList[1].playerKills + scoreList[2].playerKills;
        BlueTeamKills = scoreList[3].playerKills + scoreList[4].playerKills + scoreList[5].playerKills;
        killAmounts[0].text = RedTeamKills.ToString();
        killAmounts[1].text = BlueTeamKills.ToString();
        if (RedTeamKills > BlueTeamKills)
        {
            winnerText.text = $"Red Team Won With Score: {RedTeamKills}";
        }
        else
        {
            winnerText.text = $"Blue Team Won With Score: {BlueTeamKills}";
        }
    }
}
