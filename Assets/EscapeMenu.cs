using UnityEngine;

public class EscapeMenu : MonoBehaviour
{
    private bool isMenuOpen = false; // cursor-lock toggle
    private float cd = 0f; // keypress cooldown

    public void OpenMenu()
    {    
        if (isMenuOpen == true && cd >= 0.2f)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            isMenuOpen = false;
            cd = 0f;
        }
        if (isMenuOpen == false && cd >= 0.2f)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            isMenuOpen = true;   
            cd = 0f;
        }
        cd += Time.deltaTime;
    }
}
