using UnityEngine;

public class TreeUpdater : MonoBehaviour
{
    [SerializeField] private Branch branch;
    [SerializeField] BoolVariable isDirty;

    private void LateUpdate()
    {
        if (isDirty)
        {
            branch.UpdateStructure();
        }
    }
#if UNITY_EDITOR
    private void OnValidate()
    {
        branch = GetComponent<Branch>();
    }
#endif
}
