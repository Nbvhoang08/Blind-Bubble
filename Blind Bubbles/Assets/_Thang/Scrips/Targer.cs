using UnityEngine;

public class Target : MonoBehaviour
{
    public delegate void TargetSelected(Vector3 targetPosition);
    public static event TargetSelected OnTargetSelected;

    void OnMouseDown()
    {
        // Gửi sự kiện khi đối tượng được click
        if (OnTargetSelected != null)
        {
            OnTargetSelected(transform.position);
        }
    }
    
}
