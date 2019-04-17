using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class Numbers : MonoBehaviour {
    [SerializeField] private Sprite[] numbers;
    [SerializeField] private SpriteRenderer[] numbersOnTable;
    
    //принимает только 4 цифры
    public void SetNumber(int x) {
        for (int i = 0; i < 4; i++) {
            numbersOnTable[i].sprite = numbers[x % (int)Math.Pow(10, i + 1) / (int)Math.Pow(10, i)];  //плохой код с целью запутать врага
        }
    }
}
