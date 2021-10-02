using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace cs294_137.hw2
{
    public enum TrackableType {None, PlaneWithinPolygon, PlaneWithinBounds, PlaneWithinInfinity, PlaneEstimated, Planes, FeaturePoint, Image, Face, All}
    public class ARRaycastHit : MonoBehaviour
    {
        public ARPlane arPlane;
        public Vector3 hitPosition;
        public Vector3 hitNormal;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}