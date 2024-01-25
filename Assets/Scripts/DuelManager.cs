using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuelManager : MonoBehaviour {
    public static string player1Name;
    public static Deck player1Deck;
    public static Sprite player1Image;
    public static Sprite player1Playmat;
    public static Sprite player1Sleeve;

    public static string player2Name;
    public static Deck player2Deck;
    public static Sprite player2Image;
    public static Sprite player2Playmat;
    public static Sprite player2Sleeve;

    [SerializeField]
    private int activePlayers;
    //[SerializeField]
    //private List<Sprite> playmatSprites;
    //[SerializeField]
    //private List<HeroCard> heroes;
    //private List<Dictionary<Constants.Zones, WeaponCard>> weapons;
    //private List<Dictionary<CardTypes.SubTypes, EquipmentCard>> equipments;
    //private CardPile player2DeckPile;
    //private List<CardPile> decks;

    private CardInstance[] heroes;
    private PlaymatInstance[] playmatInstances;
    private CardPile[] deckPiles;
    private CardPile[] headPiles;
    private CardPile[] chestPiles;
    private CardPile[] armsPiles;
    private CardPile[] legsPiles;
    private CardPile[] leftPiles;
    private CardPile[] rightPiles;
    private CardPile[] arsenalPiles;
    private CardPile[] graveyardPiles;
    private CardPile[] banishedPiles;
    private CardPile[] pitchPiles;
    private CardPile[] soulPiles;
    private List<CardInstance>[] hands;

    //private PlaymatInstance player2PlaymatInstance;
    //private CardPile player2HeadPile;
    //private CardPile player2ChestPile;
    //private CardPile player2ArmsPile;
    //private CardPile player2LegsPile;
    //private CardPile player2LeftPile;
    //private CardPile player2RightPile;
    //private CardPile player2ArsenalPile;
    //private CardPile player2GraveyardPile;
    //private CardPile player2BanishedPile;
    //private CardPile player2PitchPile;
    //private CardPile player2SoulPile;
    //private List<CardInstance> player2Hand;

    //private List<PlaymatInstance> playmats;
    //private List<List<CardInstance>> hands;

    void mockPlayer2() {
        player2Name = "Player 2";
        player2Deck = new Deck(
            "name",
            "Kavdaen, Trader of Skins",
            new string[0],
            new string[0],
            new string[] {
                "Raging Onslaught (Red)",
                "Raging Onslaught (Red)",
                "Raging Onslaught (Yellow)",
                "Raging Onslaught (Yellow)",
                "Raging Onslaught (Blue)",
                "Raging Onslaught (Blue)",
                "Wounding Blow (Red)",
                "Wounding Blow (Red)",
                "Wounding Blow (Yellow)",
                "Wounding Blow (Yellow)",
                "Wounding Blow (Blue)",
                "Wounding Blow (Blue)"
            }
            );
        player2Deck.mainDeck = player2Deck.cards.ToArray();
        player2Deck.head = Resources.Load<EquipmentCard>("Card Objects/Equipment/Ironrot Helm");
        player2Deck.chest = Resources.Load<EquipmentCard>("Card Objects/Equipment/Ironrot Plate");
        player2Deck.arms = Resources.Load<EquipmentCard>("Card Objects/Equipment/Ironrot Gauntlet");
        player2Deck.legs = Resources.Load<EquipmentCard>("Card Objects/Equipment/Ironrot Legs");
        player2Deck.rightHand = Resources.Load<WeaponCard>("Card Objects/Weapon/Talishar, the Lost Prince");
        player2Playmat = Resources.Load<Sprite>("Sprites/Playmats/Map of Rathe");
        player2Sleeve = Resources.Load<Sprite>("Sprites/Sleeves/Default");
    }

    void SetupPlayer(int player, Vector3 position, Quaternion rotation) {
        GameObject playmatObj = Instantiate((GameObject)Resources.Load("Prefabs/Playmat"), position, rotation);
        playmatInstances[player-1] = playmatObj.GetComponent<PlaymatInstance>();
        playmatInstances[player-1].Start();
        switch (player) {
            case 1:
                playmatInstances[player - 1].updateArt(player1Playmat);
                break;
            case 2:
                playmatInstances[player - 1].updateArt(player2Playmat);
                break;
            default: break;
        }

        var hero = Instantiate((GameObject)Resources.Load("Prefabs/Card Instance"), playmatInstances[player-1].heroLocation.transform);
        hero.transform.localPosition = Vector3.zero;
        hero.transform.localRotation = Quaternion.Euler(-90, 0, 0);
        heroes[player - 1] = hero.GetComponent<CardInstance>();

        GameObject deckObj = Instantiate((GameObject)Resources.Load("Prefabs/Card Pile"), playmatInstances[player - 1].deckLocation.transform);
        deckObj.transform.localPosition = Vector3.zero;
        deckObj.transform.localRotation = Quaternion.Euler(0, 180, 0);
        deckPiles[player - 1] = deckObj.GetComponent<CardPile>();
        deckPiles[player - 1].zoneName = Constants.Zones.Deck;

        GameObject leftObj = Instantiate((GameObject)Resources.Load("Prefabs/Card Pile"), playmatInstances[player - 1].weaponLLocation.transform);
        leftObj.transform.localPosition = Vector3.zero;
        leftObj.transform.localRotation = Quaternion.Euler(0, 180, 0);
        leftPiles[player - 1] = leftObj.GetComponent<CardPile>();
        leftPiles[player - 1].zoneName = Constants.Zones.WeaponL;

        GameObject rightObj = Instantiate((GameObject)Resources.Load("Prefabs/Card Pile"), playmatInstances[player-1].weaponRLocation.transform);
        rightObj.transform.localPosition = Vector3.zero;
        rightObj.transform.localRotation = Quaternion.Euler(0, 180, 0);
        rightPiles[player - 1] = rightObj.GetComponent<CardPile>();
        rightPiles[player - 1].zoneName = Constants.Zones.WeaponR;

        GameObject headObj = Instantiate((GameObject)Resources.Load("Prefabs/Card Pile"), playmatInstances[player-1].headLocation.transform);
        headObj.transform.localPosition = Vector3.zero;
        headObj.transform.localRotation = Quaternion.Euler(0, 180, 0);
        headPiles[player - 1] = headObj.GetComponent<CardPile>();
        headPiles[player - 1].zoneName = Constants.Zones.Head;

        GameObject chestObj = Instantiate((GameObject)Resources.Load("Prefabs/Card Pile"), playmatInstances[player-1].chestLocation.transform);
        chestObj.transform.localPosition = Vector3.zero;
        chestObj.transform.localRotation = Quaternion.Euler(0, 180, 0);
        chestPiles[player - 1] = chestObj.GetComponent<CardPile>();
        chestPiles[player - 1].zoneName = Constants.Zones.Chest;

        GameObject armsObj = Instantiate((GameObject)Resources.Load("Prefabs/Card Pile"), playmatInstances[player-1].armsLocation.transform);
        armsObj.transform.localPosition = Vector3.zero;
        armsObj.transform.localRotation = Quaternion.Euler(0, 180, 0);
        armsPiles[player - 1] = armsObj.GetComponent<CardPile>();
        armsPiles[player - 1].zoneName = Constants.Zones.Arms;

        GameObject legsObj = Instantiate((GameObject)Resources.Load("Prefabs/Card Pile"), playmatInstances[player-1].legsLocation.transform);
        legsObj.transform.localPosition = Vector3.zero;
        legsObj.transform.localRotation = Quaternion.Euler(0, 180, 0);
        legsPiles[player - 1] = legsObj.GetComponent<CardPile>();
        legsPiles[player - 1].zoneName = Constants.Zones.Legs;

        GameObject graveyardObj = Instantiate((GameObject)Resources.Load("Prefabs/Card Pile"), playmatInstances[player-1].graveyardLocation.transform);
        graveyardObj.transform.localPosition = Vector3.zero;
        graveyardObj.transform.localRotation = Quaternion.Euler(0, 180, 0);
        graveyardPiles[player - 1] = graveyardObj.GetComponent<CardPile>();
        graveyardPiles[player - 1].zoneName = Constants.Zones.Graveyard;

        GameObject banishedObj = Instantiate((GameObject)Resources.Load("Prefabs/Card Pile"), playmatInstances[player-1].banishedLocation.transform);
        banishedObj.transform.localPosition = Vector3.zero;
        banishedObj.transform.localRotation = Quaternion.Euler(0, 180, 0);
        banishedPiles[player - 1] = banishedObj.GetComponent<CardPile>();
        banishedPiles[player - 1].zoneName = Constants.Zones.Banished;

        GameObject pitchObj = Instantiate((GameObject)Resources.Load("Prefabs/Card Pile"), playmatInstances[player-1].pitchLocation.transform);
        pitchObj.transform.localPosition = Vector3.zero;
        pitchObj.transform.localRotation = Quaternion.Euler(0, 180, 0);
        pitchPiles[player - 1] = pitchObj.GetComponent<CardPile>();
        pitchPiles[player - 1].zoneName = Constants.Zones.Pitch;

        GameObject soulObj = Instantiate((GameObject)Resources.Load("Prefabs/Card Pile"), playmatInstances[player-1].soulLocation.transform);
        soulObj.transform.localPosition = Vector3.zero;
        soulObj.transform.localRotation = Quaternion.Euler(0, 180, 0);
        soulPiles[player - 1] = soulObj.GetComponent<CardPile>();
        soulPiles[player - 1].zoneName = Constants.Zones.Soul;

        switch (player) {
            case 1:
                heroes[player - 1].setCardReference(player1Deck.hero);
                foreach (var card in player1Deck.mainDeck) {
                    deckPiles[player - 1].addCardTop(spawnCard(card.name, Constants.Zones.Deck, player, true));
                }
                if (!(player1Deck.leftHand is null))
                    leftPiles[player - 1].addCardTop(spawnCard(player1Deck.leftHand.name, Constants.Zones.WeaponL, player, false));
                if (!(player1Deck.rightHand is null))
                    rightPiles[player - 1].addCardTop(spawnCard(player1Deck.rightHand.name, Constants.Zones.WeaponR, player, false));
                if (!(player1Deck.head is null))
                    headPiles[player - 1].addCardTop(spawnCard(player1Deck.head.name, Constants.Zones.Head, player, false));
                if (!(player1Deck.chest is null))
                    chestPiles[player - 1].addCardTop(spawnCard(player1Deck.chest.name, Constants.Zones.Chest, player, false));
                if (!(player1Deck.arms is null))
                    armsPiles[player - 1].addCardTop(spawnCard(player1Deck.arms.name, Constants.Zones.Arms, player, false));
                if (!(player1Deck.legs is null))
                    legsPiles[player - 1].addCardTop(spawnCard(player1Deck.legs.name, Constants.Zones.Legs, player, false));
                break;
            case 2:
                heroes[player - 1].setCardReference(player2Deck.hero);
                foreach (var card in player2Deck.mainDeck) {
                    deckPiles[player - 1].addCardTop(spawnCard(card.name, Constants.Zones.Deck, player, true));
                }
                if (!(player2Deck.leftHand is null))
                    leftPiles[player - 1].addCardTop(spawnCard(player2Deck.leftHand.name, Constants.Zones.WeaponL, player, false));
                if (!(player2Deck.rightHand is null))
                    rightPiles[player - 1].addCardTop(spawnCard(player2Deck.rightHand.name, Constants.Zones.WeaponR, player, false));
                if (!(player2Deck.head is null))
                    headPiles[player - 1].addCardTop(spawnCard(player2Deck.head.name, Constants.Zones.Head, player, false));
                if (!(player2Deck.chest is null))
                    chestPiles[player - 1].addCardTop(spawnCard(player2Deck.chest.name, Constants.Zones.Chest, player, false));
                if (!(player2Deck.arms is null))
                    armsPiles[player - 1].addCardTop(spawnCard(player2Deck.arms.name, Constants.Zones.Arms, player, false));
                if (!(player2Deck.legs is null))
                    legsPiles[player - 1].addCardTop(spawnCard(player2Deck.legs.name, Constants.Zones.Legs, player, false));
                break;
            default: break;
        }
    }

    // Start is called before the first frame update
    void Start() {

        heroes = new CardInstance[activePlayers];
        playmatInstances = new PlaymatInstance[activePlayers];
        deckPiles = new CardPile[activePlayers];
        headPiles = new CardPile[activePlayers];
        chestPiles = new CardPile[activePlayers];
        armsPiles = new CardPile[activePlayers];
        legsPiles = new CardPile[activePlayers];
        leftPiles = new CardPile[activePlayers];
        rightPiles = new CardPile[activePlayers];
        arsenalPiles = new CardPile[activePlayers];
        graveyardPiles = new CardPile[activePlayers];
        banishedPiles = new CardPile[activePlayers];
        pitchPiles = new CardPile[activePlayers];
        soulPiles = new CardPile[activePlayers];
        hands = new List<CardInstance>[activePlayers];

        mockPlayer2();

        SetupPlayer(1, Vector3.zero, Quaternion.identity);
        SetupPlayer(2, new Vector3(0, 0, -4.04f), Quaternion.Euler(0, 180, 0));

        // Start
        StartCoroutine(beginDuel());
    }

    CardInstance spawnCard(string cardName, Constants.Zones zone, int owner, bool hidden) {
        var cardObj = Instantiate((GameObject)Resources.Load("Prefabs/Card Instance"));
        var cardInstance = cardObj.GetComponent<CardInstance>();
        cardInstance.setCardReference((Card)Resources.Load("Card Objects/Deck/" + cardName));
        if (cardInstance.getCardReference() is null)
            cardInstance.setCardReference((Card)Resources.Load("Card Objects/Equipment/" + cardName));
        if (cardInstance.getCardReference() is null)
            cardInstance.setCardReference((Card)Resources.Load("Card Objects/Weapon/" + cardName));
        if (cardInstance.getCardReference() is null)
            cardInstance.setCardReference((Card)Resources.Load("Card Objects/Token/" + cardName));
        cardInstance.zone = zone;
        cardInstance.playerOwner = owner;
        cardInstance.playerController = owner;
        switch (owner) {
            case 1:
                cardInstance.updateSleeve(player1Sleeve);
                break;
            case 2:
                cardInstance.updateSleeve(player2Sleeve);
                break;
            default: break;
        }
        if (!hidden)
            cardInstance.setHidden(hidden);
        return cardInstance;
    }

    IEnumerator beginDuel() {
        yield return new WaitForEndOfFrame();
        for (var player = 0; player < playmatInstances.Length; player++) {
            deckPiles[player].shuffle();
            yield return new WaitForSeconds(1f);
            while (playmatInstances[player].cardsInHand.Count < player1Deck.hero.intellect) {
                playmatInstances[player].addCardToHand(deckPiles[player].removeCardTop());
                yield return new WaitForSeconds(.2f);
            }
            yield return new WaitForSeconds(.5f);
        }
        yield return null;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.S)) {
            deckPiles[0].shuffle();
        }
        if (Input.GetKeyDown(KeyCode.D)) {
            playmatInstances[0].addCardToHand(deckPiles[0].removeCardTop());
        }
    }
}
