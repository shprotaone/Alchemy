using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterColor : MonoBehaviour
{
    private Color startColor;
    public Color targetColor;

    public Settings settings;
    private float time = 0.02f;

    private void Start()
    {
        startColor = GetComponent<SpriteRenderer>().color;
        targetColor = startColor;
    }

    public void ChangeColor(List<ResourceType> components, bool isWrong)
    {
        if (isWrong)
            targetColor = startColor;

        else
        {
            switch (components.Count)
            {
                case 1:
                    switch (components[0])
                    {
                        case ResourceType.Red:
                            targetColor = settings.colors[11];
                            break;

                        case ResourceType.Blue:
                            targetColor = settings.colors[12];
                            break;

                        case ResourceType.Yellow:
                            targetColor = settings.colors[13];
                            break;

                        case ResourceType.White:
                            targetColor = settings.colors[14];
                            break;

                        default:
                            break;
                    }
                    break;

                case 2:
                    if (components.Contains(ResourceType.Red) && components.Contains(ResourceType.Blue))
                        targetColor = settings.colors[0];

                    if (components.Contains(ResourceType.Red) && components.Contains(ResourceType.Yellow))
                        targetColor = settings.colors[1];

                    if (components.Contains(ResourceType.Blue) && components.Contains(ResourceType.Yellow))
                        targetColor = settings.colors[2];

                    if (components.Contains(ResourceType.Red) && components.Contains(ResourceType.White))
                        targetColor = settings.colors[3];

                    if (components.Contains(ResourceType.Blue) && components.Contains(ResourceType.White))
                        targetColor = settings.colors[5];

                    if (components.Contains(ResourceType.Yellow) && components.Contains(ResourceType.White))
                        targetColor = settings.colors[4];
                    break;

                case 3:
                    if (components.Contains(ResourceType.Red) && components.Contains(ResourceType.Blue) && components.Contains(ResourceType.Yellow))
                        targetColor = settings.colors[6];

                    if (components.Contains(ResourceType.Red) && components.Contains(ResourceType.Blue) && components.Contains(ResourceType.White))
                        targetColor = settings.colors[9];

                    if (components.Contains(ResourceType.Red) && components.Contains(ResourceType.Yellow) && components.Contains(ResourceType.White))
                        targetColor = settings.colors[8];

                    if (components.Contains(ResourceType.Blue) && components.Contains(ResourceType.Yellow) && components.Contains(ResourceType.White))
                        targetColor = settings.colors[10];
                    break;

                case 4:
                    targetColor = settings.colors[7];
                    break;

                default:
                    break;
            }
            if (name == "WaterBoil") targetColor.a = 0;
        }
    }

    public void ClearWater()
    {
        targetColor = startColor;
    }

    private void Update()
    {
        GetComponent<SpriteRenderer>().color = Color.Lerp(GetComponent<SpriteRenderer>().color, targetColor, time);
    }
}
