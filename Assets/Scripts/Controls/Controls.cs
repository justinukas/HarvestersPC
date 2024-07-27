using UnityEngine;

public class Controls : MonoBehaviour
{
    [SerializeField] EscapeMenu EscapeMenu;
    [SerializeField] ToolInteractions ToolInteractions;
    [SerializeField] Movement Movement;

    void Update()
    {
        Controls_ToolInteractions();
        Controls_EscapeMenu();
        Controls_Movement();
    }

    private void Controls_ToolInteractions()
    {
        ToolInteractions.ItemPositionAndRotation();
        if (Input.GetMouseButtonDown(0))
        {
            ToolInteractions.ItemUse();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            ToolInteractions.ItemRaycast();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ToolInteractions.ItemDrop();
        }
    }

    private void Controls_EscapeMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            EscapeMenu.OpenMenu();
        }
    }

    private void Controls_Movement()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Movement.Jump();
        }
    }
}
