using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    public void ResetBoard()
    {
        BoardManager.Instance.BoardSize = new Vector2Int(mColumnSize, mRowSize); //Reseting the baord size
        BoardManager.Instance.ResetBoard();
    }
    public void FindSameColor()
    {
        BoardManager.Instance.SameColoredTilesList();
    }
    public void ColumnSize(int value)
    {
        mColumnSize += value;
        if (mColumnSize < 1)
        {
            mColumnSize = 1;
        }
        m_ColumnText.text = mColumnSize.ToString();
    }
    public void RowSize(int value)
    {
        mRowSize += value;
        if (mRowSize < 1)
        {
            mRowSize = 1;
        }
        m_RowText.text = mRowSize.ToString();
    }
    private int mColumnSize = 5;
    private int mRowSize = 5;
    [SerializeField] Text m_ColumnText;
    [SerializeField] Text m_RowText;
}
