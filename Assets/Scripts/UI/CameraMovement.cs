using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraMovement : MonoBehaviour
{
    public Camera cam;

    public GameObject tableBG;
    public GameObject ordersBG;
    public GameObject helpTutorial1;
    public GameObject bottles;

    public AnimationCurve curve;

    public Settings settings;

    public int dir = 1;

    private Vector3 tablePos;
    private Vector3 ordersPos;
    private Vector3 destination;

    private float timer = 0f;
    private bool isMoving = false;

    private Touch touch;

    private void Start()
    {
        tablePos = tableBG.transform.position;
        ordersPos = ordersBG.transform.position;
    }

    public void MoveCam()
    {
        helpTutorial1.SetActive(false);
        if (!GetComponent<Tutorial>().canMove) return;
        if (!isMoving)
        {
            if (dir == 1) destination = ordersPos;
            if (dir == 2) destination = tablePos;
            timer = 0f;
            isMoving = true;
        }
    }

    private void Update()
    {
        GetComponent<SwipeDetection>().Swipe();

        if (isMoving)
        {
            timer += Time.deltaTime;
            float ratio = timer / settings.camSpeed;

            Vector3 position = curve.Evaluate(ratio) * ordersPos;

            if (destination.y == ordersPos.y)
                cam.transform.position = position;
            else
                cam.transform.position = ordersPos - position;
        }

        if (destination.y == tablePos.y)
        {
            if (cam.transform.position.y == tablePos.y)
            {
                isMoving = false;
                dir = 1;
                if (GetComponent<Tutorial>().helpBuy)
                {
                    GetComponent<Tutorial>().helpBuy = false;
                    GetComponent<Tutorial>().ToggleMessage("Это рабочее место. По центру находится котел, слева и справа будут располагаться ресурсы для зельеварения.");
                }
            }
        }

        if (destination.y == ordersPos.y)
        {
            if (cam.transform.position.y == ordersPos.y)
            {
                isMoving = false;
                dir = 2;
                if (!GetComponent<Tutorial>().mainGame && GetComponent<Tutorial>().canMove && !GetComponent<Tutorial>().mainTutorial)
                {
                    GetComponent<Tutorial>().canMove = false;
                    GetComponent<Tutorial>().ToggleMessage("Перенеси зелье заказчику чтобы он его забрал и заплатил.");
                }
            }
        }

        if (Input.touchCount > 0)
        {
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                return;

            touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                Vector3 pos = cam.ScreenToWorldPoint(touch.position);
                RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);

                if (Physics2D.Raycast(pos, Vector2.zero) && hit.collider.gameObject == this.gameObject) MoveCam();
            }
        }
    }
}