using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasPauseMenu : MonoBehaviour
{
    public void Init(
        RetVoidParamVoidDelegate _onClickResumeCallback, 
        RetVoidParamStringDelegate _onClickChangeSceneCallback
        )
    {
        StopCoroutine("CalcScore");
        StopCoroutine("Timer");

        score = 4000;
        totalEnemyKillCnt = 0;
        totalNormalDamagePlayerDone = 0;
        totalDamageCntPlayerGain = 0;
        totalUsedAmmoCnt = 0;
        totalGoldGain = 0;
        //totalPlayerUsedSkillCnt = 0;
        sec = 0;
        min = 0;

        SetButtonDelegate(_onClickResumeCallback, _onClickChangeSceneCallback);

        panelPauseMenu.Init();
        StartCoroutine("CalcScore");
        StartCoroutine("Timer");
    }

    public void ShowPauseMenu()
    {
        panelPauseMenu.ShowPauseMenu();
    }

    public void HidePauseMenu()
    {
        panelPauseMenu.HidePauseMenu();
    }

    public void UpdateTime()
    {
        StartCoroutine("Timer");
    }

    public void UpdateTotalUsedAmmoCount()
    {
        ++totalUsedAmmoCnt;
        textTotalUsedAmmoCount.UpdateTotalUsedAmmoCount(totalUsedAmmoCnt);
    }

    public void UpdateTotalEnemyKillCount()
    {
        ++totalEnemyKillCnt;
        textTotalEnemyKillCount.UpdateTotalEnemyKillCount(totalEnemyKillCnt);
    }

    public void UpdateTotalGoldPlayerGain(int _totalGoldGain)
    {
        totalGoldGain += _totalGoldGain;
        textTotalGoldPlayerGain.UpdateTotalGoldPlayerGain(totalGoldGain);
    }

    public void UpdateTotalDamagePlayerGain()
    {
        ++totalDamageCntPlayerGain;
        textTotalDamagePlayerTaken.UpdateTotalDamagePlayerTaken(totalDamageCntPlayerGain);
    }

    public void UpdateTotalAttackDamage(int _totalNormalDamagePlayerDone)
    {
        totalNormalDamagePlayerDone += _totalNormalDamagePlayerDone;
        textTotalNormalDamagePlayerDone.UpdateTotalNormalDamagePlayerDone(totalNormalDamagePlayerDone);
    }


    private IEnumerator Timer()
    {
        while (true)
        {
            textTime.UpdateTime(min, sec);
            yield return new WaitForSeconds(1f);

            ++sec;
            if (sec >= 60)
            {
                ++min;
                sec -= 60;
            }
        }
    }

    private IEnumerator CalcScore()
    {
        while (true)
        {
            score = Mathf.Clamp(
            4000 + CalcEnemyKillScore() - (CalcTimeScore() + CalcPlayerDamageGainCountScore()),
            0,
            999999);

            textScore.UpdateScore(score);

            yield return new WaitForSeconds(1f);
        }
    }

    private int CalcTimeScore()
    {
        return (sec + min * 60) >> 1;
    }

    private int CalcEnemyKillScore()
    {
        return totalEnemyKillCnt * 50;
    }

    private int CalcPlayerDamageGainCountScore()
    {
        return totalDamageCntPlayerGain * 3;
    }

    private void SetButtonDelegate(
        RetVoidParamVoidDelegate _onClickResumeCallback,
        RetVoidParamStringDelegate _onClickChangeSceneCallback)
    {
        Button btnResume = GetComponentInChildren<ButtonResume>().transform.GetComponent<Button>();
        btnResume.onClick.AddListener(
            () =>
            {
                _onClickResumeCallback?.Invoke();
            }
            );

        Button btnRestart = GetComponentInChildren<ButtonRestart>().transform.GetComponent<Button>();
        btnRestart.onClick.AddListener(
            () =>
            {
                _onClickChangeSceneCallback?.Invoke("Stage");
            }
            );

        Button btnMain = GetComponentInChildren<ButtonMain>().transform.GetComponent<Button>();
        btnMain.onClick.AddListener(
            () =>
            {
                _onClickChangeSceneCallback?.Invoke("MainMenu");
            }
            );
    }


    private int score = 4000;
    private int totalEnemyKillCnt = 0;
    private int totalNormalDamagePlayerDone = 0;
    private int totalDamageCntPlayerGain = 0;
    private int totalUsedAmmoCnt = 0;
    private int totalGoldGain = 0;
    //private int totalPlayerUsedSkillCnt = 0;

    private int sec = 0;
    private int min = 0;

    [SerializeField]
    private PanelPauseMenu panelPauseMenu = null;
    [SerializeField]
    private TextScore textScore = null;
    [SerializeField]
    private TextTime textTime = null;
    [SerializeField]
    private TextTotalEnemyKillCount textTotalEnemyKillCount = null;
    [SerializeField]
    private TextTotalNormalDamagePlayerDone textTotalNormalDamagePlayerDone = null;
    [SerializeField]
    private TextTotalUsedAmmoCount textTotalUsedAmmoCount = null;
    [SerializeField]
    private TextTotalGoldPlayerGain textTotalGoldPlayerGain = null;
    [SerializeField]
    private TextTotalDamageCountPlayerTaken textTotalDamagePlayerTaken = null;
    [SerializeField]
    private TextTotalUsedSkillCount textUsedSkillCount = null;
}
