using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck {
    public string name;

    public HeroCard hero;
    public List<EquipmentCard> equipment;
    public List<WeaponCard> weapons;
    public List<DeckCard> cards;

    public EquipmentCard head;
    public EquipmentCard chest;
    public EquipmentCard arms;
    public EquipmentCard legs;

    public Card leftHand;
    public Card rightHand;

    public DeckCard[] mainDeck;
    public Card[] sideboard;

    public Deck(string name, string hero, string[] equipment, string[] weapons, string[] deck) {
        this.name = name;
        this.hero = Resources.Load<HeroCard>("Card Objects/Hero/Adult/" + hero);
        if (this.hero is null)
            this.hero = Resources.Load<HeroCard>("Card Objects/Hero/Young/" + hero);
        this.equipment = new List<EquipmentCard>();
        foreach (var equip in equipment) {
            this.equipment.Add(Resources.Load<EquipmentCard>("Card Objects/Equipment/" + equip));
        }
        this.weapons = new List<WeaponCard>();
        foreach (var weap in weapons) {
            this.weapons.Add(Resources.Load<WeaponCard>("Card Objects/Weapon/" + weap));
        }
        cards = new List<DeckCard>();
        foreach (var card in deck) {
            this.cards.Add(Resources.Load<DeckCard>("Card Objects/Deck/" + card));
        }
    }

    [System.Serializable]
    class DeckJSON {
        public string hero;
        public string[] weapons;
        public string[] equipment;
        public string[] deck;
    }

    public bool validateDeck(string format) {
        return true;
    }

    public static Deck fromJSON(string name, string json) {
        try {
            var deckJson = JsonUtility.FromJson<DeckJSON>(json);
            var newDeck = new Deck(
                name: name,
                hero: deckJson.hero,
                equipment: deckJson.equipment,
                weapons: deckJson.weapons,
                deck: deckJson.deck
            );
            if (newDeck.hero is null)
                return null;
            return newDeck;
        } catch (System.Exception e) {
            Debug.Log(e);
            return null;
        }
    }
}
