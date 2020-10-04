using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class ObjectCaller : MonoBehaviour, IDragHandler, IPointerDownHandler, IEndDragHandler, IPointerUpHandler
{
    public GameObject objectDrag;
    public GameObject objectGame;
    public Canvas canvas;
    private GameObject objectDragInstance;
    private GameManage gameManager;
    public void OnDrag(PointerEventData eventData)
    {
        objectDragInstance.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        objectDragInstance.transform.position = new Vector3(objectDragInstance.transform.position.x,
                                                            objectDragInstance.transform.position.y,
                                                            10f);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        MoveCamera cam = Camera.main.GetComponent<MoveCamera>();
        cam.CanMoveCamera = false;

        objectDragInstance = Instantiate(objectDrag);
        objectDragInstance.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        objectDragInstance.GetComponent<ObjectDragging>().card = this;

        gameManager.DraggingObject = objectDragInstance;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        gameManager.PlaceObject();
        gameManager.DraggingObject = null;
        Destroy(objectDragInstance);

        MoveCamera cam = Camera.main.GetComponent<MoveCamera>();
        cam.CanMoveCamera = true;


    }

    public void OnEndDrag(PointerEventData eventData)
    {
        MoveCamera cam = Camera.main.GetComponent<MoveCamera>();
        cam.CanMoveCamera = true;
 
    }

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManage.instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
