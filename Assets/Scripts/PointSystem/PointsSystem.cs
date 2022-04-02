using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PointsSystem : MonoBehaviour
{
    private int points;
    private TextMeshProUGUI textMesh;

    private void Start()
    {
        points = 1;
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

    public int GetPoints()
    {
        return this.points;
    }
}
