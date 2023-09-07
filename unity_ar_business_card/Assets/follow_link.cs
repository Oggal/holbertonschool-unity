using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class follow_link : MonoBehaviour
{
    public string link;

    public void OpenLink()
    {
        Application.OpenURL(link);
    }

    public void SendEmail()
    {
        Application.OpenURL("mailto:" + link);
    }
}
