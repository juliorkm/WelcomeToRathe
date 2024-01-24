using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Card/Hero Card")]
public class HeroCard : Card {
    [SerializeField]
    private string moniker;
    [SerializeField]
    private bool isYoung = true;
    public int life=20;
    public int intellect=4;

    public HeroCard() {
        cardType = CardTypes.Types.Hero;
    }
}
