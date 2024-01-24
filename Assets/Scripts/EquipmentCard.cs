using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Card/Equipment Card")]
public class EquipmentCard : Card {
    [SerializeField]
    private int block;
    public CardTypes.SubTypes subType;

    public EquipmentCard() {
        cardType = CardTypes.Types.Equipment;
    }
}
