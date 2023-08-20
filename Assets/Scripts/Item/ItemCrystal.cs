using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemCrystal : ItemBase
{
    public int MyRank 
    { 
        get => crystalInfo.myRank;
        set => crystalInfo.myRank = value;
    }
    protected override void Awake()
    {
        base.Awake();
        crystalManager = GetComponentInParent<CrystalManager>();
    }

    public override void Use(GameObject _entity)
    {
        if (_entity.CompareTag("Player"))
        {
            GameObject tempGo = crystalManager.EquipCrystal(this);
            if (tempGo == null)
            {
                crystalManager.HideTooltip();
                Destroy(gameObject);
                return;
            }

            GameObject crystalGo = Instantiate(tempGo, crystalManager.transform);

            if (crystalGo == null)
            {
                crystalManager.HideTooltip();
                Destroy(gameObject);
            }

            Vector3 spawnPos = transform.position;
            spawnPos.y = 0.4f;

            crystalGo.transform.position = spawnPos;

            crystalManager.HideTooltip();

            Destroy(gameObject);
        }
    }

    public void SellCrystal(GameObject _entity)
    {
        if (_entity.CompareTag("Player"))
        {
            _entity.GetComponent<StatusGold>().IncreaseGold(60);
            crystalManager.HideTooltip();
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider _other)
    {
        if (_other.CompareTag("Player"))
        {
            Vector2 tooltipPos = Camera.main.WorldToScreenPoint(transform.position);
            crystalManager.ShowTooltip(crystalInfo.itemInfo, crystalInfo.itemStatus, crystalInfo.mySprite, tooltipPos);
        }
    }

    private void OnTriggerStay(Collider _other)
    {
        if (_other.CompareTag("Player"))
        {
            Vector2 tooltipPos = Camera.main.WorldToScreenPoint(transform.position);
            crystalManager.UpdateTooltipPos(tooltipPos);
        }
    }

    private void OnTriggerExit(Collider _other)
    {
        if(_other.CompareTag("Player"))
        {
            crystalManager.HideTooltip();
        }
    }

    private CrystalManager crystalManager;
    public SCrystalInfo crystalInfo;
}
