using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour, IPointerClickHandler
{
    public enum TileColor
    {
        Red, Green, Blue, Yellow
    }
    public TileColor ColorOfTile => m_TileColor;
    //To highlight same tiles
    public void HighlightTile(bool value)
    {
        m_HighlightImage.SetActive(value);
    }
    //To change the tile color in runtime
    public void OnPointerClick(PointerEventData eventData)
    {
        int currentColorIndex = (int)m_TileColor;
        currentColorIndex = currentColorIndex < 3 ? currentColorIndex + 1 : 0;
        m_TileColor = (TileColor)currentColorIndex;
        AssigneTileColor();
    }

    private void Start()
    {
        AssigneTileColor();
    }
    //To assign color to the tiles
    private void AssigneTileColor()
    {
        switch (m_TileColor)
        {
            case TileColor.Red:
                m_TileSprite.color = Color.red;
                m_TileSprite.color = new Color(1, 0, 0, 0.85f);
                break;
            case TileColor.Green:
                m_TileSprite.color = Color.green;
                m_TileSprite.color = new Color(0, 1, 0, 0.85f);
                break;
            case TileColor.Blue:
                m_TileSprite.color = Color.blue;
                m_TileSprite.color = new Color(0, 0, 1, 0.85f);
                break;
            case TileColor.Yellow:
                m_TileSprite.color = Color.yellow;
                m_TileSprite.color = new Color(1, 0.92f, 0.16f, 0.85f);
                break;
            default:
                break;
        }
    }

    [SerializeField] private Image m_TileSprite;
    [SerializeField] private TileColor m_TileColor;
    [SerializeField] private GameObject m_HighlightImage;
}
