using UnityEngine;

public class SetFPS : MonoBehaviour
{
    void Awake()
    {
        Application.targetFrameRate = 60;
    }
}
