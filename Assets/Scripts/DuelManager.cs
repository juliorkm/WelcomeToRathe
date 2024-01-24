using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuelManager : MonoBehaviour
{
    public static string player1Name;
    public static Deck player1Deck;
    public static Sprite player1Image;
    public static Sprite player1Playmat;
    public static Sprite player1Sleeve;

    [SerializeField]
    private int activePlayers;
    [SerializeField]
    private List<Sprite> playmatSprites;
    [SerializeField]
    private List<HeroCard> heroes;
    private List<Dictionary<Constants.Zones, WeaponCard>> weapons;
    private List<Dictionary<CardTypes.SubTypes, EquipmentCard>> equipments;
    private List<CardPile> decks;

    private List<PlaymatInstance> playmats;
    private List<List<CardInstance>> hands;

    // Start is called before the first frame update
    void Start() {
        playmats = new List<PlaymatInstance>();
        weapons = new List<Dictionary<Constants.Zones, WeaponCard>>();
        equipments = new List<Dictionary<CardTypes.SubTypes, EquipmentCard>>();
        decks = new List<CardPile>();

        // player 1
        GameObject playmatObj = Instantiate((GameObject) Resources.Load("Prefabs/Playmat"), new Vector3(0,0,0), Quaternion.Euler(0,0,0));
        playmats.Add(playmatObj.GetComponent<PlaymatInstance>());
        playmats[0].Start();
        playmats[0].updateArt(playmatSprites[0]);
        var hero1 = Instantiate((GameObject)Resources.Load("Prefabs/Card Instance"), playmats[0].heroLocation.transform);
        hero1.transform.localPosition = Vector3.zero;
        hero1.transform.localRotation = Quaternion.Euler(-90, 0, 0);
        //var hero1 = Instantiate((GameObject)Resources.Load("Prefabs/Card Instance"), playmats[0].heroLocation.position + new Vector3(0, 0.005f, 0), Quaternion.Euler(-90, 0, 0), playmats[0].transform);
        hero1.GetComponent<CardInstance>().setCardReference(heroes[0]);
        GameObject deckObj1 = Instantiate((GameObject) Resources.Load("Prefabs/Deck Pile"), playmats[0].deckLocation.transform);
        deckObj1.transform.localPosition = Vector3.zero;
        deckObj1.transform.localRotation = Quaternion.Euler(0,180,0);
        //GameObject deckObj = Instantiate((GameObject) Resources.Load("Prefabs/Deck Pile"), playmats[0].deckLocation.position, Quaternion.identity);
        decks.Add(deckObj1.GetComponent<CardPile>());
        decks[0].zoneName = Constants.Zones.Deck;
        decks[0].addCardTop(spawnCard("Raging Onslaught (Red)", Constants.Zones.Deck, 1));
        decks[0].addCardTop(spawnCard("Raging Onslaught (Red)", Constants.Zones.Deck, 1));
        decks[0].addCardTop(spawnCard("Raging Onslaught (Yellow)", Constants.Zones.Deck, 1));
        decks[0].addCardTop(spawnCard("Raging Onslaught (Yellow)", Constants.Zones.Deck, 1));
        decks[0].addCardTop(spawnCard("Raging Onslaught (Blue)", Constants.Zones.Deck, 1));
        decks[0].addCardTop(spawnCard("Raging Onslaught (Blue)", Constants.Zones.Deck, 1));
        decks[0].addCardTop(spawnCard("Wounding Blow (Red)", Constants.Zones.Deck, 1));
        decks[0].addCardTop(spawnCard("Wounding Blow (Red)", Constants.Zones.Deck, 1));
        decks[0].addCardTop(spawnCard("Wounding Blow (Yellow)", Constants.Zones.Deck, 1));
        decks[0].addCardTop(spawnCard("Wounding Blow (Yellow)", Constants.Zones.Deck, 1));
        decks[0].addCardTop(spawnCard("Wounding Blow (Blue)", Constants.Zones.Deck, 1));
        decks[0].addCardTop(spawnCard("Wounding Blow (Blue)", Constants.Zones.Deck, 1));

        // player 2
        GameObject playmatObj2 = Instantiate((GameObject)Resources.Load("Prefabs/Playmat"), new Vector3(0, 0, -4.04f), Quaternion.Euler(0, 180, 0));
        playmats.Add(playmatObj2.GetComponent<PlaymatInstance>());
        playmats[1].Start();
        playmats[1].updateArt(playmatSprites[1]);
        var hero2 = Instantiate((GameObject)Resources.Load("Prefabs/Card Instance"), playmats[1].heroLocation.transform);
        hero2.transform.localPosition = Vector3.zero;
        hero2.transform.localRotation = Quaternion.Euler(-90, 0, 0);
        //var hero2 = Instantiate((GameObject)Resources.Load("Prefabs/Card Instance"), playmats[1].heroLocation.position + new Vector3(0, 0.005f, 0), Quaternion.Euler(-90, 0, 0), playmats[1].transform);
        hero2.GetComponent<CardInstance>().setCardReference(heroes[1]);
        GameObject deckObj2 = Instantiate((GameObject)Resources.Load("Prefabs/Deck Pile"), playmats[1].deckLocation.transform);
        deckObj2.transform.localPosition = Vector3.zero;
        deckObj2.transform.localRotation = Quaternion.Euler(0, 180, 0);
        //GameObject deckObj2 = Instantiate((GameObject)Resources.Load("Prefabs/Deck Pile"), playmats[1].deckLocation.position, Quaternion.identity);
        decks.Add(deckObj2.GetComponent<CardPile>());
        decks[1].zoneName = Constants.Zones.Deck;
        decks[1].addCardTop(spawnCard("Raging Onslaught (Red)", Constants.Zones.Deck, 2));
        decks[1].addCardTop(spawnCard("Raging Onslaught (Red)", Constants.Zones.Deck, 2));
        decks[1].addCardTop(spawnCard("Raging Onslaught (Yellow)", Constants.Zones.Deck, 2));
        decks[1].addCardTop(spawnCard("Raging Onslaught (Yellow)", Constants.Zones.Deck, 2));
        decks[1].addCardTop(spawnCard("Raging Onslaught (Blue)", Constants.Zones.Deck, 2));
        decks[1].addCardTop(spawnCard("Raging Onslaught (Blue)", Constants.Zones.Deck, 2));
        decks[1].addCardTop(spawnCard("Wounding Blow (Red)", Constants.Zones.Deck, 2));
        decks[1].addCardTop(spawnCard("Wounding Blow (Red)", Constants.Zones.Deck, 2));
        decks[1].addCardTop(spawnCard("Wounding Blow (Yellow)", Constants.Zones.Deck, 2));
        decks[1].addCardTop(spawnCard("Wounding Blow (Yellow)", Constants.Zones.Deck, 2));
        decks[1].addCardTop(spawnCard("Wounding Blow (Blue)", Constants.Zones.Deck, 2));
        decks[1].addCardTop(spawnCard("Wounding Blow (Blue)", Constants.Zones.Deck, 2));

        // Start
        StartCoroutine(beginDuel());
    }

    CardInstance spawnCard(string cardName, Constants.Zones zone, int owner) {
        var cardObj = Instantiate((GameObject)Resources.Load("Prefabs/Card Instance"));
        var cardInstance = cardObj.GetComponent<CardInstance>();
        cardInstance.setCardReference((Card)Resources.Load("Card Objects/Deck/" + cardName));
        cardInstance.zone = zone;
        cardInstance.playerOwner = owner;
        cardInstance.playerController = owner;
        return cardInstance;
    }

    IEnumerator beginDuel() {
        yield return new WaitForEndOfFrame();
        decks[0].shuffle();
        yield return new WaitForSeconds(1f);
        while (playmats[0].cardsInHand.Count < heroes[0].intellect) {
            playmats[0].addCardToHand(decks[0].removeCardTop());
            yield return new WaitForSeconds(.2f);
        }
        yield return new WaitForSeconds(.5f);
        decks[1].shuffle();
        yield return new WaitForSeconds(1f);
        while (playmats[1].cardsInHand.Count < heroes[1].intellect) {
            playmats[1].addCardToHand(decks[1].removeCardTop());
            yield return new WaitForSeconds(.2f);
        }
        yield return null;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.S)) {
            decks[0].shuffle();
        }
        if (Input.GetKeyDown(KeyCode.D)) {
            playmats[0].addCardToHand(decks[0].removeCardTop());
        }
    }
}
