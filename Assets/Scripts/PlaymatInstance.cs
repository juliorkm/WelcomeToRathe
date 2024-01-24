using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaymatInstance : MonoBehaviour
{
    [HideInInspector]
    public Transform heroLocation;
    [HideInInspector]
    public Transform weaponLLocation;
    [HideInInspector]
    public Transform weaponRLocation;
    [HideInInspector]
    public Transform arsenalLocation;
    [HideInInspector]
    public Transform deckLocation;
    [HideInInspector]
    public Transform pitchLocation;
    [HideInInspector]
    public Transform graveyardLocation;
    [HideInInspector]
    public Transform banishedLocation;
    [HideInInspector]
    public Transform headLocation;
    [HideInInspector]
    public Transform chestLocation;
    [HideInInspector]
    public Transform armsLocation;
    [HideInInspector]
    public Transform legsLocation;
    [HideInInspector]
    public Transform soulLocation;
    [HideInInspector]
    public Transform permanentsLocation;
    [HideInInspector]
    public Transform handLocation;
    [HideInInspector]
    public List<CardInstance> cardsInHand;

    // Start is called before the first frame update
    public void Start()
    {
        var locations = transform.Find("Locations");
        heroLocation = locations.Find("Hero");
        weaponLLocation = locations.Find("Weapon L");
        weaponRLocation = locations.Find("Weapon R");
        arsenalLocation = locations.Find("Arsenal");
        deckLocation = locations.Find("Deck");
        pitchLocation = locations.Find("Pitch");
        graveyardLocation = locations.Find("Graveyard");
        banishedLocation = locations.Find("Banished");
        headLocation = locations.Find("Head");
        chestLocation = locations.Find("Chest");
        armsLocation = locations.Find("Arms");
        legsLocation = locations.Find("Legs");
        soulLocation = locations.Find("Soul");
        permanentsLocation = locations.Find("Permanents");
        handLocation = locations.Find("Hand");
    }

    public void addCardToHand(Transform newCard) {

        // Save previous positions
        var originalPositions = new List<Vector3>();
        var originalRotations = new List<Quaternion>();
        for (int i = 0; i < handLocation.childCount; i++) {
            var virtualTransform = handLocation.GetChild(i).GetComponent<VirtualTransform>();
            originalPositions.Add(handLocation.GetChild(i).transform.position);
            handLocation.GetChild(i).transform.position = virtualTransform.getPosition();
            originalRotations.Add(handLocation.GetChild(i).transform.rotation);
            handLocation.GetChild(i).transform.rotation = virtualTransform.getRotation();
        }
        var originalPositionNewCard = newCard.transform.position;
        var originalRotationNewCard = newCard.transform.rotation;

        // Move the card
        if (handLocation.childCount == 0) {
            newCard.transform.position = handLocation.position;
        } else {
            newCard.transform.position = handLocation.GetChild(handLocation.childCount - 1).position + new Vector3(-1, 0, 0);
        }

        newCard.transform.parent = handLocation;
        newCard.transform.localRotation = Quaternion.Euler(-50, 0, 0);

        Bounds bounds = new Bounds();

        for (int i = 0; i < handLocation.childCount; i++)
            bounds.Encapsulate(handLocation.GetChild(i).transform.localPosition);
        for (int i = 0; i < handLocation.childCount; i++)
            handLocation.GetChild(i).Translate(-bounds.center);

        // Apply virtual positions
        for (int i = 0; i < handLocation.childCount - 1; i++) {
            var virtualTransform = handLocation.GetChild(i).GetComponent<VirtualTransform>();
            virtualTransform.position = handLocation.GetChild(i).transform.position;
            virtualTransform.rotation = handLocation.GetChild(i).transform.rotation;
        }
        newCard.GetComponent<VirtualTransform>().position = newCard.transform.position;
        newCard.GetComponent<VirtualTransform>().rotation = newCard.transform.rotation;

        // Apply original positions
        for (int i = 0; i < handLocation.childCount - 1; i++) {
            handLocation.GetChild(i).transform.position = originalPositions[i];
            handLocation.GetChild(i).transform.rotation = originalRotations[i];
        }
        newCard.transform.position = originalPositionNewCard;
        newCard.transform.rotation = originalRotationNewCard;

        var cardInstance = newCard.GetComponent<CardInstance>();
        cardInstance.zone = Constants.Zones.Hand;
        cardsInHand.Add(cardInstance);
    }

    public void updateArt(Sprite sprite) {
        for (int i = 0; i < transform.childCount; i++) {
            if (transform.GetChild(i).gameObject.tag == "CardFront") {
                var front = transform.GetChild(i).gameObject;
                var m_renderer = front.GetComponent<MeshRenderer>();
                var m_mesh = front.GetComponent<MeshFilter>().mesh;
                var m_material = m_renderer.material;

                Rect spriteRect = sprite.textureRect;

                m_mesh.uv[0] = new Vector2(spriteRect.x + 0.0f, spriteRect.y + spriteRect.height);
                m_mesh.uv[1] = new Vector2(spriteRect.x + spriteRect.width, spriteRect.y + spriteRect.height);
                m_mesh.uv[2] = new Vector2(spriteRect.x + 0.0f, spriteRect.y + 0.0f);
                m_mesh.uv[3] = new Vector2(spriteRect.x + spriteRect.width, spriteRect.y + 0.0f);

                m_mesh.uv[0] = new Vector2(0.0f, 1.0f);
                m_mesh.uv[1] = new Vector2(1.0f, 1.0f);
                m_mesh.uv[2] = new Vector2(0.0f, 0.0f);
                m_mesh.uv[3] = new Vector2(1.0f, 0.0f);

                m_material.mainTexture = sprite.texture;

            }

        }
    }

    // Update is called once per frame
    void Update() {
    }
}
