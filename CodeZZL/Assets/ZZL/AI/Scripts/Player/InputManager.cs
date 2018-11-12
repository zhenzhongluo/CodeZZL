using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    private Dictionary<int, bool> m_keyMap;
    private Dictionary<int, bool> m_prevKeyMap;
    
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        // Loop through the current key map and copy them to the previous key map.
        foreach (KeyValuePair<int, bool> p in m_keyMap)
        {
            m_prevKeyMap[p.Key] = p.Value;
        }
    }

    public void PressKey(int keyId)
    {
        m_keyMap[keyId] = true;
    }

    public void ReleaseKey(int keyId)
    {
        m_keyMap[keyId] = false;
    }

    public bool IsKeyDown(int keyId)
    {
        if (m_keyMap.ContainsKey(keyId))
        {
            return m_keyMap[keyId];
        }
        else
        {
            return false;
        }
    }

    public bool IsKeyPressed(int keyId)
    {
        if (IsKeyDown(keyId) && !WasKeyDown(keyId))
        {
            return true;
        }

        return false;
    }

    private bool WasKeyDown(int keyId)
    {
        if (m_prevKeyMap.ContainsKey(keyId))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
