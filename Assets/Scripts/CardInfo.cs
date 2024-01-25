using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardInfo : MonoBehaviour {
    [HideInInspector]
    public VirtualTransform vt;
    private Vector3 hiddenPosition;
    private Vector3 publicPosition;
    private Image image;

    // Start is called before the first frame update
    void Start() {
        vt = GetComponent<VirtualTransform>();
        hiddenPosition = transform.position;
        publicPosition = hiddenPosition + new Vector3(500, 0, 0);
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update() {
    }

    public void Close() {
        vt.position = hiddenPosition;
    }

    public void Open(Sprite sprite) {
        image.sprite = sprite;
        vt.position = publicPosition;
    }
}
