using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Fuel : MonoBehaviour
{
    public TextMeshProUGUI textFuel;
    public Button addFuelButton;

    public UnityEngine.GameObject cauldron;

    public AudioClip addFuelSound;

    public int fuelCount = 0;

    private void Start()
    {
        if (fuelCount > 0)
        {
            addFuelButton.interactable = true;
            textFuel.text = fuelCount.ToString();
        }
    }

    public void AddFuel()
    {
        addFuelButton.interactable = true;
        fuelCount++;
        textFuel.text = fuelCount.ToString();

    }

    public void RemoveFuel()
    {
        GetComponent<AudioSource>().clip = addFuelSound;
        GetComponent<AudioSource>().Play();

        fuelCount--;
        textFuel.text = fuelCount.ToString();
        if (fuelCount==0)
            addFuelButton.interactable = false;
        //cauldron.GetComponent<MixingSystem>().fuelSpeedUp();
    }

    public int GetFuelCount()
    {
        return fuelCount;
    }
}