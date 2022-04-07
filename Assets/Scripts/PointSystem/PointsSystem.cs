using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PointsSystem : MonoBehaviour
{
    private int points;
    private int wrongPoints;
    private TextMeshProUGUI textMesh;

    private void Start()
    {
        points = 0;
        wrongPoints = 0;
        textMesh = GetComponent<TextMeshProUGUI>();
    }
    private void Update()
    {
        
        textMesh.text = points.ToString("0") + "/10";

    }
    public void MorePoints(int newPoints)
    {
        points += newPoints;
    }

    public void WrongPoints(int newPoints)
    {
        wrongPoints += newPoints;
    }

    public int GetPoints()
    {
        return this.points;
    }

    public int GetWrongPoints()
    {
        return this.wrongPoints;
    }
}
