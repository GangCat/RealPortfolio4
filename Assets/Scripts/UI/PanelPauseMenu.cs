using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PanelPauseMenu : MonoBehaviour
{
    public delegate void OnClickResumeDelegate();
    public delegate void OnClickRestartDelegate(string _sceneName);
    public delegate void OnClickMainDelegate(string _sceneName);

    public OnClickResumeDelegate OnClickResumeCallback
    {
        set { onClickResumeCallback = value; }
    }

    public OnClickRestartDelegate OnClickRestartCallback
    {
        set { onClickRestartCallback = value; }
    }

    public OnClickMainDelegate OnClickMainCallback
    {
        set { onClickMainCallback = value; }
    }

    public void UpdateTime()
    {
        startTime = Time.time;
        StartCoroutine("Timer");
    }

    public void UpdateUsedAmmo()
    {
        ++usedAmmoCnt;
        textUsedAmmo.text = string.Format("»ç¿ëÇÑ ÃÑ¾Ë ¼ö : {0:D3}", usedAmmoCnt);
    }

    public void UpdateDeadEnemy()
    {
        ++deadEnemyCnt;
        textDeadEnemy.text = string.Format("Á×ÀÎ ¸ó½ºÅÍ ¼ö : {0:D2}", deadEnemyCnt);
    }

    public void UpdateGold(int _totalGold)
    {
        totalGold += _totalGold;
        textTotalGold.text = string.Format("ÃÑ È¹µæ °ñµå : {0:N0}", totalGold);
    }

    public void UpdateDamagedCount()
    {
        ++playerDamagedCnt;
        textPlayerDamaged.text = string.Format("ÇÇ°Ý È½¼ö : {0}È¸", playerDamagedCnt);
    }

    public void UpdateTotalAttackDamage(int _dmg)
    {
        playerTotalDamage += _dmg;
        textTotalDamage.text = string.Format("Àû¿¡°Ô °¡ÇÑ ÃÑ µ¥¹ÌÁö: {0:N0}", playerTotalDamage);
    }

    

    public void ShowPauseMenu()
    {
        StopCoroutine("PauseMenuAnimation");
        StartCoroutine("PauseMenuAnimation", true);
    }

    public void ClosePauseMenu()
    {
        StopCoroutine("PauseMenuAnimation");
        StartCoroutine("PauseMenuAnimation", false);
    }

    private IEnumerator Timer()
    {
        while (true)
        {
            sec += Time.deltaTime;
            if (sec >= 60)
            {
                ++min;
                sec -= 60;
            }

            textTime.text = string.Format("Time: {0:D2}:{1:D2}", min, (int)sec);

            yield return null;
        }
    }

    private void CalcScore()
    {
        score = Mathf.Clamp(
            4000 + deadEnemyCnt * 50 - (int)((Time.time - startTime) * 0.5f) - playerDamagedCnt * 3, 
            0, 
            999999);

        textScore.text = string.Format("Score: {0:N0}", score);
    }

    private IEnumerator PauseMenuAnimation(bool _isOpen)
    {
        float percent = 0f;
        float curTime = Time.time;

        while(percent < 1f)
        {
            float scaleY = Mathf.Lerp(rectTr.localScale.y, _isOpen == true ? 1 : 0, percent);
            rectTr.localScale = new Vector2(1, scaleY);
            percent = (Time.time - curTime) / 0.5f ;
            yield return null;
        }
    }

    private void Awake()
    {
        rectTr = GetComponent<RectTransform>();
        SetButtonDelegate();
    }

    private void Update()
    {
        CalcScore();
    }


    private void SetButtonDelegate()
    {
        Button btnResume = GetComponentInChildren<ButtonResume>().transform.GetComponent<Button>();
        btnResume.onClick.AddListener(
            () =>
            {
                onClickResumeCallback?.Invoke();
            }
            );

        Button btnRestart = GetComponentInChildren<ButtonRestart>().transform.GetComponent<Button>();
        btnRestart.onClick.AddListener(
            () =>
            {
                onClickRestartCallback?.Invoke("Stage");
            }
            );

        Button btnMain = GetComponentInChildren<ButtonMain>().transform.GetComponent<Button>();
        btnMain.onClick.AddListener(
            () =>
            {
                onClickMainCallback?.Invoke("MainMenu");
            }
            );
    }

    public void Init()
    {
        rectTr.localScale = new Vector2(1f, 0f);
        rectTr.localPosition = Vector3.zero;
    }

    private void Start()
    {
    }

    private int score = 2000;
    private int deadEnemyCnt = 0;
    private int playerTotalDamage = 0;
    private int usedAmmoCnt = 0;
    private int totalGold = 0;
    private int playerDamagedCnt = 0;
    private int usedSkillCnt = 0;

    private float startTime = 0f;
    private float sec = 0f;
    private int min = 0;

    private RectTransform rectTr = null;

    private OnClickResumeDelegate onClickResumeCallback = null;
    private OnClickRestartDelegate onClickRestartCallback = null;
    private OnClickMainDelegate onClickMainCallback = null;


    [SerializeField]
    private TextMeshProUGUI textScore = null;
    [SerializeField]
    private TextMeshProUGUI textTime = null;
    [SerializeField]
    private TextMeshProUGUI textDeadEnemy = null;
    [SerializeField]
    private TextMeshProUGUI textTotalDamage = null;
    [SerializeField]
    private TextMeshProUGUI textUsedAmmo = null;
    [SerializeField]
    private TextMeshProUGUI textTotalGold = null;
    [SerializeField]
    private TextMeshProUGUI textPlayerDamaged = null;
    [SerializeField]
    private TextMeshProUGUI textUsedSkill = null;
}
