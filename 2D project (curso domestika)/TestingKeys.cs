using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingKeys : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /**
         * Getting mouse button event:
         * -> 0 Left Button
         * -> 1 Right Button
         * -> 2 Middle Button
         */
        if (Input.GetMouseButtonDown(0)) {
            Debug.Log("Button pressed");
        }
        if (Input.GetMouseButton(0)) {
            Debug.Log("Button is pressed");
        }
        if (Input.GetMouseButtonUp(0)) {
            Debug.Log("Button released");
        }

        /**
         * Getting Keyboard button event:
         * -> KeyCode: using keycode is valid, but it gives us little freedom to work
         * -> Button: using "Jump", for example, is also valid, but in this case, we have a lot of freedom to configure the settings to work
         */

        if (Input.GetKeyDown(KeyCode.Space)){
            Debug.Log("Using Kaycode. You con use this to make the player jump");
        }
        if (Input.GetButtonDown("Jump")){
            Debug.Log("Using Jump. You con use this to make the player jump");
        }

        /**
         * Getting the Axis for movement
         * 
         * - The default settings for:
         * -> Horizontal: A and D or LEFT and RIGHT arrow
         * -> Vertical: W and S or UP and DOWN arrow
         */

        float horizontal = Input.GetAxis("Horizontal"); // -1 to 1 on X
        float vertical = Input.GetAxis("Vertical"); // -1 to 1 on Y

        if (horizontal > 0f || horizontal < 0f) {
            Debug.Log("Horizontal axis is: " + horizontal);
        }
        if (vertical > 0f | vertical < 0f) {
            Debug.Log("Vertical axes is: " + vertical);
        }

    }
}
