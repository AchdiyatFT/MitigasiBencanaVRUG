using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class DoorHandle : MonoBehaviour
{
    public Transform handler; // Handle pintu
    public Rigidbody doorRigidbody; // Rigidbody untuk pintu
    private XRGrabInteractable grabInteractable;

    private Vector3 initialPosition;
    private Quaternion initialRotation;

    void Start()
    {
        // Simpan posisi dan rotasi awal
        initialPosition = transform.position;
        initialRotation = transform.rotation;

        // Pastikan ada komponen XRGrabInteractable
        grabInteractable = GetComponent<XRGrabInteractable>();
        if (grabInteractable == null)
        {
            grabInteractable = gameObject.AddComponent<XRGrabInteractable>();
        }

        // Subscribe ke event Grab dan Release
        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        // Disable Rigidbody untuk memastikan handle tidak terpengaruh physics saat dipegang
       // if (doorRigidbody != null)
       // {
        //    doorRigidbody.isKinematic = true;
        //}
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        // Reset posisi handle ke posisi handler (jika ingin handle selalu kembali ke posisi awal)
        transform.position = handler.position;
        transform.rotation = handler.rotation;

        // Aktifkan kembali Rigidbody pintu
        //if (doorRigidbody != null)
       // {
        //    doorRigidbody.isKinematic = false;
       // }
    }

    void OnDestroy()
    {
        // Unsubscribe dari event untuk mencegah error
        grabInteractable.selectEntered.RemoveListener(OnGrab);
        grabInteractable.selectExited.RemoveListener(OnRelease);
    }
}
