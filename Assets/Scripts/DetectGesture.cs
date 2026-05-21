using UnityEngine;
using UnityEngine.XR.Hands;
using UnityEngine.XR.Hands.Gestures;
using UnityEngine.XR.Hands.Samples.GestureSample;

public class DetectGesture : MonoBehaviour
{
    [SerializeField] private XRHandTrackingEvents handTrackingEvents;
    [SerializeField] private XRHandShape[] handShapes;
    [SerializeField] private float gestureDetectionInterval = 0.05f;
    [SerializeField] private float minimumDetectionThreshold = 0.74f;
    [SerializeField] private HandShapeCompletenessCalculator handShapeCompletenessCalculator;

    public XRHandShape shapeRecognized;

    private float timeOfLastCondition; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable() => handTrackingEvents.jointsUpdated.AddListener(OnJointsUpdate);

    void OnDisable() => handTrackingEvents.jointsUpdated.RemoveListener(OnJointsUpdate);

    void OnJointsUpdate(XRHandJointsUpdatedEventArgs eventArgs)
    {
        if (Time.time - timeOfLastCondition < gestureDetectionInterval)
            return;

        foreach (var handShape in handShapes)
        {
            handShapeCompletenessCalculator.TryCalculateHandShapeCompletenessScore(eventArgs.hand,
                handShape, out float completenessScore);

            var detected = handTrackingEvents.handIsTracked && completenessScore >= minimumDetectionThreshold;

            if (detected)
            {
                // Debug.Log($"Hand Gesture Detected: {handShape.name} | Score: {completenessScore}");
                shapeRecognized = handShape;
            }
        }
        timeOfLastCondition = Time.time;
    }
}
