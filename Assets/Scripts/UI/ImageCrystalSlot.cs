using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ImageCrystalSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public int PrevCrystalIdx => prevCrystalIdx;

    public void ChangeCrystal(int _crystalIdx)
    {
        image.sprite = crystalPrefabs[_crystalIdx].GetComponent<SpriteRenderer>().sprite;
        prevCrystalIdx = _crystalIdx;
        StartCoroutine("ResetTooltip");
    }

    private IEnumerator ResetTooltip()
    {
        GetComponent<Image>().raycastTarget = false;
        yield return new WaitForEndOfFrame();
        GetComponent<Image>().raycastTarget = true;
    }

    private void Awake()
    {
        image = GetComponent<Image>();
        panel = GetComponentInParent<PanelEquipUI>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (prevCrystalIdx < 12)
        {
            SCrystalInfo info = crystalPrefabs[prevCrystalIdx].GetComponent<ItemCrystal>().crystalInfo;
            panel.ShowTooltip(info.itemInfo, info.itemStatus, info.mySprite, transform.position);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (prevCrystalIdx < 12)
            panel.HideTooltip();
    }

    [SerializeField]
    private GameObject[] crystalPrefabs;

    private PanelEquipUI panel = null;
    private Image image = null;
    private int prevCrystalIdx = 12;
}
