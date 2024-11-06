using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public Button start;
    public TMP_InputField rule;

    void Start()
    {
        start.onClick.AddListener(GenerateTile);
    }

    void GenerateTile()
    {
        Automaton automaton = GetComponent<Automaton>();

        // Llama al método SetRule con la regla especificada
        automaton.SetRule(int.Parse(rule.text));
    }
}