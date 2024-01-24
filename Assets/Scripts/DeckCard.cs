using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Card/Deck Card")]
public class DeckCard : Card {
    public CardTypes.Types cardType;
    public CardTypes.SubTypes cardSubtype;
    [SerializeField]
    private int pitch;
    [SerializeField]
    private int cost;
    [SerializeField]
    private int power;
    [SerializeField]
    private int block;
}
