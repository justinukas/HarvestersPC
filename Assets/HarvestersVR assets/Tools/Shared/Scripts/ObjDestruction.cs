using UnityEngine;

public class ObjDestruction : MonoBehaviour
{
    public void DestroyObject(float delay)
    {
        Destroy(gameObject, delay);
    }
}
