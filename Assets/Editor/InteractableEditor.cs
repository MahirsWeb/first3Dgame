using UnityEditor;
[CustomEditor(typeof(Door),true)]
public class InteractableEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Door interactable = (Door)target;
        base.OnInspectorGUI();
        if (interactable.useEvents)
        {
            if(interactable.GetComponent<InteractionEvent>()==null)
                interactable.gameObject.AddComponent<InteractionEvent>();
        }
        else
        {
            if(interactable.GetComponent<InteractionEvent>() != null)
            {
                DestroyImmediate(interactable.GetComponent<InteractionEvent>());
            }
        }
    }
}
