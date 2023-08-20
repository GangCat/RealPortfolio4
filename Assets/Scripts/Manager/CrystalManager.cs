using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ECrystalCategory
{
    None = -1,
    Slot1,
    Slot2,
    Slot3,
    Slot4,
    Presize
}

public enum ECrystalColor
{
    None = -1,
    Red = 0, Purple, Lilac,
    Lightblue, Blue, Darkblue,
    Yellow, Green, Emerald,
    Pink, Violet, LightViolet
}

public class CrystalManager : MonoBehaviour
{
    public void ShowTooltip(string _itemInfo, string _itemStatus, Sprite _itemSprite, Vector2 _pos)
    {
        tooltipPrefab.GetComponent<Tooltip>().ShowTooltip(_itemInfo, _itemStatus, _itemSprite, _pos);
    }

    public void UpdateTooltipPos(Vector2 _pos)
    {
        tooltipPrefab.GetComponent<Tooltip>().UpdateTooltipPos(_pos);
    }

    public void HideTooltip()
    {
        tooltipPrefab.GetComponent<Tooltip>().HideTooltip();
    }

    public GameObject EquipCrystal(ItemCrystal _crystal)
    {
        int prevIdx = imageCrystalSlots[(int)_crystal.crystalInfo.myCategory].PrevCrystalIdx;

        if (prevIdx == (int)_crystal.crystalInfo.myColor) // ³¢°íÀÖ´Â ³à¼®À» ´Ù½Ã ³¢·Á°í ÇÏ¸é µî¾÷
        {
            ++crystalRankArr[(int)_crystal.crystalInfo.myColor];
            if (crystalRankArr[(int)_crystal.crystalInfo.myColor] > 3)
            {
                crystalRankArr[(int)_crystal.crystalInfo.myColor] = 3;
                return crystalPrefabs[prevIdx];
            }

            SetStatus(_crystal.crystalInfo, crystalRankArr[(int)_crystal.crystalInfo.myColor]);
            imageCrystalSlots[(int)_crystal.crystalInfo.myCategory].GetComponentInChildren<TextCrystalRank>().SetRank(crystalRankArr[(int)_crystal.crystalInfo.myColor]);
            return null;
        }
        else if (prevIdx < 12)
        {
            if (crystalRankArr[prevIdx] > 1) // ³¢°íÀÖ´Â ³à¼®ÀÌ 2µî±ÞÀÎµ¥ »õ·Î¿î ³à¼®À» ³¢·Á°í ÇÏ¸é
            {
                crystalRankArr[prevIdx] = 1; // µî±Þ 1·Î ÃÊ±âÈ­
                imageCrystalSlots[(int)_crystal.crystalInfo.myCategory].GetComponentInChildren<TextCrystalRank>().SetRank(crystalRankArr[(int)_crystal.crystalInfo.myColor]);
            }
        }

        SetStatus(_crystal.crystalInfo);
        imageCrystalSlots[(int)_crystal.crystalInfo.myCategory].ChangeCrystal((int)_crystal.crystalInfo.myColor);
        

        return prevIdx < 12 ? crystalPrefabs[prevIdx] : null;
    }

    private void SetStatus(SCrystalInfo _crystalInfo, int rank = 1)
    {
        switch (_crystalInfo.myCategory)
        {
            case ECrystalCategory.None:
                break;
            case ECrystalCategory.Slot1:
                weapon.ChangeDmg(_crystalInfo.increaseAttackDmg * rank);
                weapon.ChangeAttackRate(_crystalInfo.ratioAttackRate * rank);
                break;
            case ECrystalCategory.Slot2:
                player.GetComponent<StatusSkill>().ChangeSkillDmgs(_crystalInfo.increaseSkillDmg * rank);
                player.GetComponent<StatusSkill>().ChangeSkillCooltimes(_crystalInfo.ratioSkillRate * rank);
                break;
            case ECrystalCategory.Slot3:
                player.GetComponent<StatusHP>().ChangeMaxHp(_crystalInfo.increaseMaxHp * rank);
                player.GetComponent<StatusDefense>().ChangeDefense(_crystalInfo.increaseDefense * rank);
                player.GetComponent<StatusSpeed>().ChangeSpeed(_crystalInfo.ratioMoveSpeed * rank);
                break;
            case ECrystalCategory.Slot4:
                player.GetComponent<StatusDefense>().ChangeAttributeDefenses(_crystalInfo.increaseAttributeDefense * rank);
                weapon.ChangeAttributeDmgs(_crystalInfo.increaseAttributeDmg * rank);
                break;
            case ECrystalCategory.Presize:
                break;
        }
    }

    private void Awake()
    {
        weapon = player.GetComponentInChildren<WeaponAssaultRifle>();
    }

    private void Start()
    {
        for (int i = 0; i < crystalRankArr.Length; ++i)
            crystalRankArr[i] = 1;
    }

    [Header("-Crystal Slots & Rank")]
    [SerializeField]
    private ImageCrystalSlot[] imageCrystalSlots = null;

    [Header("-Player")]
    [SerializeField]
    private GameObject player = null;

    [Header("-Crystal Prefabs")]
    [SerializeField]
    private GameObject[] crystalPrefabs = null;

    [Header("-Tooltip Prefab")]
    [SerializeField]
    private GameObject tooltipPrefab;


    private int[] crystalRankArr = new int[12];

    private WeaponAssaultRifle weapon = null;
}
