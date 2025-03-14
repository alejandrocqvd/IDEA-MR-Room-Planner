using UnityEngine;

public class SnapZRotation : MonoBehaviour
{
    // This method can be called when the object is released.
    public void SnapRotation()
    {
        Vector3 currentEuler = transform.eulerAngles;
        float z = currentEuler.z;

        // Normalize z to be between -180 and 180 degrees
        if (z > 180f)
            z -= 360f;

        // Snap logic: if beyond ±45°, go to ±90°, else snap to 0.
        //if (z > 45f)
        //    z = 90f;
        if (z < -45f)
            z = -90f;
        else
            z = 0f;

        // Apply the snapped rotation (keeping x and y as is)
        transform.eulerAngles = new Vector3(currentEuler.x, currentEuler.y, z);
    }
}
