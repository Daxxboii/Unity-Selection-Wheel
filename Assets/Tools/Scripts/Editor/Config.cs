using UnityEngine;
using System;
using System.Collections.Generic;
public class Config : MonoBehaviour
{
    public struct ButtonConfig
    {
        public string title;
        public Action action;
    }

    public static List<ButtonConfig> buttonConfigs = new List<ButtonConfig>();

    public static void Init(){
        buttonConfigs.Clear();
        buttonConfigs.Add(new ButtonConfig{title = "Button 1", action = () => Debug.Log("Button 1 Pressed")});
        buttonConfigs.Add(new ButtonConfig{title = "Button 2", action = () => Debug.Log("Button 2 Pressed")});
        buttonConfigs.Add(new ButtonConfig{title = "Button 3", action = () => Debug.Log("Button 3 Pressed")});
        buttonConfigs.Add(new ButtonConfig{title = "Button 4", action = () => Debug.Log("Button 4 Pressed")});
    }
}
