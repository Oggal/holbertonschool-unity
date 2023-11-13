using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.ARFoundation;

public class PlaneManager : MonoBehaviour
{
    ARPlaneManager planeManager;
    public ARPlane mainPlane {get; internal set; } = null;
    public bool searching = true;

    public UnityEvent mainPlaneFound, mainPlaneLost;
    public void OnGUI()
    {
        if (planeManager != null)
        {
            foreach (ARPlane plain in planeManager.trackables)
            {
                if (!plain.enabled)
                {
                    continue;
                }
                Mesh mesh = plain.GetComponent<MeshFilter>().mesh;
                Vector3 screenPos = Camera.main.WorldToScreenPoint(plain.transform.position);
                GUI.Label(new Rect(screenPos.x, Screen.height - screenPos.y, 100, 100),
                    plain.gameObject.name, 
                    new GUIStyle() { fontSize = 20}
                );
            }
        }

    }

    public void Awake()
    {
        planeManager = GetComponent<ARPlaneManager>();
    }

    public void Update()
    {
        if(mainPlane == null && Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Began)
            {
                RaycastHit hit;
                if(Physics.Raycast(Camera.main.ScreenPointToRay(touch.position),out hit))
                {
                    if(hit.collider.gameObject.GetComponent<ARPlane>() != null)
                    {
                        mainPlane = hit.collider.gameObject.GetComponent<ARPlane>();
                        mainPlaneFound.Invoke();
                        
                        foreach( ARPlane plane in planeManager.trackables)
                        {
                            if(plane != mainPlane)
                            {
                                plane.gameObject.SetActive(false);
                            }
                        }
                        planeManager.enabled = false;
                    }
                }
            }
        }
    }

        public void OnPlanesChanged(ARPlanesChangedEventArgs args)
        {
            if(args.removed.Contains(mainPlane))
            {
                mainPlane = null;
                mainPlaneLost.Invoke();
            }
            args.added.ForEach((plane) => {
                plane.gameObject.SetActive(searching || plane == mainPlane);
            });
        }
}
