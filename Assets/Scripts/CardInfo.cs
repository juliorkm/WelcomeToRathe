using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardInfo : MonoBehaviour {
    [HideInInspector]
    public VirtualTransform vt;
    private RectTransform rt;
    private Vector3 hiddenPosition;
    private Vector3 publicPosition;
    private Image image;
    private bool isOpen = false;

    // Start is called before the first frame update
    void Start() {
        vt = GetComponent<VirtualTransform>();
        rt = GetComponent<RectTransform>();
        hiddenPosition = new Vector3(-300, 0, 0);
        publicPosition = new Vector3(500, 0, 0);
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update() {
    }

    public void Close() {
        var currentPosition = rt.anchoredPosition;
        rt.anchoredPosition = hiddenPosition;
        vt.position = transform.position;
        rt.anchoredPosition = currentPosition;
        isOpen = false;
    }

    public void Open(Sprite sprite) {
        image.sprite = sprite;
        var currentPosition = rt.anchoredPosition;
        rt.anchoredPosition = publicPosition;
        vt.position = transform.position;
        rt.anchoredPosition = currentPosition;
        isOpen = true;
    }
}
