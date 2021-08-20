using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ObjectPlacement : MonoBehaviour
{
    // For raycasting, we have to first add the AR Raycast Manager Script to the ARSessionOrigin object. 
    public ARSessionOrigin ar_session_origin;

    private List<ARRaycastHit> raycastHit = new List<ARRaycastHit>();

    public GameObject cubePrefab;
    private GameObject instantiatedObject;
    

    // Update is called once per frame
    void Update()
    {
        // 1. Detect the user touch
        // 2. Project a raycast
        // 3. Instantiate the virtual cube at the point where the raycast hits the detected plane

        // GetMouseButton(0) represents left mouse click in PC and tap on the screen in mobiles.
        if (Input.GetMouseButton(0))
        {
            // Raycast function has 3 parameters. The first one is the screen point. It is the point at which the user touched the screen.
            // We get this by Input.MousePosition, which is the touch position.
            bool hit = ar_session_origin.GetComponent<ARRaycastManager>().Raycast(Input.mousePosition, raycastHit, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon);

            if(hit)
            {
                if (instantiatedObject == null)
                {
                    instantiatedObject = Instantiate(cubePrefab);

                    // To hide all the plane visualizers that the app has added till now,
                    // we use this loop that iterates over all the trackables (there can be many plane visualizers in the scene) that the app has already added till now
                    // and disables all those one by one.
                    foreach (var plane in ar_session_origin.GetComponent<ARPlaneManager>().trackables)
                        plane.gameObject.SetActive(false);

                    // prevent the app from adding new plane visualizers
                    ar_session_origin.GetComponent<ARPlaneManager>().enabled = false;
                }
                    
                instantiatedObject.transform.position = raycastHit[0].pose.position;
            }
        }
    }
}
