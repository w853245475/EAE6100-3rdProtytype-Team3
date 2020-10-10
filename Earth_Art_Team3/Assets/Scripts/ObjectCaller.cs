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

    [Header("Given By Customer")]
    public bool IsGivenByCustomer;
    public int SeedTag;
    public void OnDrag(PointerEventData eventData)
    {
        objectDragInstance.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        objectDragInstance.transform.position = new Vector3(objectDragInstance.transform.position.x,
                                                            objectDragInstance.transform.position.y,
                                                            10f);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        this.transform.localScale = 1.5f * this.transform.localScale;
        if (IsGivenByCustomer)
        {
            if(SeedTag == 1)
            {
                canvas.transform.GetChild(4).gameObject.SetActive(true);
                Destroy(this.gameObject);
            }
        }

        MoveCamera cam = Camera.main.GetComponent<MoveCamera>();
        cam.CanMoveCamera = false;

        objectDragInstance = Instantiate(objectDrag, canvas.transform);
        objectDragInstance.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        objectDragInstance.GetComponent<ObjectDragging>().card = this;

        gameManager.DraggingObject = objectDragInstance;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        this.transform.localScale = this.transform.localScale / 1.5f;
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
