using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Card/Weapon Card")]
public class WeaponCard : Card {
    [SerializeField]
    private CardTypes.WeaponTypes weaponType;
    public int numberOfHands=2;
    [SerializeField]
    private int power=4;

    public WeaponCard() {
        cardType = CardTypes.Types.Weapon;
    }
}
