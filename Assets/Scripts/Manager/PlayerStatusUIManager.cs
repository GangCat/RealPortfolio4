using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerStatusUIManager : MonoBehaviour
{
    private void Awake()
    {
        weapon = player.GetComponentInChildren<WeaponAssaultRifle>();

        player.GetComponent<StatusHP>().onMaxHpEvent.AddListener(UpdateMaxHp);
        player.GetComponent<StatusSpeed>().onSpeedEvent.AddListener(UpdateSpeed);
        player.GetComponent<StatusDefense>().onDefenseEvent.AddListener(UpdateDefense);
        player.GetComponent<StatusSkill>().onSkillDmgEvent.AddListener(UpdateSkillDmg);
        player.GetComponent<StatusSkill>().onSkillCooltimeEvent.AddListener(UpdateSkillCooltime);
        player.GetComponent<StatusDefense>().onAttributeDefenseEvent.AddListener(UpdateAttributeDefenses);

        weapon.onDmgEvent.AddListener(UpdateWeaponDmg);
        weapon.onAttributeDmgEvent.AddListener(UpdateAttributeDmgs);
        weapon.onAttackRateEvent.AddListener(UpdateWeaponAttackRate);
    }

    private void Start()
    {
        StringBuilder sb = new StringBuilder();

        textWeaponDmg.text = weapon.Dmg.ToString();
        textAttackRate.text = weapon.AttackRate.ToString();

        textMaxHp.text = player.GetComponent<StatusHP>().MaxHP.ToString();
        textDefense.text = player.GetComponent<StatusDefense>().CurDefense.ToString();

        sb.Append(player.GetComponent<StatusSkill>().SkillDmgs[0].ToString());
        sb.Append(" / ");
        sb.Append(player.GetComponent<StatusSkill>().SkillDmgs[1].ToString());
        sb.Append(" / ");
        sb.Append(player.GetComponent<StatusSkill>().SkillDmgs[2].ToString());
        textSkillDmg.text = sb.ToString();
        sb.Clear();

        sb.Append(player.GetComponent<StatusSpeed>().WalkSpeed.ToString());
        sb.Append(" / ");
        sb.Append(player.GetComponent<StatusSpeed>().RunSpeed.ToString());
        textMoveSpeed.text = sb.ToString();
        sb.Clear();

        sb.Append(weapon.AttributeDmgs[0].ToString());
        sb.Append(" / ");
        sb.Append(weapon.AttributeDmgs[1].ToString());
        sb.Append(" / ");
        sb.Append(weapon.AttributeDmgs[2].ToString());
        textAttributeDmg.text = sb.ToString();
        sb.Clear();

        sb.Append(player.GetComponent<StatusDefense>().AttributeDefenses[0].ToString());
        sb.Append(" / ");
        sb.Append(player.GetComponent<StatusDefense>().AttributeDefenses[1].ToString());
        sb.Append(" / ");
        sb.Append(player.GetComponent<StatusDefense>().AttributeDefenses[2].ToString());
        textAttributeDefense.text = sb.ToString();
        sb.Clear();

        sb.Append(player.GetComponent<StatusSkill>().SkillCooltimes[0].ToString());
        sb.Append(" / ");
        sb.Append(player.GetComponent<StatusSkill>().SkillCooltimes[1].ToString());
        sb.Append(" / ");
        sb.Append(player.GetComponent<StatusSkill>().SkillCooltimes[2].ToString());
        textSkillCooltime.text = sb.ToString();
        sb.Clear();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            if (!isInvenOpen)
            {
                StopCoroutine("CloseInventory");
                StartCoroutine("OpenInventory");
            }
            else
            {
                StopCoroutine("OpenInventory");
                StartCoroutine("CloseInventory");
            }
        }

        if (isInvenOpen)
        {
            //OnMouseOver evt = new OnMouseOver();
            //Debug.Log(evt.target);
        }
    }

    private void UpdateMaxHp(float _maxHp)
    {
        textMaxHp.text = _maxHp.ToString();
    }

    private void UpdateWeaponDmg(float _dmg)
    {
        textWeaponDmg.text = _dmg.ToString();
    }

    private void UpdateWeaponAttackRate(float _attackRate)
    {
        textAttackRate.text = _attackRate.ToString();
    }

    private void UpdateDefense(float _defense)
    {
        textDefense.text = _defense.ToString();
    }

    private void UpdateSkillDmg(float[] _skillDmg)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(_skillDmg[0].ToString());
        sb.Append(" / ");
        sb.Append(_skillDmg[1].ToString());
        sb.Append(" / ");
        sb.Append(_skillDmg[2].ToString());
        textSkillDmg.text = sb.ToString();
        sb.Clear();
    }

    private void UpdateSpeed(float _walkSpeed, float _runSpeed)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append(_walkSpeed.ToString());
        sb.Append(" / ");
        sb.Append(_runSpeed.ToString());
        textMoveSpeed.text = sb.ToString();
        sb.Clear();
    }

    private void UpdateAttributeDmgs(float[] _attributeDmgs)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(_attributeDmgs[0].ToString());
        sb.Append(" / ");
        sb.Append(_attributeDmgs[1].ToString());
        sb.Append(" / ");
        sb.Append(_attributeDmgs[2].ToString());
        textAttributeDmg.text = sb.ToString();
        sb.Clear();
    }

    private void UpdateAttributeDefenses(float[] _attributeDefenses)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(_attributeDefenses[0].ToString());
        sb.Append(" / ");
        sb.Append(_attributeDefenses[1].ToString());
        sb.Append(" / ");
        sb.Append(_attributeDefenses[2].ToString());
        textAttributeDmg.text = sb.ToString();
        sb.Clear();
    }

    private void UpdateSkillCooltime(float[] _skillCooltimes)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(_skillCooltimes[0].ToString());
        sb.Append(" / ");
        sb.Append(_skillCooltimes[1].ToString());
        sb.Append(" / ");
        sb.Append(_skillCooltimes[2].ToString());
        textSkillCooltime.text = sb.ToString();
        sb.Clear();
    }

    private IEnumerator OpenInventory()
    {
        isInvenOpen = true;
        float percent = 0.0f;
        float curTime = Time.time;
        while (percent < 1.0f)
        {
            percent = (Time.time - curTime) * 0.5f;

            equipPartsPanelRt.anchoredPosition = Vector2.Lerp(equipPartsPanelRt.anchoredPosition, new Vector3(0, equipPartsPanelRt.anchoredPosition.y), percent);
            statusPanelRt.anchoredPosition = Vector2.Lerp(statusPanelRt.anchoredPosition, new Vector2(0, statusPanelRt.anchoredPosition.y), percent);
            setEffectPanelRt.anchoredPosition = Vector2.Lerp(setEffectPanelRt.anchoredPosition, new Vector2(600, setEffectPanelRt.anchoredPosition.y), percent);

            yield return null;
        }

        equipPartsPanelRt.anchoredPosition = new Vector2(0, equipPartsPanelRt.anchoredPosition.y);
        statusPanelRt.anchoredPosition = new Vector2(0, statusPanelRt.anchoredPosition.y);
        setEffectPanelRt.anchoredPosition = new Vector2(600, setEffectPanelRt.anchoredPosition.y);
    }

    private IEnumerator CloseInventory()
    {
        isInvenOpen = false;
        float percent = 0.0f;
        float curTime = Time.time;
        while (percent < 1.0f)
        {
            percent = (Time.time - curTime) * 0.5f;

            equipPartsPanelRt.anchoredPosition = Vector2.Lerp(equipPartsPanelRt.anchoredPosition, new Vector3(-600, equipPartsPanelRt.anchoredPosition.y), percent);
            statusPanelRt.anchoredPosition = Vector2.Lerp(statusPanelRt.anchoredPosition, new Vector2(-600, statusPanelRt.anchoredPosition.y), percent);
            setEffectPanelRt.anchoredPosition = Vector2.Lerp(setEffectPanelRt.anchoredPosition, new Vector2(-300, setEffectPanelRt.anchoredPosition.y), percent);

            yield return null;
        }
        equipPartsPanelRt.anchoredPosition = new Vector3(-600, equipPartsPanelRt.anchoredPosition.y);
        statusPanelRt.anchoredPosition = new Vector2(-600, statusPanelRt.anchoredPosition.y);
        setEffectPanelRt.anchoredPosition = new Vector2(-300, setEffectPanelRt.anchoredPosition.y);
    }


    [Header("-Status Text")]
    [SerializeField]
    private TextMeshProUGUI textMaxHp;
    [SerializeField]
    private TextMeshProUGUI textWeaponDmg;
    [SerializeField]
    private TextMeshProUGUI textSkillDmg;
    [SerializeField]
    private TextMeshProUGUI textAttackRate;
    [SerializeField]
    private TextMeshProUGUI textDefense;

    [SerializeField]
    private TextMeshProUGUI textMoveSpeed; // 걷기속도 / 달리기 속도
    [SerializeField]
    private TextMeshProUGUI textAttributeDmg;
    [SerializeField]
    private TextMeshProUGUI textAttributeDefense;
    [SerializeField]
    private TextMeshProUGUI textSkillCooltime;

    [Header("-Panel Transform")]
    [SerializeField]
    private RectTransform statusPanelRt;
    [SerializeField]
    private RectTransform equipPartsPanelRt;
    [SerializeField]
    private RectTransform setEffectPanelRt;

    [Header("-Player")]
    [SerializeField]
    private GameObject player;

    private WeaponAssaultRifle weapon;

    
    private bool isInvenOpen = false;
}
