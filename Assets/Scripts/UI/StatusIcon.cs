using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StatusIcon : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        panel.ShowTooltip(info, null, sprite, this.transform.position);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        panel.HideTooltip();
    }

    private void Awake()
    {
        panel = GetComponentInParent<PanelEquipUI>();
        sprite = GetComponent<Image>().sprite;
    }


    private PanelEquipUI panel = null;

    [TextArea]
    [SerializeField]
    private string info;

    private Sprite sprite = null;
    
}
