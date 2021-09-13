using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace cs294_137.hw2
{
    public enum PlaneType { NONE, VERTICAL, HORIZONTAL }
    
    [RequireComponent(typeof(MeshRenderer))]
    public class ARPlane : MonoBehaviour
    {
        public PlaneType planeType = PlaneType.NONE;


        MeshRenderer meshRenderer;
        ARPlaneManager arPlaneManager;
        // Start is called before the first frame update
        void Start()
        {
            meshRenderer = this.GetComponent<MeshRenderer>();
         arPlaneManager = FindObjectOfType<ARPlaneManager>();
        }

        // Update is called once per frame
        void Update()
        {
            if(!meshRenderer.enabled )
            {
                if ((planeType != PlaneType.NONE && (int)planeType == (int)arPlaneManager.detectionMode) || arPlaneManager.detectionMode == PlaneDetectionMode.All)
                {
                    meshRenderer.enabled = true; 
                }
            }
            else
            {
                if ((planeType == PlaneType.NONE || ((int)planeType != (int)arPlaneManager.detectionMode)) && arPlaneManager.detectionMode != PlaneDetectionMode.All)
                {
                    meshRenderer.enabled = false;
                }
            }

        }



    }
}