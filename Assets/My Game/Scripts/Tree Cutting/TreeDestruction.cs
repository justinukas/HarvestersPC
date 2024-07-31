using System.Collections;
using UnityEngine;

public class TreeDestruction : MonoBehaviour
{
    public IEnumerator DestroyTree()
    {
        yield return new WaitForSeconds(2.0f);
        Destroy(gameObject);
    }
}
