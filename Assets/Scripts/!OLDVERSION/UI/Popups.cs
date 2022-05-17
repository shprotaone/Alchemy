using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Popups : MonoBehaviour     //динамический help? 
{
    public GameObject popupShop;
    public GameObject popupGuilds;
    public GameObject popupRecipes;
    public GameObject popupPause;

    public GameObject buttonShop;
    public GameObject buttonGuilds;
    public GameObject buttonRecipes;
    public GameObject buttonPause;
    public GameObject helpTutorial2;

    public AudioClip openRecipes;
    public AudioClip openGuilds;
    public AudioClip openShop;

    public bool popupOpen = false;
    public int popupOpenID = 0;

    private void Update()
    {
        if (popupOpen)
        {
            switch (popupOpenID)
            {
                case 1:
                    EventSystem.current.SetSelectedGameObject(buttonShop);
                    break;
                case 2:
                    EventSystem.current.SetSelectedGameObject(buttonGuilds);
                    break;
                case 3:
                    EventSystem.current.SetSelectedGameObject(buttonRecipes);
                    break;
                case 4:
                    EventSystem.current.SetSelectedGameObject(buttonPause);
                    break;
                default:
                    EventSystem.current.SetSelectedGameObject(null);
                    break;
            }
        }
        else
            EventSystem.current.SetSelectedGameObject(null);
    }

    public void PopupOpen(int id)
    {
        if (!GetComponent<Tutorial>().canOpenShop) return;

        switch (id)
        {
            case 1:
                helpTutorial2.SetActive(false);
                popupOpenID = 1;
                if (popupShop.activeInHierarchy)
                {
                    PopupClose();
                    popupOpen = false;
                    return;
                }

                GetComponent<AudioSource>().clip = openShop;
                GetComponent<AudioSource>().Play();

                Time.timeScale = 1;
                popupOpen = true;
                popupShop.gameObject.SetActive(true);
                popupPause.gameObject.SetActive(false);
                popupGuilds.gameObject.SetActive(false);
                popupRecipes.gameObject.SetActive(false);
                break;

            case 2:
                if (!GetComponent<Tutorial>().canOpenPopups) return;
                popupOpenID = 2;
                if (popupGuilds.activeInHierarchy)
                {
                    PopupClose();
                    popupOpen = false;
                    return;
                }

                GetComponent<AudioSource>().clip = openGuilds;
                GetComponent<AudioSource>().Play();

                Time.timeScale = 1;
                popupOpen = true;
                popupGuilds.gameObject.SetActive(true);
                popupPause.gameObject.SetActive(false);
                popupShop.gameObject.SetActive(false);
                popupRecipes.gameObject.SetActive(false);
                break;

            case 3:
                if (!GetComponent<Tutorial>().canOpenPopups) return;
                popupOpenID = 3;
                if (popupRecipes.activeInHierarchy)
                {
                    Time.timeScale = 1;
                    PopupClose();
                    popupOpen = false;
                    return;
                }

                GetComponent<AudioSource>().clip = openRecipes;
                GetComponent<AudioSource>().Play();

                if (!GetComponent<Tutorial>().readingHelp)
                    Time.timeScale = 0;
                popupOpen = true;
                popupRecipes.gameObject.SetActive(true);
                popupPause.gameObject.SetActive(false);
                popupShop.gameObject.SetActive(false);
                popupGuilds.gameObject.SetActive(false);
                break;

            case 4:
                if (!GetComponent<Tutorial>().canOpenPopups) return;
                popupOpenID = 4;
                if (popupPause.activeInHierarchy)
                {
                    Time.timeScale = 1;
                    PopupClose();
                    popupOpen = false;
                    return;
                }
                Time.timeScale = 0;
                popupOpen = true;
                popupPause.gameObject.SetActive(true);
                popupRecipes.gameObject.SetActive(false);
                popupShop.gameObject.SetActive(false);
                popupGuilds.gameObject.SetActive(false);
                break;

            default:
                break;
        }
    }

    public void PopupClose()
    {
        if (!GetComponent<Tutorial>().mainTutorial && !GetComponent<Tutorial>().mainGame) return;
        popupOpenID = 0;
        popupOpen = false;
        Time.timeScale = 1;
        popupShop.gameObject.SetActive(false);
        popupGuilds.gameObject.SetActive(false);
        popupRecipes.gameObject.SetActive(false);
        popupPause.gameObject.SetActive(false);
    }
}