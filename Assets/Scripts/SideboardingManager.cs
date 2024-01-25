using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SideboardingManager : MonoBehaviour {
    public Image playerImage;
    public TMPro.TextMeshProUGUI playerName;
    public TMPro.TMP_Dropdown headDropdown;
    public Image headImage;
    public TMPro.TMP_Dropdown chestDropdown;
    public Image chestImage;
    public TMPro.TMP_Dropdown armsDropdown;
    public Image armsImage;
    public TMPro.TMP_Dropdown legsDropdown;
    public Image legsImage;
    public TMPro.TMP_Dropdown leftDropdown;
    public Image leftImage;
    public TMPro.TMP_Dropdown rightDropdown;
    public Image rightImage;

    private List<EquipmentCard> heads;
    private List<EquipmentCard> chests;
    private List<EquipmentCard> arms;
    private List<EquipmentCard> legs;

    private List<Card> weapons;

    private Sprite na;
    [SerializeField]
    private Sprite unknown;

    // Start is called before the first frame update
    void Start() {
        na = leftImage.sprite;
        playerName.text = DuelManager.player1Name;
        playerImage.sprite = DuelManager.player1Image;

        heads = DuelManager.player1Deck.equipment.Where(x => x.subType == CardTypes.SubTypes.Head).OrderBy(x => x.name).ToList();
        chests = DuelManager.player1Deck.equipment.Where(x => x.subType == CardTypes.SubTypes.Chest).OrderBy(x => x.name).ToList();
        arms = DuelManager.player1Deck.equipment.Where(x => x.subType == CardTypes.SubTypes.Arms).OrderBy(x => x.name).ToList();
        legs = DuelManager.player1Deck.equipment.Where(x => x.subType == CardTypes.SubTypes.Legs).OrderBy(x => x.name).ToList();

        headDropdown.AddOptions(heads.Select(x => x.name).ToList());
        chestDropdown.AddOptions(chests.Select(x => x.name).ToList());
        armsDropdown.AddOptions(arms.Select(x => x.name).ToList());
        legsDropdown.AddOptions(legs.Select(x => x.name).ToList());

        weapons = DuelManager.player1Deck.weapons.ToList<Card>();
        foreach (var offh in DuelManager.player1Deck.equipment.Where(x => x.subType == CardTypes.SubTypes.Off_Hand).ToList<Card>()) {
            weapons.Add(offh);
        }

        leftDropdown.AddOptions(weapons.Select(x => x.name).ToList());
        rightDropdown.AddOptions(weapons.Select(x => x.name).ToList());
    }

    private Sprite sliceSprite(Sprite sprite) {
        var texture = sprite.texture;
        var rect = new Rect(texture.width * 80f / 450, texture.height * 255 / 628, texture.width * 291 / 450, texture.height * 291 / 628);
        var vector = new Vector2(80f / 450, 82f / 628);
        return Sprite.Create(texture, rect, Vector2.zero);
    }

    private IEnumerator getHeadSprite(List<Sprite> response, string code) {
        headImage.sprite = unknown;
        response = new List<Sprite>();
        yield return ImageCache.getSprite(response, code);
        var croppedSprite = sliceSprite(response[0]);
        if (response.Count == 0) {
            yield break;
        }
        headImage.sprite = croppedSprite;
    }

    public void updateHead() {
        if (headDropdown.value == 0) {
            headImage.sprite = na;
            DuelManager.player1Deck.head = null;
            return;
        }
        DuelManager.player1Deck.head = heads[headDropdown.value - 1];
        List<Sprite> spriteResponse = new List<Sprite>();
        StartCoroutine(getHeadSprite(spriteResponse, heads[headDropdown.value - 1].cardCode));
    }

    private IEnumerator getChestSprite(List<Sprite> response, string code) {
        chestImage.sprite = unknown;
        response = new List<Sprite>();
        yield return ImageCache.getSprite(response, code);
        var croppedSprite = sliceSprite(response[0]);
        if (response.Count == 0) {
            yield break;
        }
        chestImage.sprite = croppedSprite;
    }

    public void updateChest() {
        if (chestDropdown.value == 0) {
            chestImage.sprite = na;
            DuelManager.player1Deck.chest = null;
            return;
        }
        DuelManager.player1Deck.chest = chests[chestDropdown.value - 1];
        List<Sprite> spriteResponse = new List<Sprite>();
        StartCoroutine(getChestSprite(spriteResponse, chests[chestDropdown.value - 1].cardCode));
    }

    private IEnumerator getArmsSprite(List<Sprite> response, string code) {
        armsImage.sprite = unknown;
        response = new List<Sprite>();
        yield return ImageCache.getSprite(response, code);
        var croppedSprite = sliceSprite(response[0]);
        if (response.Count == 0) {
            yield break;
        }
        armsImage.sprite = croppedSprite;
    }

    public void updateArms() {
        if (armsDropdown.value == 0) {
            armsImage.sprite = na;
            DuelManager.player1Deck.arms = null;
            return;
        }
        DuelManager.player1Deck.arms = arms[armsDropdown.value - 1];
        List<Sprite> spriteResponse = new List<Sprite>();
        StartCoroutine(getArmsSprite(spriteResponse, arms[armsDropdown.value - 1].cardCode));
    }

    private IEnumerator getLegsSprite(List<Sprite> response, string code) {
        legsImage.sprite = unknown;
        response = new List<Sprite>();
        yield return ImageCache.getSprite(response, code);
        var croppedSprite = sliceSprite(response[0]);
        if (response.Count == 0) {
            yield break;
        }
        legsImage.sprite = croppedSprite;
    }

    public void updateLegs() {
        if (legsDropdown.value == 0) {
            legsImage.sprite = na;
            DuelManager.player1Deck.legs = null;
            return;
        }
        DuelManager.player1Deck.legs = legs[legsDropdown.value - 1];
        List<Sprite> spriteResponse = new List<Sprite>();
        StartCoroutine(getLegsSprite(spriteResponse, legs[legsDropdown.value - 1].cardCode));
    }

    private IEnumerator getLeftSprite(List<Sprite> response, string code) {
        leftImage.sprite = unknown;
        response = new List<Sprite>();
        yield return ImageCache.getSprite(response, code);
        var croppedSprite = sliceSprite(response[0]);
        if (response.Count == 0) {
            yield break;
        }
        leftImage.sprite = croppedSprite;
    }

    public void updateLeft() {
        if (leftDropdown.value == 0) {
            leftImage.sprite = na;
            DuelManager.player1Deck.leftHand = null;
            return;
        }
        if (rightDropdown.value > 0) // unequips the right hand when:
            if ((leftDropdown.value == rightDropdown.value) // same weapon as the already equipped one in the other hand
                || (weapons[leftDropdown.value - 1] is WeaponCard && ((WeaponCard)weapons[leftDropdown.value - 1]).numberOfHands == 2) // is two-handed
                || ( // two off-hands
                    (weapons[leftDropdown.value - 1] is EquipmentCard && ((EquipmentCard)weapons[leftDropdown.value -1]).subType == CardTypes.SubTypes.Off_Hand)
                    && (weapons[rightDropdown.value - 1] is EquipmentCard && ((EquipmentCard)weapons[rightDropdown.value - 1]).subType == CardTypes.SubTypes.Off_Hand))
                ) {
                rightDropdown.value = 0;
                rightImage.sprite = na;
                DuelManager.player1Deck.rightHand = null;
            }
        DuelManager.player1Deck.leftHand = weapons[leftDropdown.value - 1];
        List<Sprite> spriteResponse = new List<Sprite>();
        StartCoroutine(getLeftSprite(spriteResponse, weapons[leftDropdown.value - 1].cardCode));
    }

    private IEnumerator getRightSprite(List<Sprite> response, string code) {
        rightImage.sprite = unknown;
        response = new List<Sprite>();
        yield return ImageCache.getSprite(response, code);
        var croppedSprite = sliceSprite(response[0]);
        if (response.Count == 0) {
            yield break;
        }
        rightImage.sprite = croppedSprite;
    }

    public void updateRight() {
        if (rightDropdown.value == 0) {
            rightImage.sprite = na;
            DuelManager.player1Deck.rightHand = null;
            return;
        }
        if (leftDropdown.value > 0) // unequips the left hand when:
            if ((leftDropdown.value == rightDropdown.value) // same weapon as the already equipped one in the other hand
                || (weapons[rightDropdown.value - 1] is WeaponCard && ((WeaponCard)weapons[rightDropdown.value - 1]).numberOfHands == 2) // is two-handed
                || ( // two off-hands
                    (weapons[leftDropdown.value - 1] is EquipmentCard && ((EquipmentCard)weapons[leftDropdown.value - 1]).subType == CardTypes.SubTypes.Off_Hand)
                    && (weapons[rightDropdown.value - 1] is EquipmentCard && ((EquipmentCard)weapons[rightDropdown.value - 1]).subType == CardTypes.SubTypes.Off_Hand))
                ) {
                leftDropdown.value = 0;
                leftImage.sprite = na;
                DuelManager.player1Deck.leftHand = null;
            }
        DuelManager.player1Deck.rightHand = weapons[rightDropdown.value - 1];
        List<Sprite> spriteResponse = new List<Sprite>();
        StartCoroutine(getRightSprite(spriteResponse, weapons[rightDropdown.value - 1].cardCode));
    }

    public void submit() {
        DuelManager.player1Deck.mainDeck = DuelManager.player1Deck.cards.ToArray();
        SceneManager.LoadScene("DuelScene");
    }
}
