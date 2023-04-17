# unity-ui-toolkit-joystick
UI Toolkit based floating joystick for unity mobile games 🕹️ 

<img src="https://raw.githubusercontent.com/enessayaci/unity-ui-toolkit-joystick/main/Assets/Public/presentation.gif" width="300">

# Usage

<ol>
    <li>Open the scene, create a new UI Document object by Right Click > UI Toolkit > UI Document </li>
    <li>Select Panel Settings and Source Asset (uxml file) of UIDocument component on the inspector (Demo Panel Settings and Demo.uxml for my case)</li>
    <li>Attach JoystickController.cs to the UI Document object. This script will create an element named "JoystickTouchArea" and place in root to handle joystick pointer events.</li>
    <li>Be sure that picking-mode properties of your UI Toolkit element's is properly set to use floating buttons which are clickable as joystick working at the same time.</li>
    
</ol>

 Note: The VisualElement "JoystickTouchArea" will be placed at beggining of nodes so it will be rendered at backmost layer, so you must be careful adjusting picking modes the elements over JoystickTouchArea (every element will be over it anyway because it is rendered by root.Insert(0, JoystickTouchArea))
    
<img src="https://raw.githubusercontent.com/enessayaci/unity-ui-toolkit-joystick/main/Assets/Public/everything.png" width="300"> <img src="https://raw.githubusercontent.com/enessayaci/unity-ui-toolkit-joystick/main/Assets/Public/body.png" width="300"> <img src="https://raw.githubusercontent.com/enessayaci/unity-ui-toolkit-joystick/main/Assets/Public/floating_button.png" width="300">

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
