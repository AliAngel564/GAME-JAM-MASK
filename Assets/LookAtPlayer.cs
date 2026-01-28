using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    
    [Header("Camera and Canvas Transforms")]
    [SerializeField] private RectTransform promptTransform;
    [SerializeField] private Transform playerTransform;
    
    private void Update()
    {
        promptTransform.LookAt(promptTransform.position-playerTransform.position, Vector3.up);
    }
    
}
