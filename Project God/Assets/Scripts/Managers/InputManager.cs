using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    private enum INPUT_STATE
    {
        KEYBOARD,
        CONTROLLER
    }

    //String to controller axis
    private Dictionary<string, XboxCtrlrInput.XboxAxis> m_stringToFloat = new Dictionary<string, XboxCtrlrInput.XboxAxis>();
    private Dictionary<string, XboxCtrlrInput.XboxButton> m_stringToBool = new Dictionary<string, XboxCtrlrInput.XboxButton>();

    //Decide if using controller or keyboard
    private INPUT_STATE m_inputState = INPUT_STATE.KEYBOARD;

    private Camera m_mainCamera = null;

    public static InputManager m_instance = null;

    //Setting up for singleton use
    void Awake()
    {
        if (m_instance == null)
            m_instance = this;

        else if (m_instance != this)
            Destroy(gameObject);
    }

    // Use this for initialization
    void Start ()
    {
        // Set up player varible
        m_mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        if (XboxCtrlrInput.XCI.GetNumPluggedCtrlrs() > 0)
            m_inputState = INPUT_STATE.CONTROLLER; //Controller is detected

        //Set up dictionaries

        //Xbox Controls
        //Axis
        m_stringToFloat.Add("MovementHorizontal", XboxCtrlrInput.XboxAxis.LeftStickX);
        m_stringToFloat.Add("MovementVertical", XboxCtrlrInput.XboxAxis.LeftStickY);
        m_stringToFloat.Add("AimingHorizontal", XboxCtrlrInput.XboxAxis.RightStickX);
        m_stringToFloat.Add("AimingVertical", XboxCtrlrInput.XboxAxis.RightStickY);
        m_stringToFloat.Add("HeavyAttack", XboxCtrlrInput.XboxAxis.RightTrigger);

        //Buttons
        m_stringToBool.Add("Jump", XboxCtrlrInput.XboxButton.A); 
        m_stringToBool.Add("Sprint", XboxCtrlrInput.XboxButton.B);
        m_stringToBool.Add("Dash", XboxCtrlrInput.XboxButton.X);
        m_stringToBool.Add("LightAttack", XboxCtrlrInput.XboxButton.RightBumper);
        m_stringToBool.Add("SwapWeapon", XboxCtrlrInput.XboxButton.Y);
    }

    public float GetInputFloat(string inputName)
    {
        if (m_inputState == INPUT_STATE.KEYBOARD)
            return Input.GetAxis(inputName);
        else
            return XboxCtrlrInput.XCI.GetAxis(m_stringToFloat[inputName]);
    }

    public float GetInputFloatRaw(string inputName)
    {
        if (m_inputState == INPUT_STATE.KEYBOARD)
            return Input.GetAxisRaw(inputName);
        else
            return XboxCtrlrInput.XCI.GetAxisRaw(m_stringToFloat[inputName]);
    }

    public bool GetInputBool(string inputName)
    {
        if (m_inputState == INPUT_STATE.KEYBOARD)
            return Input.GetButton(inputName);
        else
        {
            switch (inputName)
            {
                //Special cases
                case "HeavyAttack": // Trigger float to bool
                    if (XboxCtrlrInput.XCI.GetAxisRaw(m_stringToFloat[inputName]) > 0)
                        return true;
                    return false;

                default:
                    return XboxCtrlrInput.XCI.GetButton(m_stringToBool[inputName]);
            }
        }
    }

    public Vector3 GetAimingDirection(Vector3 compareToPos)
    {
        if (m_inputState == INPUT_STATE.KEYBOARD)
        {
            //Get relavant vectors
            Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, compareToPos.z - m_mainCamera.gameObject.transform.position.z);
            mousePos = m_mainCamera.ScreenToWorldPoint(mousePos); //Convert to world coords

            Vector3 aimingDir = mousePos - compareToPos;
            aimingDir.z = 0;

            return aimingDir.normalized;
        }
        else
        {
            return (new Vector3(GetInputFloat("AimingHorizontal"), GetInputFloat("AimingVertical"), 0.0f)); 
        }
    }
}
