
using System.Collections;
using UnityEngine;
using Vuforia;

public class VirtualButtonEventHandler : MonoBehaviour
{


    public Material VirtualButtonDefault;
    public Material VirtualButtonPressed;
    
    public float ButtonReleaseTimeDelay;
    
    public GameObject AcademicRecord;
    public GameObject RegisteredCourses;


    VirtualButtonBehaviour[] MyVB;

    void Awake()
    {

        AcademicRecord.SetActive(false);
        RegisteredCourses.SetActive(false);

        // Register with the virtual buttons ObserverBehaviour
        MyVB = GetComponentsInChildren<VirtualButtonBehaviour>();

        for (var i = 0; i < MyVB.Length; ++i)
        {
            MyVB[i].RegisterOnButtonPressed(OnButtonPressed);
            MyVB[i].RegisterOnButtonReleased(OnButtonReleased);
        }
    }

    public void Destroy()
    {
        MyVB = GetComponentsInChildren<VirtualButtonBehaviour>();

        for (var i = 0; i < MyVB.Length; ++i)
        {
            MyVB[i].UnregisterOnButtonPressed(OnButtonPressed);
            MyVB[i].UnregisterOnButtonReleased(OnButtonReleased);
        }
    }

    public void OnButtonPressed(VirtualButtonBehaviour vb)
    {
        //StopAllCoroutines();
        BroadcastMessage("HandleVirtualButtonPressed", SendMessageOptions.DontRequireReceiver);

        switch (vb.VirtualButtonName)
        {
            case "Courses Requests":
                SetVirtualButtonMaterial(VirtualButtonPressed, vb);
                Debug.Log(vb.VirtualButtonName);
                break;
            case "Academic Record":
                SetVirtualButtonMaterial(VirtualButtonPressed, vb);
                Debug.Log(vb.VirtualButtonName);
                AcademicRecord.SetActive(true);
                break;
            case "Show Registered Courses":
                SetVirtualButtonMaterial(VirtualButtonPressed, vb);
                Debug.Log(vb.VirtualButtonName);
                RegisteredCourses.SetActive(true);
                break;
            default:
                break;
        }


    }

    public void OnButtonReleased(VirtualButtonBehaviour vb)
    {
        switch (vb.VirtualButtonName)
        {
            case "Courses Requests":
                SetVirtualButtonMaterial(VirtualButtonDefault, vb);
                Debug.Log(vb.VirtualButtonName);
                StartCoroutine(DelayOnButtonReleasedEvent(ButtonReleaseTimeDelay));
                break;
            case "Academic Record":
                SetVirtualButtonMaterial(VirtualButtonDefault, vb);
                Debug.Log(vb.VirtualButtonName);

                AcademicRecord.SetActive(false);
                StartCoroutine(DelayOnButtonReleasedEvent(ButtonReleaseTimeDelay));
                break;
            case "Show Registered Courses":
                SetVirtualButtonMaterial(VirtualButtonDefault, vb);
                Debug.Log(vb.VirtualButtonName);
                RegisteredCourses.SetActive(false);
                StartCoroutine(DelayOnButtonReleasedEvent(ButtonReleaseTimeDelay));
                break;
            default:
                break;
        }
        
        
    }

    void SetVirtualButtonMaterial(Material material, VirtualButtonBehaviour vb)
    {
        //for (var i = 0; i < MyVB.Length; ++i)
        //{
        if (material != null)
            vb.GetComponent<MeshRenderer>().sharedMaterial = material;
        //}
    }

    IEnumerator DelayOnButtonReleasedEvent(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        BroadcastMessage("HandleVirtualButtonReleased", SendMessageOptions.DontRequireReceiver);
    }
}

