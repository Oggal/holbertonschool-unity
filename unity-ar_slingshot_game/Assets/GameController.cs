using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

[RequireComponent(typeof(PlaneManager))]
public class GameController : MonoBehaviour
{

    List<Vector2> targets;
    public GameObject targetPrefab;
    [Space(15)]
    [Header("UI Elements")]
    public GameObject cam;
    public int lineResolution = 10;
    [SerializeField] GameObject hitmarker;
    LineRenderer line;

    GameObject targetParent;
    PlaneManager planeManager;
    Vector2? touchStart = null;

    enum status { searching, gameStarted, gameEnded };
    
    [SerializeField] AnimationCurve curve;
    status curStatus = status.searching;
    public void Start()
    {
        planeManager = GetComponent<PlaneManager>();
        line = cam.GetComponent<LineRenderer>();
        planeManager.mainPlaneFound.AddListener(StartGame);
        planeManager.mainPlaneLost.AddListener(ErrorEnd);
    }
    public void OnGUI()
    {

        GUI.Label(new Rect(Screen.width/2f, Screen.height/2f, 100, 100),
            curStatus.ToString(), 
            new GUIStyle() {
                fontSize = 20,
                alignment = TextAnchor.MiddleCenter
            });
    }

    public void Update()
    {
        if(curStatus == status.gameStarted)
        {
            HandleRunningInput();
        }
    }

    public void StartGame()
    {
        targetParent = planeManager.mainPlane.gameObject;
        curStatus = status.gameStarted;
    }

    public void ErrorEnd()
    {
        curStatus = status.gameEnded;
    }

    public void HandleRunningInput()
    {
        if( Input.touchCount ==0)
        {
            //line.enabled = false;
            return;
        }
        Touch touch = Input.GetTouch(0);
        if (touch.phase == TouchPhase.Began || touchStart == null)
        {
            touchStart = touch.position;
            return;
        }
        Vector2 drag = touch.position - touchStart.Value;
        Vector3 rayDir = cam.transform.forward;
        rayDir = Quaternion.Euler(0, drag.x * -0.25f, (drag.y) * 0.37f) * rayDir;
        RaycastHit hit;
        Vector3 hitPos = cam.transform.position + (rayDir * 5f);
        
        if(Physics.Raycast(cam.transform.position, rayDir, out hit, 10f))
        {
            hitPos = hit.point;
            hitmarker.transform.position = hit.point;
        }
        line.positionCount = lineResolution;
        for(int i = 0; i < lineResolution; i++)
        {
            var t = i / (float)lineResolution;
            line.SetPosition(i,Vector3.Lerp(cam.transform.position 
                - (cam.transform.up * 1.5f) 
                + (cam.transform.forward*t),
            hitPos,curve.Evaluate(t)));
        }
        line.enabled = true;
        hitmarker.SetActive(hit.collider != null);
    }

}
