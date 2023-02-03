using UnityEngine;

public class MouseIndicator : MonoBehaviour
{
    public BoolVariable canMerge;
    public BoolVariable canSplit;
    public BoolVariable shouldShow;

    public GameObject merge;
    public GameObject split;

    private void LateUpdate()
    {
        var pos = Input.mousePosition;
        pos.z = 10;
        transform.position = Camera.main.ScreenToWorldPoint(pos);
        split.SetActive(shouldShow && canSplit);
        merge.SetActive(shouldShow && canMerge);

    }

}
