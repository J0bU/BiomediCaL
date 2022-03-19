using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Text
{
    [TextArea(2, 6)] // min 2 - max 6
    public string[] textArray;
  
}
