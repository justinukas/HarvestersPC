using UnityEngine;

// handles all scripts that have to do with keyboard/mouse controls
namespace Main.Controls
{
    public class Controls : MonoBehaviour
    {
        [SerializeField] EscapeMenu EscapeMenu;
        [SerializeField] ToolInteractions ToolInteractions;
        [SerializeField] Movement Movement;
        [SerializeField] ChargeBar ChargeBar;
        [SerializeField] MouseLook MouseLook;

        void Update()
        {
            Controls_Movement();
            Controls_MouseLook();
            Controls_EscapeMenu();
            Controls_ToolInteractions();
            Controls_ChargeBar();
        }

        private void Controls_Movement()
        {
            Movement.MoveCharacterController();
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Movement.Jump();
            }
        }

        private void Controls_MouseLook()
        {
            MouseLook.ControlMouse();
        }

        private void Controls_EscapeMenu()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                EscapeMenu.OpenMenu();
            }
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

        private void Controls_ChargeBar()
        {
            ChargeBar.ShowBar();
            ChargeBar.ThrowBag();
            ChargeBar.HideBar();

            if (Input.GetMouseButton(0))
            {
                ChargeBar.FillBar();
            }

            if (!Input.GetMouseButtonDown(0))
            {
                ChargeBar.LockBar();
            }
        }
    }
}

