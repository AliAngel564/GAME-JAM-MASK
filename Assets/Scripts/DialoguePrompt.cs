using System;
using UnityEngine;

public class DialoguePrompt : MonoBehaviour
{
    private SphereCollider dialogueCollider;
    private Transform npcTransform;
    [Header("Prompt Spot Transform")]
    [SerializeField] private Transform promptSpot;
    [Header("Prompt Canvas")]
    [SerializeField]private Canvas promptCanvas;
    
    
    
    private void Start()
    {
        npcTransform = GetComponentInParent<Transform>();
        promptSpot.position = new Vector3(npcTransform.position.x, npcTransform.position.y+2f, npcTransform.position.z);
        dialogueCollider = GetComponent<SphereCollider>();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            promptCanvas.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            promptCanvas.enabled = false;
        }
    }
}
