using BreadAndButter.Mobile;
using UnityEngine;
public class MobileTest : MonoBehaviour
{
    [SerializeField] private bool testJoystick = false;
    [SerializeField] private bool testSwipe = false;
    
    private void Start() => MobileInput.Initialise();
    private void Update()
    {
        if (testJoystick)
        {
            transform.position += transform.right * MobileInput.GetJoystickAxis(JoystickAxis.Horizontal) * Time.deltaTime;
            transform.position += transform.up * MobileInput.GetJoystickAxis(JoystickAxis.Vertical) * Time.deltaTime;
        }

        if (testSwipe)
        {
#if (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
            // touch inputs here
#else
            // Touch position emulation.
            Vector2 touchPos = Input.mousePosition;
            
            // Touch start emulation.
            if (Input.GetMouseButtonDown(0))
            {

            }

            // Touch update emulation.
            if (Input.GetMouseButton(0))
            {

            }

            // Touch end emulation.
            if (Input.GetMouseButtonUp(0))
            {

            }
#endif
        }
    }
}