using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelEquipUI : MonoBehaviour
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

    [SerializeField]
    private GameObject tooltipPrefab;
}
