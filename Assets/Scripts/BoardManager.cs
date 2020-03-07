using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class BoardManager : MonoBehaviour
{
    public static BoardManager Instance { get; private set; }
    public Vector2Int BoardSize
    {
        get
        {
            return m_BoardSize;
        }
        set
        {
            m_BoardSize = value;
        }
    }
    //To get the same colored tiles
    [ContextMenu("Show same tiles")]
    public void SameColoredTilesList()
    {
        List<GameObject> sameColoredTiles = new List<GameObject>();
        ResetHighlightedTiles();
        sameColoredTiles.AddRange(VerticalCheck()); //Checking vertical matched elements
        sameColoredTiles.AddRange(HorizontalCheck());//Checking horizontal matched elements
        for (int i = 0; i < sameColoredTiles.Count; i++)
        {
            sameColoredTiles[i].GetComponent<Tile>().HighlightTile(true); //Highligthing the matched elements
        }
    }
    //To reset the board
    public void ResetBoard()
    {
        foreach (Transform item in m_TilesContainer.transform)
        {
            Destroy(item.gameObject);
        }
        InitializeBoard();
    }
    //To reset the highlights
    private void ResetHighlightedTiles()
    {
        foreach (Transform item in m_TilesContainer.transform)
        {
            item.gameObject.GetComponent<Tile>().HighlightTile(false);
        }
    }
    private void Start()
    {
        Instance = this;
        InitializeBoard();
    }
    
    //To initialize the board
    private void InitializeBoard()
    {
        m_GridLayoutGroup.constraintCount = BoardSize.x;
        Rect boardArea = GetComponent<RectTransform>().rect;
        Vector2 cellSize = new Vector2(boardArea.width / BoardSize.x, boardArea.height / BoardSize.y);
        m_GridLayoutGroup.cellSize = cellSize;
        mTilesArray = new GameObject[BoardSize.x, BoardSize.y];
        for (int y = 0; y < BoardSize.y; y++)
        {
            for (int x = 0; x < BoardSize.x; x++)
            {
                int randomNumber = Random.Range(0, m_Tile.Length);
                mTilesArray[x, y] = Instantiate(m_Tile[randomNumber], m_TilesContainer);
                //mTilesArray[x, y].GetComponentInChildren<Text>().text = $"{x},{y}"; //To display tiles position in a 2d array
            } 
        }
    }
    //To check the vertical matching elemetns
    private List<GameObject> VerticalCheck()
    {
        List<GameObject> sameColoredTiles = new List<GameObject>();
        List<GameObject> contiguousRowElements = new List<GameObject>();

        for (int x = 0; x < BoardSize.x; x++)
        {
            GameObject previousTile = null;
            GameObject currentTile = null;

            for (int y = 0; y < BoardSize.y; y++)
            {
                currentTile = mTilesArray[x, y];
                if (previousTile != null && currentTile.GetComponent<Tile>().ColorOfTile == previousTile.GetComponent<Tile>().ColorOfTile)
                {
                    contiguousRowElements.Add(currentTile);
                    if (contiguousRowElements.Count > 2)
                    {
                        sameColoredTiles.AddRange(contiguousRowElements);
                    }
                }
                else
                {
                    contiguousRowElements = new List<GameObject>();
                    contiguousRowElements.Add(currentTile);
                }
                previousTile = currentTile;
            }
        }
        return sameColoredTiles;
    }
    //To check the horizontal matching elements
    private List<GameObject> HorizontalCheck()
    {
        List<GameObject> sameColoredTiles = new List<GameObject>();
        List<GameObject> contiguousColumnElements = new List<GameObject>();

        for (int y = 0; y < BoardSize.y; y++)
        {
            GameObject previousTile = null;
            GameObject currentTile = null;

            for (int x = 0; x < BoardSize.x; x++)
            {
                currentTile = mTilesArray[x, y];

                if (previousTile != null && currentTile.GetComponent<Tile>().ColorOfTile == previousTile.GetComponent<Tile>().ColorOfTile)
                {
                    contiguousColumnElements.Add(currentTile);
                    if (contiguousColumnElements.Count > 2)
                    {
                        sameColoredTiles.AddRange(contiguousColumnElements);
                    }
                }
                else
                {
                    contiguousColumnElements = new List<GameObject>();
                    contiguousColumnElements.Add(currentTile);
                }
                previousTile = currentTile;
            }
        }
        return sameColoredTiles;

    }

    private GameObject[,] mTilesArray;
    [SerializeField] private Text m_Columns;
    [SerializeField] private Text m_Rows;
    [SerializeField] private GridLayoutGroup m_GridLayoutGroup;
    [SerializeField] private GameObject[] m_Tile;
    [SerializeField] private Vector2Int m_BoardSize;
    [SerializeField] private Transform m_TilesContainer;
}
