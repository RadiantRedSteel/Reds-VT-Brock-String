using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class FocusBead : MonoBehaviour
{
    public Camera mainCamera;
    public Transform beadToFocusOn;

    private Volume volume;
    private DepthOfField depthOfField;

    void Start()
    {
        // Find the Volume in the scene
        volume = FindFirstObjectByType<Volume>();
        if (volume != null && volume.profile.TryGet(out depthOfField))
        {
            Debug.Log("Depth of Field component found!");
        }
        else
        {
            Debug.LogError("Depth of Field not found in Volume!");
        }
    }

    void Update()
    {
        if (beadToFocusOn != null && depthOfField != null)
        {
            float distance = Vector3.Distance(mainCamera.transform.position, beadToFocusOn.position);
            depthOfField.focusDistance.Override(distance); // Use Override to set the value
        }
    }
}