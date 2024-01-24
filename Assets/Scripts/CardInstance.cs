using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardInstance : MonoBehaviour
{
    [SerializeField]
    private Card cardReference;
    public bool isHidden = true;
    public int playerOwner;
    public int playerController;
    public Constants.Zones zone;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(updateFrontImage());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public Card getCardReference() {
        return cardReference;
    }

    public void setCardReference(Card card) {
        cardReference = card;
    }

    public void setHidden(bool hidden) {
        isHidden = hidden;
    }

    private IEnumerator updateFrontImage() {
        for (int i = 0; i < transform.childCount; i++) {
            if (transform.GetChild(i).gameObject.tag == "CardFront") {
                var front = transform.GetChild(i).gameObject;
                var m_renderer = front.GetComponent<MeshRenderer>();
                var m_mesh = front.GetComponent<MeshFilter>().mesh;
                var m_material = m_renderer.material;
                //var m_sprite = cardReference.getSprite();
                List<Sprite> spriteResponse = new List<Sprite>();
                yield return ImageCache.getSprite(spriteResponse, cardReference.cardCode);
                if (spriteResponse.Count == 0) {
                    yield break;
                }
                var m_sprite = spriteResponse[0];

                Rect spriteRect = m_sprite.textureRect;

                m_mesh.uv[0] = new Vector2(spriteRect.x + 0.0f, spriteRect.y + spriteRect.height);
                m_mesh.uv[1] = new Vector2(spriteRect.x + spriteRect.width, spriteRect.y + spriteRect.height);
                m_mesh.uv[2] = new Vector2(spriteRect.x + 0.0f, spriteRect.y + 0.0f);
                m_mesh.uv[3] = new Vector2(spriteRect.x + spriteRect.width, spriteRect.y + 0.0f);

                m_mesh.uv[0] = new Vector2(0.0f, 1.0f);
                m_mesh.uv[1] = new Vector2(1.0f, 1.0f);
                m_mesh.uv[2] = new Vector2(0.0f, 0.0f);
                m_mesh.uv[3] = new Vector2(1.0f, 0.0f);

                m_material.SetTexture("_CardFront", m_sprite.texture);

            }

        }
        yield return null;
    }
}
