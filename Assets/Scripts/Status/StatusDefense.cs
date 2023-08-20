using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseEvent : UnityEngine.Events.UnityEvent<float> { }
public class AttributeDefenseEvent : UnityEngine.Events.UnityEvent<float[]> { }
public class StatusDefense : MonoBehaviour
{
    public DefenseEvent onDefenseEvent = new DefenseEvent();
    public AttributeDefenseEvent onAttributeDefenseEvent = new AttributeDefenseEvent();

    public float CurDefense => defense;
    public float[] AttributeDefenses => attributeDefenses;

    public float DefenseDmg(float _dmg)
    {
        return _dmg -= defense > 0 ? defense : 0;
    }

    #region BuffDefense
    public void BuffDefense(float _ratio, float _duration)
    {
        if (isDefenseBuff)
            StopCoroutine("ResetDefense");

        isDefenseBuff = true;

        defense = defense * _ratio > maxDefense ? maxDefense : defense * _ratio;

        onDefenseEvent.Invoke(defense);

        StartCoroutine("ResetDefense", _duration);
    }

    private IEnumerator ResetDefense(float _duration)
    {
        yield return new WaitForSeconds(_duration);

        defense = oriDefense;

        onDefenseEvent.Invoke(defense);
        isDefenseBuff = false;
    }
    #endregion

    #region BuffAttDefense
    public void BuffAttributeDefenses(float _ratio, float _duration)
    {
        if (isAttributeDefenseBuff)
            StopCoroutine("ResetAttDefences");

        isAttributeDefenseBuff = true;

        for(int i = 0; i < attributeDefenses.Length; ++i)
            attributeDefenses[i] = attributeDefenses[i] * _ratio > maxAttributeDefense ? maxAttributeDefense : attributeDefenses[i] * _ratio;

        onAttributeDefenseEvent.Invoke(attributeDefenses);

        StartCoroutine("ResetAttDefences", _duration);
    }

    private IEnumerator ResetAttDefences(float _duration)
    {
        yield return new WaitForSeconds(_duration);

        for (int i = 0; i < attributeDefenses.Length; ++i)
            attributeDefenses[i] = oriAttributeDefenses[i];

        onAttributeDefenseEvent.Invoke(attributeDefenses);

        isAttributeDefenseBuff = false;
    }
    #endregion

    public void ChangeDefense(float _increaseDefense)
    {
        defense = oriDefense + _increaseDefense;

        onDefenseEvent.Invoke(defense);
    }

    public void ChangeAttributeDefenses(float _increaseAttDefense)
    {
        for (int i = 0; i < attributeDefenses.Length; ++i)
            attributeDefenses[i] = oriAttributeDefenses[i] + _increaseAttDefense;

        onAttributeDefenseEvent.Invoke(attributeDefenses);
    }


    private void Start()
    {
        oriDefense = defense;
        oriAttributeDefenses = new float[attributeDefenses.Length];

        for (int i = 0; i < attributeDefenses.Length; ++i)
            oriAttributeDefenses[i] = attributeDefenses[i];
    }

    [SerializeField]
    private float defense;
    [SerializeField]
    private float maxDefense;
    [SerializeField]
    private float[] attributeDefenses;
    [SerializeField]
    private float maxAttributeDefense;

    private bool isDefenseBuff = false;
    private bool isAttributeDefenseBuff = false;

    private float oriDefense;
    private float[] oriAttributeDefenses;
}
