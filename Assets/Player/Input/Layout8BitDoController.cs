#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Layouts;

#if UNITY_EDITOR
[InitializeOnLoad] 
#endif
public static class Layer8BitDoController
{
    static Layer8BitDoController()
    {
        ApplyLayout();
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void ApplyLayout()
    {
        string layoutJson = @"
        {
            ""name"": ""8BitDo Ultimate 2C Wireless"",
            ""extend"": ""Gamepad"",
            ""format"": ""HID"",
            ""controls"": [
                { ""name"": ""dpad"", ""layout"": ""Dpad"", ""offset"": 1, ""bit"": 0, ""format"": ""BIT"", ""sizeInBits"": 4 },
                { ""name"": ""dpad/up"", ""layout"": ""DiscreteButton"", ""offset"": 0, ""bit"": 0, ""format"": ""BIT"", ""sizeInBits"": 4, ""parameters"": ""minValue=7,maxValue=1,nullValue=15,wrapAtValue=7"" },
                { ""name"": ""dpad/right"", ""layout"": ""DiscreteButton"", ""offset"": 0, ""bit"": 0, ""format"": ""BIT"", ""sizeInBits"": 4, ""parameters"": ""minValue=1,maxValue=3"" },
                { ""name"": ""dpad/down"", ""layout"": ""DiscreteButton"", ""offset"": 0, ""bit"": 0, ""format"": ""BIT"", ""sizeInBits"": 4, ""parameters"": ""minValue=3,maxValue=5"" },
                { ""name"": ""dpad/left"", ""layout"": ""DiscreteButton"", ""offset"": 0, ""bit"": 0, ""format"": ""BIT"", ""sizeInBits"": 4, ""parameters"": ""minValue=5,maxValue=7"" },

                { ""name"": ""leftStick"", ""layout"": ""Stick"", ""offset"": 2, ""format"": ""VC2B"" },
                { ""name"": ""leftStick/x"", ""layout"": ""Axis"", ""offset"": 0, ""format"": ""BYTE"", ""parameters"": ""normalize,normalizeMin=0,normalizeMax=1,normalizeZero=0.5"" },
                { ""name"": ""leftStick/y"", ""layout"": ""Axis"", ""offset"": 1, ""format"": ""BYTE"", ""parameters"": ""invert,normalize,normalizeMin=0,normalizeMax=1,normalizeZero=0.5"" },

                { ""name"": ""rightStick"", ""layout"": ""Stick"", ""offset"": 4, ""format"": ""VC2B"" },
                { ""name"": ""rightStick/x"", ""layout"": ""Axis"", ""offset"": 0, ""format"": ""BYTE"", ""parameters"": ""normalize,normalizeMin=0,normalizeMax=1,normalizeZero=0.5"" },
                { ""name"": ""rightStick/y"", ""layout"": ""Axis"", ""offset"": 1, ""format"": ""BYTE"", ""parameters"": ""invert,normalize,normalizeMin=0,normalizeMax=1,normalizeZero=0.5"" },

                { ""name"": ""buttonSouth"", ""layout"": ""Button"", ""offset"": 8, ""bit"": 0, ""format"": ""BIT"" },
                { ""name"": ""buttonEast"", ""layout"": ""Button"", ""offset"": 8, ""bit"": 1, ""format"": ""BIT"" },
                { ""name"": ""buttonWest"", ""layout"": ""Button"", ""offset"": 8, ""bit"": 3, ""format"": ""BIT"" },
                { ""name"": ""buttonNorth"", ""layout"": ""Button"", ""offset"": 8, ""bit"": 4, ""format"": ""BIT"" },
                { ""name"": ""leftShoulder"", ""layout"": ""Button"", ""offset"": 8, ""bit"": 6, ""format"": ""BIT"" },
                { ""name"": ""rightShoulder"", ""layout"": ""Button"", ""offset"": 8, ""bit"": 7, ""format"": ""BIT"" },

                { ""name"": ""leftTrigger"", ""layout"": ""Button"", ""offset"": 9, ""bit"": 0, ""format"": ""BIT"" },
                { ""name"": ""rightTrigger"", ""layout"": ""Button"", ""offset"": 9, ""bit"": 1, ""format"": ""BIT"" },
                { ""name"": ""select"", ""layout"": ""Button"", ""offset"": 9, ""bit"": 2, ""format"": ""BIT"" },
                { ""name"": ""start"", ""layout"": ""Button"", ""offset"": 9, ""bit"": 3, ""format"": ""BIT"" },
                { ""name"": ""system"", ""layout"": ""Button"", ""offset"": 9, ""bit"": 4, ""format"": ""BIT"" },
                { ""name"": ""leftStickPress"", ""layout"": ""Button"", ""offset"": 9, ""bit"": 5, ""format"": ""BIT"" },
                { ""name"": ""rightStickPress"", ""layout"": ""Button"", ""offset"": 9, ""bit"": 6, ""format"": ""BIT"" }
            ]
        }";

        InputSystem.RegisterLayout(layoutJson);

        InputSystem.RegisterLayoutMatcher(
            "8BitDo Ultimate 2C Wireless",
            new InputDeviceMatcher()
                .WithInterface("HID")
                .WithCapability("vendorId", 11720)
                .WithCapability("productId", 12315)
        );
    }
}