using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 3f;

    public virtual void Interact()
    {
        //this method is meant to be overwritten
        //Debug.Log("Interact");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}