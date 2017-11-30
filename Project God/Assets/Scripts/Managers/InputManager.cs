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
    private Dictionary<string, XboxCtrlrInput.XboxAxis> m_stringToFloat = new Dictionary<string, XboxCtrlrInput.XboxAxis>();
    private Dictionary<string, XboxCtrlrInput.XboxButton> m_stringToBool = new Dictionary<string, XboxCtrlrInput.XboxButton>();

    private INPUT_STATE m_inputState = INPUT_STATE.KEYBOARD;

    // Use this for initialization
    void Start ()
    {
        if (XboxCtrlrInput.XCI.GetNumPluggedCtrlrs() > 0)
            m_inputState = INPUT_STATE.CONTROLLER; //Controller is detected

        //Set up dictionaries

        //Xbox Controls
        //Axis
        m_stringToFloat.Add("Horizontal", XboxCtrlrInput.XboxAxis.LeftStickX);
        m_stringToFloat.Add("Vertical", XboxCtrlrInput.XboxAxis.LeftStickY);
        m_stringToFloat.Add("HeavyAttack", XboxCtrlrInput.XboxAxis.RightTrigger);
        //Buttons
        m_stringToBool.Add("Jump", XboxCtrlrInput.XboxButton.A);
        m_stringToBool.Add("Sprint", XboxCtrlrInput.XboxButton.B);
        m_stringToBool.Add("LightAttack", XboxCtrlrInput.XboxButton.RightBumper);
    }

    public float GetInputFloat(string inputName)
    {
        if (m_inputState == INPUT_STATE.KEYBOARD)
            return Input.GetAxis(inputName);
        else
            return XboxCtrlrInput.XCI.GetAxis(m_stringToFloat[inputName]);
    }

    public bool GetInputBool(string inputName)
    {
        if (m_inputState == INPUT_STATE.KEYBOARD)
            return Input.GetButton(inputName);
        else
        {
            switch (inputName)
            {
                case "Jump":
                case "Sprint":
                case "LightAttack":
                    return XboxCtrlrInput.XCI.GetButton(m_stringToBool[inputName]);

                case "HeavyAttack": // Trigger float to bool
                    if (XboxCtrlrInput.XCI.GetAxisRaw(m_stringToFloat[inputName]) > 0)
                        return true;
                    return false;
            }
        }

        return false;
    }
}
