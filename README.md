# unity-ui-toolkit-joystick
UI Toolkit based floating joystick for unity mobile games 🕹️ 

<img src="https://raw.githubusercontent.com/enessayaci/unity-ui-toolkit-joystick/main/Assets/Public/presentation.gif" width="300">

# Customization
You can customize some properties,

### Size and sensitivity radius

#### /UIToolkitJoystick/JoystickController.cs file
```C#
private float size = 60; // size(width and height) of joystick element, modify it if you want
private float sensitivity = 50; // the higher, the more sensitive. 0 means sudden switches between directions(no sensitivity)
```

sensitivity variable assumes a virtual circle in pixels of given value (50 means 50 pixels), but it is a bit primitive approach,. It would be more advanced by calculating it using device pixel ratios, resolutions etc. but it is useful enough as it is.

### Colors

#### /UIToolkitJoystick/Joystick.uss file
```css
#JoystickOuterBorder{
    display:none;
    border-width:3px;
    border-color: white; /* Change this to give color to outer circle */
    border-radius: 100%;
    position:absolute;
    justify-content: center;
    align-items: center;
}

#JoystickKnob{
    position: absolute;
    height: 40%;
    width: 40%;
    background-color: white; /* Change this to give color to inner circle (knob) */
    border-radius: 100%;
}
```

You can change commented lines' property values to modify colors. "border-color" property for outer circle and "background-color" property for inner circle(knob)
