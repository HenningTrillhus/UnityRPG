using UnityEngine;
using System;

public class ShelfMouseInteraction : MonoBehaviour
{
    public float hoverDistance = 0.5f;

    public GameObject _Shelf;
    public GameObject _HoverIcon;
    public GameObject _HoverTextBox;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _HoverIcon.SetActive(false);
        _HoverTextBox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;
        float distance = Vector2.Distance(mousePos, _Shelf.transform.position);

        if (distance < hoverDistance)
        {
            _HoverIcon.SetActive(true);
            _HoverTextBox.SetActive(true);

        }
        else
        {
            _HoverIcon.SetActive(false);
            _HoverTextBox.SetActive(false);

        }
    }
}
