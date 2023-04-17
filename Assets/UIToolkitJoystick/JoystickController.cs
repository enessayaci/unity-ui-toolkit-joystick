using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

// This scripts get attached UI Document object's UIDocument component and accesses its root then renders joystick in the root

public class JoystickController : MonoBehaviour
{
    private Vector3 startPos;
    public static Vector3 input = Vector3.zero; // it is base input to publish it anywhere to use for know which direction and how strenght I move my finger.
    private bool detectJoystickMovement = false; // you can assume it as a bug fixer flag. App triggers <PointerMoveEvent> for once at the ver first frame of app for a reason I dont know why. So this flag is to prevent it happen
    private VisualElement joystickElement; // joystick itself (parent joystick element) it will be used to show and hide joystick by changing its style (display: none | flex)
    private VisualElement joystickKnob; // inner circle of joystick, dynamic moving part
    private float size = 60; // size(width and height) of joystick element, modify it if you want
    private float sensitivity = 50; // the higher, the more sensitive. 0 means sudden switches between directions(no sensitivity)
    [SerializeField] private VisualTreeAsset joystickUXML; // Joystick.uxml file
    [SerializeField] private StyleSheet joystickUSS; // Joystick.uss file

    private void OnEnable()
    {
        Initialize();
    }

    private void Initialize()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        VisualElement joystickUI = joystickUXML.Instantiate();
        VisualElement joystickTouchArea = joystickUI.Q<VisualElement>("JoystickTouchArea");
        joystickElement = joystickUI.Q("JoystickOuterBorder"); // There is a parent node named "JoystickOuterBorder" in Joystick.uxml file, just leave it as it is, you will need this variable to show/hide joystick later
        joystickKnob = joystickElement.Q("JoystickKnob"); // There is a child node named "JoystickKnob" in Joystick.uxml file, just leave it as it is, you will need this variable to move the little circle on the middle of the joystick later

        joystickElement.style.width = size; // applying width of joystick
        joystickElement.style.height = size; // applying height of joystick

        joystickKnob.style.transformOrigin = new TransformOrigin(Length.Percent(100), 0, 0);

        root.styleSheets.Add(joystickUSS); // add joystick uss file to root, it is needed to apply joystick styles
        root.Insert(0, joystickTouchArea); // add joystick touchable node to root

        joystickTouchArea.RegisterCallback<PointerDownEvent>((ev) =>
        {
            ShowJoystick(ev);
        });

        joystickTouchArea.RegisterCallback<PointerMoveEvent>((ev) =>
        {
            UpdateJoystick(ev);
        });

        joystickTouchArea.RegisterCallback<PointerUpEvent>((ev) =>
        {
            HideJoystick(ev);
        });

        joystickTouchArea.RegisterCallback<PointerLeaveEvent>((ev) =>
        {
            HideJoystick(ev);
        });

    }

    private void ShowJoystick(PointerDownEvent _ev)
    {
        detectJoystickMovement = true;
        startPos = _ev.position;
        joystickElement.style.left = _ev.position.x - size / 2;
        joystickElement.style.top = _ev.position.y - size / 2;
        joystickElement.style.display = DisplayStyle.Flex;
    }

    private void UpdateJoystick(PointerMoveEvent _ev)
    {
        if (detectJoystickMovement)
        {
            float deltaX = _ev.position.x - startPos.x;
            float deltaY = startPos.y - _ev.position.y;
            input = new Vector3(deltaX, deltaY, 0);
            input = input.normalized;

            ApplySensitivity(ref input, deltaX, deltaY, sensitivity);

            joystickKnob.style.translate = new StyleTranslate(new Translate(new Length(input.x * size / 2, LengthUnit.Pixel), new Length(-input.y * size / 2, LengthUnit.Pixel)));
        }
    }

    private void HideJoystick(PointerUpEvent _ev)
    {
        input = Vector3.zero;
        detectJoystickMovement = false;
        joystickElement.style.display = DisplayStyle.None;
        joystickKnob.style.translate = new StyleTranslate(new Translate(new Length(0, LengthUnit.Pixel), new Length(0, LengthUnit.Pixel)));
    }

    private void HideJoystick(PointerLeaveEvent _ev)
    {
        input = Vector3.zero;
        detectJoystickMovement = false;
        joystickElement.style.display = DisplayStyle.None;
        joystickKnob.style.translate = new StyleTranslate(new Translate(new Length(0, LengthUnit.Pixel), new Length(0, LengthUnit.Pixel)));
    }

    private static void ApplySensitivity(ref Vector3 input, float _deltaX, float _deltaY, float sensitivity)
    {


        if (Mathf.Abs(_deltaX) >= sensitivity || Mathf.Abs(_deltaY) >= sensitivity) { return; } // it is to avoid stuttering when one of directions is above sensitivity limit, you can assume it as a bug fixer line

        if (_deltaX > 0) // if finger movement is towards right
        {
            input.x = (_deltaX >= sensitivity) ? input.x : Mathf.Lerp(0f, 1f, _deltaX / sensitivity);
        }
        else // if finger movement is towards left
        {
            input.x = (_deltaX <= -sensitivity) ? input.x : Mathf.Lerp(0f, -1f, _deltaX / -sensitivity);
        }

        if (_deltaY > 0) // if finger movement is towards up
        {
            input.y = (_deltaY >= sensitivity) ? input.y : Mathf.Lerp(0f, 1f, _deltaY / sensitivity);
        }
        else // if finger movement is towards down
        {
            input.y = (_deltaY <= -sensitivity) ? input.y : Mathf.Lerp(0f, -1f, _deltaY / -sensitivity);
        }
    }
}
