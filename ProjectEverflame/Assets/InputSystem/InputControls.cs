//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.11.0
//     from Assets/InputSystem/InputControls.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @InputControls: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputControls"",
    ""maps"": [
        {
            ""name"": ""Play"",
            ""id"": ""a310a24b-b77a-4bdb-9709-ed8ba38c4c5a"",
            ""actions"": [
                {
                    ""name"": ""UseAbility"",
                    ""type"": ""Button"",
                    ""id"": ""340d83fc-8b99-4e77-8b28-c5e7b155abf9"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SwitchAbility"",
                    ""type"": ""Value"",
                    ""id"": ""756b04b3-ad70-4173-aed8-f3b7c67636b1"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""a05b84dd-869e-4327-9d65-232289b538d7"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""UseAbility"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9f711663-53d1-440a-a3e5-dfcbbc5c2936"",
                    ""path"": ""<Mouse>/scroll"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SwitchAbility"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Play
        m_Play = asset.FindActionMap("Play", throwIfNotFound: true);
        m_Play_UseAbility = m_Play.FindAction("UseAbility", throwIfNotFound: true);
        m_Play_SwitchAbility = m_Play.FindAction("SwitchAbility", throwIfNotFound: true);
    }

    ~@InputControls()
    {
        UnityEngine.Debug.Assert(!m_Play.enabled, "This will cause a leak and performance issues, InputControls.Play.Disable() has not been called.");
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Play
    private readonly InputActionMap m_Play;
    private List<IPlayActions> m_PlayActionsCallbackInterfaces = new List<IPlayActions>();
    private readonly InputAction m_Play_UseAbility;
    private readonly InputAction m_Play_SwitchAbility;
    public struct PlayActions
    {
        private @InputControls m_Wrapper;
        public PlayActions(@InputControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @UseAbility => m_Wrapper.m_Play_UseAbility;
        public InputAction @SwitchAbility => m_Wrapper.m_Play_SwitchAbility;
        public InputActionMap Get() { return m_Wrapper.m_Play; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayActions set) { return set.Get(); }
        public void AddCallbacks(IPlayActions instance)
        {
            if (instance == null || m_Wrapper.m_PlayActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_PlayActionsCallbackInterfaces.Add(instance);
            @UseAbility.started += instance.OnUseAbility;
            @UseAbility.performed += instance.OnUseAbility;
            @UseAbility.canceled += instance.OnUseAbility;
            @SwitchAbility.started += instance.OnSwitchAbility;
            @SwitchAbility.performed += instance.OnSwitchAbility;
            @SwitchAbility.canceled += instance.OnSwitchAbility;
        }

        private void UnregisterCallbacks(IPlayActions instance)
        {
            @UseAbility.started -= instance.OnUseAbility;
            @UseAbility.performed -= instance.OnUseAbility;
            @UseAbility.canceled -= instance.OnUseAbility;
            @SwitchAbility.started -= instance.OnSwitchAbility;
            @SwitchAbility.performed -= instance.OnSwitchAbility;
            @SwitchAbility.canceled -= instance.OnSwitchAbility;
        }

        public void RemoveCallbacks(IPlayActions instance)
        {
            if (m_Wrapper.m_PlayActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IPlayActions instance)
        {
            foreach (var item in m_Wrapper.m_PlayActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_PlayActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public PlayActions @Play => new PlayActions(this);
    public interface IPlayActions
    {
        void OnUseAbility(InputAction.CallbackContext context);
        void OnSwitchAbility(InputAction.CallbackContext context);
    }
}