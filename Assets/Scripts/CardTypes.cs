using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardTypes
{
    public enum Classes {
        Generic       = 0b0000_0000_0000_0000,
        Brute         = 0b0000_0000_0000_0001,
        Guardian      = 0b0000_0000_0000_0010,
        Warrior       = 0b0000_0000_0000_0100,
        Ninja         = 0b0000_0000_0000_1000,
        Mechanologist = 0b0000_0000_0001_0000,
        Ranger        = 0b0000_0000_0010_0000,
        Runeblade     = 0b0000_0000_0100_0000,
        Wizard        = 0b0000_0000_1000_0000,
        Illusionist   = 0b0000_0001_0000_0000,
        Assassin      = 0b0000_0010_0000_0000,
        Shapeshifter  = 0b0000_0100_0000_0000,
        Merchant      = 0b0000_1000_0000_0000,
        Bard          = 0b0001_0000_0000_0000,
    }

    public enum Talents {
        Light     = 0b0000_0000_0000_0001,
        Shadow    = 0b0000_0000_0000_0010,
        Elemental = 0b0000_0000_0000_0100,
        Earth     = 0b0000_0000_0000_1000,
        Ice       = 0b0000_0000_0001_0000,
        Lightning = 0b0000_0000_0010_0000,
        Draconic  = 0b0000_0000_0100_0000,
        Royal     = 0b0000_0000_1000_0000,
    }

    public enum Types {
        Hero             = 0b0000_0000_0000_0001,
        Equipment        = 0b0000_0000_0000_0010,
        Weapon           = 0b0000_0000_0000_0100,
        Action           = 0b0000_0000_0000_1000,
        Instant          = 0b0000_0000_0001_0000,
        Attack_Reaction  = 0b0000_0000_0010_0000,
        Defense_Reaction = 0b0000_0000_0100_0000,
        Block            = 0b0000_0000_1000_0000,
        Resource         = 0b0000_0001_0000_0000,
    }

    public enum SubTypes {
        Young      = 0b0000_0000_0000_0001,
        Head       = 0b0000_0000_0000_0010,
        Chest      = 0b0000_0000_0000_0100,
        Arms       = 0b0000_0000_0000_1000,
        Legs       = 0b0000_0000_0001_0000,
        Off_Hand   = 0b0000_0000_0010_0000,
        Attack     = 0b0000_0000_0100_0000,
        Item       = 0b0000_0000_1000_0000,
        Aura       = 0b0000_0001_0000_0000,
        Ally       = 0b0000_0010_0000_0000,
        Figment    = 0b0000_0100_0000_0000,
        Invocation = 0b0000_1000_0000_0000,
        Ash        = 0b0001_0000_0000_0000,
    }

    public enum WeaponTypes {
        Sword   = 0b0000_0000_0000_0001,
        Club    = 0b0000_0000_0000_0010,
        Hammer  = 0b0000_0000_0000_0100,
        Dagger  = 0b0000_0000_0000_1000,
        Bow     = 0b0000_0000_0001_0000,
        Staff   = 0b0000_0000_0010_0000,
        Claw    = 0b0000_0000_0100_0000,
        Gun     = 0b0000_0000_1000_0000,
        Axe     = 0b0000_0001_0000_0000,
        Flail   = 0b0000_0010_0000_0000,
        Scythe  = 0b0000_0100_0000_0000,
        Scepter = 0b0000_1000_0000_0000,
        Orb     = 0b0001_0000_0000_0000,
        Rock    = 0b0001_0000_0000_0000,
        Book    = 0b0001_0000_0000_0000,
    }
}
