using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class OpenDoor : MonoBehaviour
{
    [SerializeField] UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor keySocket;
    [SerializeField] bool isLocked;
    [SerializeField] Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        if (keySocket != null)
        {
            keySocket.selectEntered.AddListener(OnUnclocked);
            keySocket.selectExited.AddListener(OnLocked);
        }
    }

    private void OnUnclocked(SelectEnterEventArgs arg0)
    {
        isLocked = false;
        Debug.Log("Terbuka");
    }

    private void OnLocked(SelectExitEventArgs arg0)
    {
        isLocked = true;
        Debug.Log("Tertutup");
    }
    // Update is called once per frame
    void Update()
    {
        if (!isLocked)
        {
            animator.SetBool("Open", true);
        }
        else if (isLocked)
        {
            animator.SetBool("Open", false);
        }
    }
}
