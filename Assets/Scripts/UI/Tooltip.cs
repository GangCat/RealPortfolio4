using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    public void ShowTooltip(string _itemInfo, string _itemStatus, Sprite _itemSprite, Vector2 _pos)
    {
        EditInfoText(_itemInfo);
        EditStatusText(_itemStatus);
        ChangeSprite(_itemSprite);
        SetPosition(_pos);

        //gameObject.transform.localScale = Vector3.one * 0.01f;
        gameObject.SetActive(true);
        StartCoroutine("ShowTooltipAnim");
    }

    private IEnumerator ShowTooltipAnim()
    {
        float percent = 0.0f;
        float curTime = Time.time;

        while(percent < 1)
        {
            percent = (Time.time - curTime) / 0.25f;
            gameObject.transform.localScale = Vector3.Lerp(new Vector3(1f, 0.01f, 1f), new Vector3(1f, 1f, 1f), percent);
            yield return null;
        }
    }

    public void UpdateTooltipPos(Vector2 _pos)
    {
        SetPosition(_pos);
    }

    public void HideTooltip()
    {
        StopCoroutine("ShowTooltipAnim");
        gameObject.SetActive(false);
    }


    private void EditInfoText(string _textInfo)
    {
        textInfo.text = _textInfo;
    }

    private void EditStatusText(string _textStatus)
    {
        textStatus.text = _textStatus;
    }

    private void ChangeSprite(Sprite _sprite)
    {
        imageItem.sprite = _sprite;
    }

    private void SetPosition(Vector2 _pos)
    {
        rectTr.position = _pos;
    }

    private void Awake()
    {
        rectTr = GetComponent<RectTransform>();
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    [SerializeField]
    private TextMeshProUGUI textInfo = null;
    [SerializeField]
    private TextMeshProUGUI textStatus = null;
    [SerializeField]
    private Image imageItem = null;

    private RectTransform rectTr = null;
}
