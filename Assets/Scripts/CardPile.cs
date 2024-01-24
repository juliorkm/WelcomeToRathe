using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardPile : MonoBehaviour {
    public int playerOwner;
    [SerializeField]
    private GameObject cardInstancePrefab;
    [SerializeField]
    private List<Card> cards;
    [SerializeField]
    private List<GameObject> cardInstances;
    public Constants.Zones zoneName;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        if (cardInstances is null)
            cardInstances = new List<GameObject>();
        //float height = 0;
        //foreach (var card in cards) {
        //    var newCard = Instantiate(cardInstancePrefab, transform.position + new Vector3(0,0+height,0), Quaternion.Euler(90,0,0), transform);
        //    var newInstance = newCard.GetComponent<CardInstance>();
        //    newInstance.setHidden(true);
        //    newInstance.setCardReference(card);
        //    newInstance.playerOwner = playerOwner;
        //    cardInstances.Add(newCard);
        //    height += 0.005f;
        //}

        audioSource = GetComponent<AudioSource>();
    }

    public void addCardTop(CardInstance card) {
        var cardObj = card.gameObject;
        var cardRef = card.getCardReference();

        if (cardObj.transform.parent == transform) {
            return;
        }

        cardInstances.Insert(cardInstances.Count, cardObj);
        cards.Insert(cards.Count, cardRef);

        if (cardInstances.Count == 1)
            cardObj.transform.position = transform.position;
        else
            cardObj.transform.position = cardInstances[cardInstances.Count - 2].transform.position + new Vector3(0, 0 + 0.005f, 0);
        cardObj.transform.parent = transform;
        cardObj.transform.localRotation = Quaternion.Euler(90 * (card.isHidden ? 1 : -1), 0, 0);
    }

    public void addCardBottom(CardInstance card) {
        var cardObj = card.gameObject;
        var cardRef = card.getCardReference();

        if (cardObj.transform.parent == transform) {
            return;
        }

        cardInstances.Insert(0, cardObj);
        cards.Insert(cards.Count, cardRef);

        cardObj.transform.localRotation = Quaternion.Euler(90 * (card.isHidden ? 1 : -1), 0, 0);
        cardObj.transform.position = transform.position;
        cardObj.transform.parent = transform;

        foreach (GameObject otherCardObj in cardInstances) {
            otherCardObj.transform.position += new Vector3(0, 0 + 0.005f, 0);
        }
    }

    public Transform removeCardTop() {
        audioSource.PlayOneShot((AudioClip)Resources.Load("sFX/draw"));
        var card = cardInstances[cardInstances.Count - 1];
        card.transform.parent = null;
        cards.RemoveAt(cards.Count - 1);
        cardInstances.RemoveAt(cardInstances.Count - 1);
        return card.transform;
    }

    public void shuffle() {
        audioSource.PlayOneShot((AudioClip) Resources.Load("sFX/shuffle"));
        var count = cards.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i) {
            var r = Random.Range(i, count);
            var tmp1 = cards[i];
            cards[i] = cards[r];
            cards[r] = tmp1;
            var tmp2 = cardInstances[i];
            cardInstances[i] = cardInstances[r];
            cardInstances[r] = tmp2;
        }
        float height = 0;
        foreach (var card in cardInstances) {
            card.transform.position = transform.position + new Vector3(0, 0 + height, 0);
            card.transform.localRotation = Quaternion.Euler(90 * (card.GetComponent<CardInstance>().isHidden ? 1 : -1), 0, 0);
            height += 0.005f;
        }
        StartCoroutine(shuffleAnimation());
    }

    private IEnumerator shuffleAnimation(float seconds=.6f) {
        float step = .1f;
        float distance = .3f;

        // Calculate the midpoint to split the list into two halves
        List<VirtualTransform> shuffledList = cardInstances.Select(x => x.GetComponent<VirtualTransform>()).ToList();
        int midpoint = shuffledList.Count / 2;

        // Create two halves using the shuffled list
        List<VirtualTransform> firstHalf = shuffledList.GetRange(0, midpoint);
        List<VirtualTransform> secondHalf = shuffledList.GetRange(midpoint, shuffledList.Count - midpoint);

        for (float i=0; i<seconds/step; i++) {
            if (i % 2 == 0) {
                foreach (var t in firstHalf) {
                    t.localPosition = new Vector3(distance, t.transform.localPosition.y, t.transform.localPosition.z);
                }
                foreach (var t in secondHalf) {
                    t.localPosition = new Vector3(-distance, t.transform.localPosition.y, t.transform.localPosition.z);
                }
            } else {
                foreach (var t in firstHalf) {
                    t.localPosition = new Vector3(-distance, t.transform.localPosition.y, t.transform.localPosition.z);
                }
                foreach (var t in secondHalf) {
                    t.localPosition = new Vector3(distance, t.transform.localPosition.y, t.transform.localPosition.z);
                }
            }
            yield return new WaitForSeconds(step);
        }
        foreach (var t in shuffledList) {
            t.localPosition = new Vector3(0, t.transform.localPosition.y, t.transform.localPosition.z);
        }
        yield return null;
    }

    // Update is called once per frame
    void Update() {
    }
}
