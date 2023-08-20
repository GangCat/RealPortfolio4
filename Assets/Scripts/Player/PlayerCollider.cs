using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    public OnPlayerDamagedDelegate OnPlayerDamagedCallback
    {
        set => onPlayerDamagedCallback = value;
    }

    public void SetCollider(bool _boolean)
    {
        myCollider.enabled = _boolean;
    }

    public void TakeDmg(float _dmg)
    {
        if (!playerAnim.CurAnimationIs("Dash"))
        {
            bool isDead = statusHp.DecreaseHp(_dmg);
            onPlayerDamagedCallback?.Invoke();

            if (isDead)
                Debug.Log("GameOver");
        }
    }

    public void IgnoreLayer(int _minLayerMask, int _maxLayerMask, bool _ignore)
    {
        Physics.IgnoreLayerCollision(_minLayerMask, _maxLayerMask, _ignore);
    }

    private void OnTriggerEnter(Collider _other)
    {
        if (_other.CompareTag("Item"))
            _other.GetComponent<ItemBase>().Use(gameObject);
    }

    private void OnTriggerStay(Collider _other)
    {
        if (_other.CompareTag("Crystal"))
        {
            if (GetComponent<PlayerInputManager>().IsInteract)
                _other.GetComponent<ItemBase>().Use(gameObject);
            else if (GetComponent<PlayerInputManager>().IsSellCrystal)
                _other.GetComponent<ItemCrystal>().SellCrystal(gameObject);
        }
        else if (_other.CompareTag("Interactive"))
        {
            if (GetComponent<PlayerInputManager>().IsInteract)
                _other.GetComponent<InteractiveBase>().Use();
        }
    }

    private void Awake()
    {
        myCollider = GetComponent<Collider>();
        statusHp = GetComponent<StatusHP>();
        playerAnim = GetComponent<PlayerAnimatorController>();
    }

    private Collider myCollider = null;
    private StatusHP statusHp = null;
    private PlayerAnimatorController playerAnim = null;
    private OnPlayerDamagedDelegate onPlayerDamagedCallback = null;
}
