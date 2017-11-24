using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    private enum INPUT_STATE
    {
        KEYBOARD,
        CONTROLLER
    }

    //String to controller axis
    private Dictionary<string, XboxCtrlrInput.XboxAxis> m_stringToAxis = new Dictionary<string, XboxCtrlrInput.XboxAxis>();

    private Dictionary<string, XboxCtrlrInput.XboxButton> m_stringToButton = new Dictionary<string, XboxCtrlrInput.XboxButton>();

    private INPUT_STATE m_inputState = INPUT_STATE.KEYBOARD;

    // Use this for initialization
    void Start ()
    {
        if (XboxCtrlrInput.XCI.GetNumPluggedCtrlrs() > 0)
            m_inputState = INPUT_STATE.CONTROLLER; //Controller is detected

        //Set up dictionaries
        //Keyboard TODO

        //Xbox Controls
        //Axis
        m_stringToAxis.Add("Horizontal", XboxCtrlrInput.XboxAxis.LeftStickX);
        m_stringToAxis.Add("Vertical", XboxCtrlrInput.XboxAxis.LeftStickY);
        //Buttons
        m_stringToButton.Add("Jump", XboxCtrlrInput.XboxButton.A);
        m_stringToButton.Add("Sprint", XboxCtrlrInput.XboxButton.B);
        m_stringToButton.Add("Attack", XboxCtrlrInput.XboxButton.X);
    }

    public float GetRawAxis(string axisName)
    {
        if (m_inputState == INPUT_STATE.KEYBOARD)
            return Input.GetAxisRaw(axisName);
        else
            return XboxCtrlrInput.XCI.GetAxisRaw(m_stringToAxis[axisName]);
    }

    public bool GetButton(string buttonName)
    {
        if (m_inputState == INPUT_STATE.KEYBOARD)
            return Input.GetButton(buttonName);
        else
            return XboxCtrlrInput.XCI.GetButton(m_stringToButton[buttonName]);
    }
}
