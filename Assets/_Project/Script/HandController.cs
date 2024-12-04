using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Readers;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
public class HandController : MonoBehaviour
{
    [Header("Animator")]
    [SerializeField]
    private Animator m_Animator;

    [Header("Trigger")]
    [SerializeField]
    private string m_TriggerParameter = "Trigger";

    [SerializeField]
    private XRInputValueReader<float> m_TriggerInput = new XRInputValueReader<float>("Trigger");

    [Header("Grip")]
    [SerializeField]
    private string m_GripParameter = "Grip";

    [SerializeField]
    private XRInputValueReader<float> m_GripInput = new XRInputValueReader<float>("Grip");

    [Header("GrabObject")]
    [SerializeField] private NearFarInteractor grabbable;
    private string m_ObjectParameter = "HasObject";
    private string m_FlashLightParameter = "HasFlashLight";
    private void OnEnable()
    {
        if (m_Animator == null)
        {
            enabled = false;
            Debug.LogWarning($"Animator is not assigned on {gameObject.name}", this);
            return;
        }

        m_TriggerInput?.EnableDirectActionIfModeUsed();
        m_GripInput?.EnableDirectActionIfModeUsed();
    }

    private void OnDisable()
    {
        m_TriggerInput?.DisableDirectActionIfModeUsed();
        m_GripInput?.DisableDirectActionIfModeUsed();
    }

    private void Update()
    {
        if (m_TriggerInput != null)
        {
            float triggerValue = m_TriggerInput.ReadValue();
            m_Animator.SetFloat(m_TriggerParameter, triggerValue);
        }

        if (m_GripInput != null)
        {
            float gripValue = m_GripInput.ReadValue();
            m_Animator.SetFloat(m_GripParameter, gripValue);
        }

        // Check if the grabbable interactor has a selected object
        if (grabbable.hasSelection)
        {
            m_Animator.SetBool(m_ObjectParameter, true);
        }
        else
        {
            m_Animator.SetBool(m_ObjectParameter, false);
        }
    }
}
