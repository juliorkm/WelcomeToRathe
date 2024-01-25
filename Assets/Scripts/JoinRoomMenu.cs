using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class JoinRoomMenu : MonoBehaviour {
    private static Sprite unknown;

    public Image playerImage;
    public TMPro.TextMeshProUGUI playerName;
    public TMPro.TMP_InputField playerNameInput;
    public TMPro.TMP_Dropdown deckDropdown;
    private List<Deck> decks;

    public Image playmatImage;
    public TMPro.TMP_Dropdown playmatDropdown;
    private List<Sprite> playmats;

    public Image sleeveImage;
    public TMPro.TMP_Dropdown sleeveDropdown;
    private List<Sprite> sleeves;

    // Start is called before the first frame update
    void Start() {
        unknown = playerImage.sprite;

        var info = new DirectoryInfo(Application.streamingAssetsPath + "/Decks/");
        var fileInfo = info.GetFiles();
        decks = new List<Deck>();
        foreach (FileInfo file in fileInfo) {
            if (file.Extension.ToUpper() == ".JSON") {
                FileStream fs = file.OpenRead();
                StreamReader sr = new StreamReader(fs);
                var newDeck = Deck.fromJSON(file.Name.Substring(0, file.Name.Length - file.Extension.Length), sr.ReadToEnd());
                if (!(newDeck is null))
                    decks.Add(newDeck);
            }
        }
        decks.OrderBy(x => x.name).ToList();
        deckDropdown.AddOptions(decks.Select(x => x.name).ToList());
        var deckSaved = PlayerPrefs.GetString("deck", "");
        for (int i = 0; i < decks.Count; i++) {
            if (deckDropdown.options[i].text == deckSaved)
                deckDropdown.value = i;
        }
        updatePlayerImage();

        var playerNameSaved = PlayerPrefs.GetString("playerName", "Player");
        playerName.text = playerNameSaved;
        playerNameInput.text = playerNameSaved;

        playmats = Resources.LoadAll<Sprite>("Sprites/Playmats/").OrderBy(x => x.name).ToList();
        playmatDropdown.AddOptions(playmats.Select(x => x.name).ToList());
        var playmatSaved = PlayerPrefs.GetString("playmat", "Map of Rathe");
        for (int i = 0; i < playmats.Count; i++) {
            if (playmatDropdown.options[i].text == playmatSaved)
                playmatDropdown.value = i;
        }
        updatePlaymat();

        sleeves = Resources.LoadAll<Sprite>("Sprites/Sleeves/")
                    .OrderBy(x => x.name == "Default" ? 0 : 1)
                    .ThenBy(x => x.name == "Beta" ? 0 : 1)
                    .ThenBy(x => x.name).ToList();
        sleeveDropdown.AddOptions(sleeves.Select(x => x.name).ToList());
        var sleeveSaved = PlayerPrefs.GetString("sleeve", "Default");
        for (int i = 0; i < sleeves.Count; i++) {
            if (sleeveDropdown.options[i].text == sleeveSaved)
                sleeveDropdown.value = i;
        }
        updateSleeve();
    }

    public static IEnumerator getPlayerSprite(Image pImage, string cardCode) {
        pImage.sprite = unknown;
        List<Sprite> spriteResponse = new List<Sprite>();
        yield return ImageCache.getSprite(spriteResponse, cardCode);
        var texture = spriteResponse[0].texture;
        var rect = new Rect(texture.width * 80f / 450, texture.height * 255 / 628, texture.width * 291 / 450, texture.height * 291 / 628);
        var vector = new Vector2(80f / 450, 82f / 628);
        var croppedSprite = Sprite.Create(texture, rect, Vector2.zero);
        if (spriteResponse.Count == 0) {
            yield break;
        }
        pImage.sprite = croppedSprite;
    }

    public void updatePlayerImage() {
        StartCoroutine(getPlayerSprite(playerImage, decks[deckDropdown.value].hero.cardCode));
        PlayerPrefs.SetString("deck", decks[deckDropdown.value].name);
        PlayerPrefs.Save();
    }

    public void updatePlayerName() {
        playerName.text = playerNameInput.text;
        PlayerPrefs.SetString("playerName", playerName.text);
        PlayerPrefs.Save();
    }

    public void updatePlaymat() {
        playmatImage.sprite = playmats[playmatDropdown.value];
        PlayerPrefs.SetString("playmat", playmats[playmatDropdown.value].name);
        PlayerPrefs.Save();
    }

    public void updateSleeve() {
        sleeveImage.sprite = sleeves[sleeveDropdown.value];
        PlayerPrefs.SetString("sleeve", sleeves[sleeveDropdown.value].name);
        PlayerPrefs.Save();
    }

    public void submit() {
        DuelManager.player1Name = playerNameInput.text;
        DuelManager.player1Deck = decks[deckDropdown.value];
        DuelManager.player1Playmat = playmats[playmatDropdown.value];
        DuelManager.player1Sleeve = sleeves[sleeveDropdown.value];
        SceneManager.LoadScene("Sideboarding");
    }
}
