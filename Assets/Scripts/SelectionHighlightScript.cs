using UnityEngine;

public class SelectionHighlightScript : MonoBehaviour
{
    public void SetActivity(bool active)
    {
        gameObject.SetActive(active);
    }

    public void MoveToPos(Vector3 pos)
    {
        transform.position = pos;
    }
}
