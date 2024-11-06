using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Automaton : MonoBehaviour
{
    
    public bool Black = false;
    public GameObject white;
    private int SizeX = 100;
    private int SizeY = 100;
    private string binary;
    private bool[] rules;
    private bool[,] map;
    private Dictionary<string, bool> param;
    public List<GameObject> objs;

    void Start()
    {
        objs = new List<GameObject>();
    }

    // Método para configurar e imprimir la regla de Wolfram
    public void SetRule(int type)
    {
        Clean();
        SizeX = 100;
        SizeY = 100;
        this.rules = new bool[8];
        RuleSetUp(type);
        GenerateBoard();
    }

    private void GenerateBoard()
    {
        BoardSetUp();
        FillBoard();
        CreateAllTiles();
    }

    private void FillBoard()
    {
        for (int j = 1; j < SizeY; j++)
        {
            for (int i = 0; i < SizeX; i++)
            {
                CheckUp(i, j);
            }
        }
    }

    private void BoardSetUp()
    {
        for (int i = 0; i < SizeX; i++)
        {
            map[i, 0] = false;
        }
        map[SizeX / 2, 0] = true;  // Célula inicial en el centro
    }

    void RuleSetUp(int type)
    {
        binary = Convert.ToString(type, 2).PadLeft(8, '0');  // Convierte el número en una cadena binaria de 8 bits
        for (int i = 0; i < binary.Length; i++)
        {
            rules[i] = binary[i] == '1';
        }

        param = new Dictionary<string, bool>
        {
            { "111", rules[0] },
            { "110", rules[1] },
            { "101", rules[2] },
            { "100", rules[3] },
            { "011", rules[4] },
            { "010", rules[5] },
            { "001", rules[6] },
            { "000", rules[7] }
        };

        map = new bool[SizeX, SizeY];
    }

    void CheckUp(int x, int y)
    {
        string number = (x > 0 ? map[x - 1, y - 1] : map[SizeX - 1, y - 1]) ? "1" : "0";
        number += map[x, y - 1] ? "1" : "0";
        number += (x < SizeX - 2 ? map[x + 1, y - 1] : map[0, y - 1]) ? "1" : "0";

        map[x, y] = param[number];
    }

    void CreateAllTiles()
    {
        for (int j = 0; j < SizeY; j++)
        {
            for (int i = 0; i < SizeX; i++)
            {
                GameObject g = Instantiate(white, new Vector2(i, -j), Quaternion.identity);
                g.SetActive(true);
                if (!map[i, j])
                {
                    g.GetComponent<SpriteRenderer>().color = Black ? Color.black : Color.Lerp(Color.red, Color.blue, (float)j / SizeY);
                }
                objs.Add(g);
            }
        }
    }

    void Clean()
    {
        foreach (GameObject obj in objs)
        {
            Destroy(obj);
        }
        objs.Clear();
    }
}