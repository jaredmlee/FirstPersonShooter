using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QualityAdjuster : MonoBehaviour
{
    private int currQualityLevel;
    public Text qualityText;
    // Start is called before the first frame update
    void Start()
    {
        currQualityLevel = QualitySettings.GetQualityLevel();
    }

    // Update is called once per frame
    void Update()
    {
        if (currQualityLevel == 5)
        {
            qualityText.text = "Extreme";
        }
        else if (currQualityLevel == 4)
        {
            qualityText.text = "Very High";
        }
        else if (currQualityLevel == 3)
        {
            qualityText.text = "High";
        }
        else if (currQualityLevel == 2)
        {
            qualityText.text = "Medium";
        }
        else if (currQualityLevel == 1)
        {
            qualityText.text = "Low";
        }
    }

    public void setQualityLevel(int qualityLevel)
    {
        QualitySettings.SetQualityLevel(qualityLevel);
    }

    public void incrementQualityLevel()
    {
        if (currQualityLevel < 5)
        {
            currQualityLevel++;
            setQualityLevel(currQualityLevel);
        }
    }

    public void decrementQualityLevel()
    {
        Debug.Log("decremete");
        if (currQualityLevel > 1)
        {
            currQualityLevel--;
            setQualityLevel(currQualityLevel);
        }
    }
}
