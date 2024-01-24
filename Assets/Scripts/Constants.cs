using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants {
    public enum Zones {
        Deck        = 0b0000_0000_0000_0000_0001,
        Hero        = 0b0000_0000_0000_0000_0010,
        WeaponL     = 0b0000_0000_0000_0000_0100,
        WeaponR     = 0b0000_0000_0000_0000_1000,
        Arsenal     = 0b0000_0000_0000_0001_0000,
        Head        = 0b0000_0000_0000_0010_0000,
        Chest       = 0b0000_0000_0000_0100_0000,
        Arms        = 0b0000_0000_0000_1000_0000,
        Legs        = 0b0000_0000_0001_0000_0000,
        Pitch       = 0b0000_0000_0010_0000_0000,
        Graveyard   = 0b0000_0000_0100_0000_0000,
        Banished    = 0b0000_0000_1000_0000_0000,
        Field       = 0b0000_0001_0000_0000_0000,
        CombatChain = 0b0000_0010_0000_0000_0000,
        Stack       = 0b0000_0100_0000_0000_0000,
        Soul        = 0b0000_1000_0000_0000_0000,
        Sideboard   = 0b0001_0000_0000_0000_0000,
        Hand        = 0b0010_0000_0000_0000_0000,
    }
}
