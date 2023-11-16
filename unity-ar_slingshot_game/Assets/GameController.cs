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
    [SerializeField] GameObject displaySphere;
    SphereController sphereData;
    LineRenderer line;

    GameObject targetParent;
    PlaneManager planeManager;
    Vector2? touchStart = null;
    Vector3 sphereStart;

    enum status { searching, gameStarted, gameEnded };
    
    [SerializeField] AnimationCurve curve;
    status curStatus = status.searching;
    public void Start()
    {
        planeManager = GetComponent<PlaneManager>();
        line = cam.GetComponent<LineRenderer>();
        line.positionCount = lineResolution; 
        planeManager.mainPlaneFound.AddListener(StartGame);
        planeManager.mainPlaneLost.AddListener(ErrorEnd);
        sphereStart = displaySphere.transform.localPosition;
        displaySphere.GetComponent<SphereController>().curve = curve;
        sphereData = displaySphere.GetComponent<SphereController>();
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
            if(sphereData.isReady)
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
            if(touchStart.HasValue)
            {
                sphereData.launch();
                touchStart = null;
                return;
            }
            displaySphere.transform.localPosition = sphereStart;
            return;
        }
        Touch touch = Input.GetTouch(0);
        if (touch.phase == TouchPhase.Began || touchStart == null)
        {
            touchStart = touch.position;
            return;
        }
        Vector2 drag = touch.position - touchStart.Value;
        drag.x /= Screen.width;
        drag.y /= Screen.height;
        Vector3 rayDir = cam.transform.forward;
        rayDir = Quaternion.Euler((drag.y) * 45, (drag.x) * 45, 0) * rayDir;
        RaycastHit hit;
        Vector3 hitPos = cam.transform.position + (rayDir * 5f);
        
        if(Physics.Raycast(cam.transform.position, rayDir, out hit, 10f))
        {
            hitPos = hit.point;
            hitmarker.transform.position = hit.point;
        }

        sphereData.SetData(
            Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 0.15f)),
            cam.transform.forward,
            hitPos);

        for(int i = 0; i < lineResolution; i++)
        {
            var t = i / (float)lineResolution;
            line.SetPosition(i,sphereData.CalcLine(t)); 
        }
        line.enabled = true;
        hitmarker.SetActive(hit.collider != null);
        //displaySphere.transform.position = line.GetPosition(2);
    }

}
