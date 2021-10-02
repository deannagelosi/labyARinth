using System.Collections.Generic;
using UnityEngine;

namespace cs294_137.hw2
{
    public class ARRaycastManager : MonoBehaviour
    {
        ARPlaneManager arPlaneManager;
        // Start is called before the first frame update
        void Start()
        {
            arPlaneManager = FindObjectOfType<ARPlaneManager>();

        }

        // Update is called once per frame
        void Update()
        {

        }
        public bool Raycast(Vector2 screenPoint, ref List<ARRaycastHit> hitResults, TrackableType trackableTypes = TrackableType.PlaneWithinPolygon)
        {
            Ray ray = Camera.main.ScreenPointToRay(screenPoint);
            RaycastHit[] hits;
            //hitResults = new List<ARRaycastHit>();

            bool didHitARPlane = false;
            hits = Physics.RaycastAll(ray, Mathf.Infinity);

            for (int i = 0; i < hits.Length; i++)
            {
                RaycastHit hit = hits[i];
                ARPlane arPlane = hit.transform.gameObject.GetComponent<ARPlane>();
                if (arPlane!=null)
                {
                    PlaneType planeType = arPlane.planeType;
                    if ((planeType != PlaneType.NONE && (int)planeType == (int)arPlaneManager.detectionMode) || arPlaneManager.detectionMode == PlaneDetectionMode.All)
                    {
                        didHitARPlane = true;
                        ARRaycastHit arRaycastHit = new ARRaycastHit();
                        arRaycastHit.arPlane = arPlane;
                        arRaycastHit.hitPosition = hit.point;
                        arRaycastHit.hitNormal = hit.normal;
                        hitResults.Add(arRaycastHit);
                    }

                }
            }
            return (didHitARPlane);
        }
    }
}