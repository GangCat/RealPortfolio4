using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUDManager : MonoBehaviour
{
    [Header("-Components")]
    [SerializeField]
    private WeaponAssaultRifle weapon = null; // 현재 정보가 출력되고 있는 무기
    [SerializeField]
    private StatusHP statusHP = null;

    [Header("-HP & BloodScreenUI")]
    [SerializeField]
    private Image[] ImagesHpbarFront;
    [SerializeField]
    private Image[] imagesBloodScreen;

    [Header("-WeaponHud")]
    [SerializeField]
    private TextMeshProUGUI textWeaponCurammo;


    private void Awake()
    {
        weapon.onAmmoEvent.AddListener(UpdateWeaponHUD);
        statusHP.onHPEvent.AddListener(UpdateHPHDU);
    }

    private void UpdateHPHDU(float _prevHP, float _curHP)
    {
        foreach(Image image in ImagesHpbarFront)
            image.rectTransform.localScale = new Vector3((_curHP / statusHP.MaxHP) * 100, 1f, 1f);

        if (_prevHP > _curHP)
        {
            float ratioHP = statusHP.GetRatio();
            StopCoroutine("OnBloodScreen");
            StartCoroutine("OnBloodScreen", ratioHP < 0.3f);
        }
        else
        {
            if (statusHP.GetRatio() > 0.3f)
            {
                Color color = imagesBloodScreen[0].color;
                color.a = 0;
                imagesBloodScreen[0].color = color;
            }
        }
    }

    private void UpdateWeaponHUD(float _curAmmo)
    {
        textWeaponCurammo.text = "[" + _curAmmo.ToString() + "]";
    }

    private IEnumerator OnBloodScreen(bool _isLow)
    {
        float percent = 0f;
        Image nowBloodScreen = _isLow ? imagesBloodScreen[1] : imagesBloodScreen[0];
        while (percent < 1f)
        {
            percent += Time.deltaTime;

            Color color = nowBloodScreen.color;
            color.a = Mathf.Lerp(0.2f, 0, percent);
            nowBloodScreen.color = color;

            yield return null;
        }
    }
}