using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    public string[] speetchText;
    public string actorNameText;
    private DialogueControl dc;
    private bool onRadious;
    private bool isDialogueActive = false;
    public LayerMask playerLayer;
    public float radius;
    void Start()
    {
        dc = FindObjectOfType<DialogueControl>();
    }
    private void FixedUpdate()
    {
        Interact();
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && onRadious && !isDialogueActive)
        {
            StartDialogue();
        }
    }
    private void StartDialogue()
    {
        isDialogueActive = true;
        dc.Speech(speetchText, actorNameText);
        Debug.Log("Dialogo iniciado com " + actorNameText);
    }
    private void EndDialogue()
    {
        isDialogueActive = false;
        dc.HidePanel();
        Debug.Log("Dialogo encerrado com " + actorNameText);
    }
    private void OawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, radius);
    }
    public void Interact()
    {
        Vector3 point1 = transform.position + Vector3.up * radius;
        Vector3 point2 = transform.position - Vector3.up * radius;
        Collider[] hits = Physics.OverlapCapsule(point1, point2, radius, playerLayer);
        //Collider2D hit = Physics2D.OverlapCircle(transform.position, radious, playerLayer);
        //if(hit != null)
        if (hits.Length > 0)
        {
            if (!onRadious)
            {
                Debug.Log("Player entrou na area de dialogo com " + actorNameText);
            }           
            onRadious = true;
        }
        else
        {
            if (onRadious)
            {
                Debug.Log("Player saiu da area de dialogo com " + actorNameText);
                EndDialogue();
            }
            onRadious = false;
            if (isDialogueActive)
            {
                EndDialogue();
            }
        }
    }
}
