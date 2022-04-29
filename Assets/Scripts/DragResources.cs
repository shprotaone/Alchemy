using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragResources : MonoBehaviour
{
    public Camera cam;

    public GameObject resourceRed;
    public GameObject resourceBlue;
    public GameObject resourceYellow;
    public GameObject resourceWhite;
    public GameObject resourceLadan;
    public GameObject resourceEye;
    public GameObject resourceStone;
    public GameObject resourceSand;

    public GameObject spawnerRed;
    public GameObject spawnerBlue;
    public GameObject spawnerYellow;
    public GameObject spawnerWhite;
    public GameObject spawnerLadan;
    public GameObject spawnerEye;
    public GameObject spawnerStone;
    public GameObject spawnerSand;

    public Sprite redTaken;
    public Sprite blueTaken;
    public Sprite yellowTaken;
    public Sprite whiteTaken;
    public Sprite ladanTaken;
    public Sprite eyeTaken;
    public Sprite stoneTaken;
    public Sprite sandTaken;

    public Settings settings;

    public AudioClip takeRed;
    public AudioClip takeBlue;
    public AudioClip takeYellow;
    public AudioClip takeWhite;
    public AudioClip takeLadan;
    public AudioClip takeEye;
    public AudioClip takeStone;
    public AudioClip takeSand;

    public AudioClip stopDragging;

    private Touch touch;
    private Transform toDrag;
    private Rigidbody2D toDragRB;

    private bool isDragging = false;
    public bool canTake = true;

    private void Start()
    {
        canTake = true;
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began && canTake)
            {
                if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                    return;

                isDragging = true;

                Vector3 pos = cam.ScreenToWorldPoint(touch.position);   //позиция начала тача
                RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);    //луч к World UI

                #region Модуль взятия предмета из сумки с звуками и заменами спрайтов
                if (Physics2D.Raycast(pos,Vector2.zero) && hit.collider.gameObject.layer == LayerMask.NameToLayer("Platform"))      //поиск позиции слота
                {
                    switch (hit.transform.tag)                                                                                      //switch для выбранного ресурса
                    {
                        case "ResourceRed":
                            if (GetComponent<ResourceSystem>().GetAmount(Resource.Red) > 0)     //если предмета нет в сумке, то взять нельзя
                            {
                                spawnerRed.GetComponent<SpriteRenderer>().sprite = redTaken;    //замена спрайта на спрайт взятия(выделение при нажатии)
                                GetComponent<AudioSource>().clip = takeRed;                     //звук взятия
                                GetComponent<AudioSource>().Play();                             //??
                                canTake = false;
                                toDrag = Instantiate(resourceRed, pos, transform.rotation).transform;   //Создание префаба с порошком, когда обьект тянется
                                GetComponent<ResourceSystem>().RemoveResource(Resource.Red, 1);         //удаление из сумки
                            }
                            break;

                        case "ResourceBlue":
                            if (GetComponent<ResourceSystem>().GetAmount(Resource.Blue) > 0)
                            {
                                spawnerBlue.GetComponent<SpriteRenderer>().sprite = blueTaken;
                                GetComponent<AudioSource>().clip = takeBlue;
                                GetComponent<AudioSource>().Play();
                                canTake = false;
                                toDrag = Instantiate(resourceBlue, pos, transform.rotation).transform;
                                GetComponent<ResourceSystem>().RemoveResource(Resource.Blue, 1);
                            }
                            break;

                        case "ResourceYellow":
                            if (GetComponent<ResourceSystem>().GetAmount(Resource.Yellow) > 0)
                            {
                                spawnerYellow.GetComponent<SpriteRenderer>().sprite = yellowTaken;
                                GetComponent<AudioSource>().clip = takeYellow;
                                GetComponent<AudioSource>().Play();
                                canTake = false;
                                toDrag = Instantiate(resourceYellow, pos, transform.rotation).transform;
                                GetComponent<ResourceSystem>().RemoveResource(Resource.Yellow, 1);
                            }
                            break;

                        case "ResourceWhite":
                            if (GetComponent<ResourceSystem>().GetAmount(Resource.White) > 0)
                            {
                                spawnerWhite.GetComponent<SpriteRenderer>().sprite = whiteTaken;
                                GetComponent<AudioSource>().clip = takeWhite;
                                GetComponent<AudioSource>().Play();
                                canTake = false;
                                toDrag = Instantiate(resourceWhite, pos, transform.rotation).transform;
                                GetComponent<ResourceSystem>().RemoveResource(Resource.White, 1);
                            }
                            break;

                        case "ResourceLadan":
                            if (GetComponent<ResourceSystem>().GetAmount(Resource.Ladan) > 0)
                            {
                                spawnerLadan.GetComponent<SpriteRenderer>().sprite = ladanTaken;
                                GetComponent<AudioSource>().clip = takeLadan;
                                GetComponent<AudioSource>().Play();
                                canTake = false;
                                toDrag = Instantiate(resourceLadan, pos, transform.rotation).transform;
                                GetComponent<ResourceSystem>().RemoveResource(Resource.Ladan, 1);
                            }
                            break;

                        case "ResourceEye":
                            if (GetComponent<ResourceSystem>().GetAmount(Resource.Eye) > 0)
                            {
                                spawnerEye.GetComponent<SpriteRenderer>().sprite = eyeTaken;
                                GetComponent<AudioSource>().clip = takeEye;
                                GetComponent<AudioSource>().Play();
                                canTake = false;
                                toDrag = Instantiate(resourceEye, pos, transform.rotation).transform;
                                GetComponent<ResourceSystem>().RemoveResource(Resource.Eye, 1);
                            }
                            break;

                        case "ResourceStone":
                            if (GetComponent<ResourceSystem>().GetAmount(Resource.Stone) > 0)
                            {
                                spawnerStone.GetComponent<SpriteRenderer>().sprite = stoneTaken;
                                GetComponent<AudioSource>().clip = takeStone;
                                GetComponent<AudioSource>().Play();
                                canTake = false;
                                toDrag = Instantiate(resourceStone, pos, transform.rotation).transform;
                                GetComponent<ResourceSystem>().RemoveResource(Resource.Stone, 1);
                            }
                            break;

                        case "ResourceSand":
                            if (GetComponent<ResourceSystem>().GetAmount(Resource.Sand) > 0)
                            {
                                spawnerSand.GetComponent<SpriteRenderer>().sprite = sandTaken;
                                GetComponent<AudioSource>().clip = takeSand;
                                GetComponent<AudioSource>().Play();
                                canTake = false;
                                toDrag = Instantiate(resourceSand, pos, transform.rotation).transform;
                                GetComponent<ResourceSystem>().RemoveResource(Resource.Sand, 1);
                            }
                            break;

                        default:
                            break;
                    }

                    if (toDrag!=null)
                        toDragRB = toDrag.GetComponent<Rigidbody2D>();
                }
            }
            #endregion

            if (touch.phase == TouchPhase.Moved && isDragging)      //отключение Rigid объекта порошка
            {
                if (toDrag != null)
                {
                    toDragRB.simulated = false;
                    toDrag.position = cam.ScreenToWorldPoint(touch.position);
                }
            }

            if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)  //включение Rigid или свободного падения порошка
            {
                isDragging = false;
                if (toDrag != null && toDragRB != null)     //зануление состояний оттягиваемого предмета
                {
                    StartCoroutine(Wait());                 //Задержка с проигрышем звука отпускания предмета
                    toDragRB.simulated = true;
                    toDrag = null;
                    toDragRB = null;

                    GameObject[] goArray = FindObjectsOfType<GameObject>();     //??
                    for (int i = 0; i < goArray.Length; i++)
                    {
                        if (goArray[i].layer == LayerMask.NameToLayer("Resource"))
                            toDrag = goArray[i].transform;
                    }
                }
            }
        }
        #region //возвращение в слот инвентаря, если порошок бросили на свободную область
        if (toDrag && !isDragging)      
        {
            switch (toDrag.tag)
            {
                case "ResourceRed":
                    toDrag.position = Vector3.MoveTowards(toDrag.position, spawnerRed.transform.position, settings.resourceSpeed);
                    if (toDrag.position == spawnerRed.transform.position)
                    {
                        Destroy(toDrag.gameObject);
                        GetComponent<ResourceSystem>().AddResource(Resource.Red, 1);
                        canTake = true;
                    }
                    break;

                case "ResourceBlue":
                    toDrag.position = Vector3.MoveTowards(toDrag.position, spawnerBlue.transform.position, settings.resourceSpeed);
                    if (toDrag.position == spawnerBlue.transform.position)
                    {
                        Destroy(toDrag.gameObject);
                        GetComponent<ResourceSystem>().AddResource(Resource.Blue, 1);
                        canTake = true;
                    }
                    break;

                case "ResourceYellow":
                    toDrag.position = Vector3.MoveTowards(toDrag.position, spawnerYellow.transform.position, settings.resourceSpeed);
                    if (toDrag.position == spawnerYellow.transform.position)
                    {
                        Destroy(toDrag.gameObject);
                        GetComponent<ResourceSystem>().AddResource(Resource.Yellow, 1);
                        canTake = true;
                    }
                    break;

                case "ResourceWhite":
                    toDrag.position = Vector3.MoveTowards(toDrag.position, spawnerWhite.transform.position, settings.resourceSpeed);
                    if (toDrag.position == spawnerWhite.transform.position)
                    {
                        Destroy(toDrag.gameObject);
                        GetComponent<ResourceSystem>().AddResource(Resource.White, 1);
                        canTake = true;
                    }
                    break;

                case "ResourceLadan":
                    toDrag.position = Vector3.MoveTowards(toDrag.position, spawnerLadan.transform.position, settings.resourceSpeed);
                    if (toDrag.position == spawnerLadan.transform.position)
                    {
                        Destroy(toDrag.gameObject);
                        GetComponent<ResourceSystem>().AddResource(Resource.Ladan, 1);
                        canTake = true;
                    }
                    break;

                case "ResourceEye":
                    toDrag.position = Vector3.MoveTowards(toDrag.position, spawnerEye.transform.position, settings.resourceSpeed);
                    if (toDrag.position == spawnerEye.transform.position)
                    {
                        Destroy(toDrag.gameObject);
                        GetComponent<ResourceSystem>().AddResource(Resource.Eye, 1);
                        canTake = true;
                    }
                    break;

                case "ResourceStone":
                    toDrag.position = Vector3.MoveTowards(toDrag.position, spawnerStone.transform.position, settings.resourceSpeed);
                    if (toDrag.position == spawnerStone.transform.position)
                    {
                        Destroy(toDrag.gameObject);
                        GetComponent<ResourceSystem>().AddResource(Resource.Stone, 1);
                        canTake = true;
                    }
                    break;

                case "ResourceSand":
                    toDrag.position = Vector3.MoveTowards(toDrag.position, spawnerSand.transform.position, settings.resourceSpeed);
                    if (toDrag.position == spawnerSand.transform.position)
                    {
                        Destroy(toDrag.gameObject);
                        GetComponent<ResourceSystem>().AddResource(Resource.Sand, 1);
                        canTake = true;
                    }
                    break;

                default:
                    break;
            }
        }
        #endregion
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.02f);
        if (toDrag != null)
        {
            GetComponent<AudioSource>().clip = stopDragging;
            GetComponent<AudioSource>().Play();

            toDrag.GetComponent<Collider2D>().enabled = false;
            toDrag.GetComponent<SpriteRenderer>().sortingOrder = 4;
        }
        else
            canTake = true;
    }

    public void StartMixing()       //состояние обьекта когда его нельзя взять? 
    {
        canTake = false;
    }

    public void StopMixing()        //состояние обьекта когда его можно взять? вероятно готовый напиток? 
    {
        canTake = true;
    }
}