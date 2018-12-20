using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UserID : MonoBehaviour
{

    public Text userIDText;
    
    public void SetUserID()
    {
        PlayerPrefs.SetString("UserID", userIDText.text);
    }
}
