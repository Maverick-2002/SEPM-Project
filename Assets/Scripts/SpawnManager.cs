using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;

[RequireComponent(typeof(ARRaycastManager))]
public class SpawnManager : MonoBehaviour
{
    public GameObject objectToSpawn;
    private GameObject createdObject;
    private ARRaycastManager aRRaycastManager;

    private List<ARRaycastHit> hits = new List<ARRaycastHit>();
    private Touch touchZero;
    private Touch touchOne;
    private Vector2 touchZeroPreviousPos;
    private Vector2 touchOnePreviousPos;
    private Vector3 initialScale;
    private float initialDistance;
    private bool planeDetectionEnabled = true;

    public Slider zoomSlider;
    public float minZoom = 1f;
    public float maxZoom = 5f;

    private void Awake()
    {
        aRRaycastManager = GetComponent<ARRaycastManager>();
    }

    private void Start()
    {
        zoomSlider.onValueChanged.AddListener(OnZoomSliderChanged);
    }

    void Update()
    {
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            // Rotation
            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
            float rotationSpeed = 0.5f;
            if (createdObject != null)
            {
                createdObject.transform.Rotate(0, -touchDeltaPosition.x * rotationSpeed, 0, Space.World);
            }
        }

        if (Input.touchCount == 0)
        {
            touchZeroPreviousPos = Vector2.zero;
            touchOnePreviousPos = Vector2.zero;
        }

        if (Input.touchCount > 0 && createdObject == null && planeDetectionEnabled)
        {
            var touchPosition = Input.touches[0].position;

            if (aRRaycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
            {
                Pose hitPose = hits[0].pose;
                createdObject = Instantiate(objectToSpawn, hitPose.position, hitPose.rotation);
                initialScale = createdObject.transform.localScale;
                initialDistance = Vector2.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);

                // Disable plane detection
                aRRaycastManager.enabled = false;
                planeDetectionEnabled = false;
            }
        }
    }

    public void OnZoomSliderChanged(float value)
    {
        if (createdObject != null)
        {
            // Clamp the zoom value between minZoom and maxZoom
            float clampedZoom = Mathf.Clamp(value, minZoom, maxZoom);

            // Adjust the scale of the object based on the zoom value
            createdObject.transform.localScale = initialScale * clampedZoom;
        }
    }
}
