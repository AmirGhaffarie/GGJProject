using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class Branch : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Branch branchPrefab;
    public BoolVariable IsDirty;

    [Space]
    public SpriteRenderer sprite;
    public BoolVariable canMerge;
    public BoolVariable canSplit;
    public BoolVariable shouldShow;

    [Space]

    public Transform childPosition;

    [Space]
    public Branch parent = null;
    public List<Branch> children = new();

    [Space]
    public int level = 1;
    public int capacity = 3;


    public bool CanAddChild(Branch branch)
    {
        return children.Count < capacity && branch.level <= level;
    }

    public void RemoveChild(Branch branch)
    {
        branch.parent = null;
        children.Remove(branch);
        IsDirty.Set(true);
    }

    public void AddChild(Branch branch)
    {
        branch.parent = this;
        children.Add(branch);
        IsDirty.Set(true);
    }

    public bool CanSplit()
    {
        if (parent == null)
            return false;
        if (level < 2) return false;

        if (parent.children.Count >= parent.capacity)
            return false;

        return !children.Any(c => c.level >= level);
    }

    public void Split()
    {
        var midPoint = children.Count / 2;
        var branch2 = Instantiate(branchPrefab);

        level--;
        branch2.level = level;

        parent.AddChild(branch2);

        for (int i = midPoint; i < children.Count; i++)
        {
            var child = children[i];
            RemoveChild(child);
            branch2.AddChild(child);
        }

    }
    public bool CanMerge()
    {
        if (parent == null)
            return false;
        if (level > parent.level - 1) return false;
        for (int i = 0; i < parent.children.Count; i++)
        {
            Branch candidate = parent.children[i];

            if (candidate == this) continue;
            if (candidate.level == level)
            {
                if (capacity >= candidate.children.Count + children.Count)
                    return true;
            }
        }
        return false;
    }

    public void Merge()
    {
        Branch selection = null;
        for (int i = 0; i < parent.children.Count; i++)
        {
            Branch candidate = parent.children[i];
            if (candidate == this) continue;
            if (candidate.level == level)
            {
                if (capacity >= candidate.children.Count + children.Count)
                    selection = candidate;
            }
        }
        parent.RemoveChild(selection);
        for (int i = selection.children.Count - 1; i >= 0; i--)
        {
            var child = selection.children[i];
            selection.RemoveChild(child);
            AddChild(child);
        }
        level++;
        Destroy(selection.gameObject);
    }


    public void UpdateStructure()
    {
        transform.localScale = Vector3.one * level;
        if (parent == null)
        {
            transform.position = Vector3.zero;
            transform.rotation = Quaternion.identity;
        }
        else
        {
            var childCount = parent.children.Count;
            var childIndex = parent.children.IndexOf(this);

            transform.position = parent.childPosition.position;
            transform.rotation = GetRotation(childIndex, childCount);
        }
        foreach (var child in children)
        {
            child.UpdateStructure();
        }

    }

    const float maxAngle = 60;
    const float fullAngle = 120;
    private Quaternion GetRotation(int childIndex, int childCount)
    {
        if (childCount == 1) return parent.childPosition.rotation;

        var seperation = fullAngle / (childCount - 1);
        var angle = -maxAngle + seperation * childIndex;

        return parent.childPosition.rotation * Quaternion.Euler(Vector3.forward * angle);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (CanSplit())
                Split();
        }
        else
        {
            if (CanMerge())
                Merge();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        shouldShow.Set(true);
        canMerge.Set(CanMerge());
        canSplit.Set(CanSplit());
        sprite.color = Color.yellow;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        shouldShow.Set(false);
        sprite.color = Color.white;
    }
}
