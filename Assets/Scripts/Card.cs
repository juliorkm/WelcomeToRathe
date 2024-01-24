using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Card/Generic Card")]
public class Card : ScriptableObject {
    [HideInInspector]
    public CardTypes.Types cardType;
    [SerializeField]
    private CardTypes.Classes[] classes;
    [SerializeField]
    private CardTypes.Talents[] talents;
    [SerializeField]
    private string cardName;
    public string cardCode;
    [SerializeField]
    private bool isLegendary;
    [SerializeField]
    private string[] specialization;
}
