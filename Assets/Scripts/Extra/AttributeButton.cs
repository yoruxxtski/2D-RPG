using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributeButton : MonoBehaviour
{
    public static event Action<AttributeType> OnAttributeSelectedEvent;
    [SerializeField] private AttributeType attribute;

    public void SelectAttribute() {
        OnAttributeSelectedEvent?.Invoke(attribute);
    }
}
