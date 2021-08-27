// GENERATED AUTOMATICALLY FROM 'Assets/Controller.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @Controller : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @Controller()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Controller"",
    ""maps"": [
        {
            ""name"": ""MainMap"",
            ""id"": ""6766b87b-8d58-4161-ab33-b0ae60aee905"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""a92f6d45-ae41-41d0-a6c7-a607b24176f2"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Look Delta"",
                    ""type"": ""Value"",
                    ""id"": ""51793fee-6604-42eb-9ba2-75e900fa5dfe"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""f83676e0-36bc-4f57-bffe-978ad27d6992"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""ab2fbe20-55e3-43e4-8c40-44659126b1a0"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""066beea7-9031-4998-b6b1-4f6425f83cc1"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""f81ec130-c30b-4670-b326-b47c4083bdcd"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""a93d1642-754d-4d05-af9b-a59cb4d6a791"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""75335241-e998-4810-a526-25318c14a88f"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Look Delta"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // MainMap
        m_MainMap = asset.FindActionMap("MainMap", throwIfNotFound: true);
        m_MainMap_Movement = m_MainMap.FindAction("Movement", throwIfNotFound: true);
        m_MainMap_LookDelta = m_MainMap.FindAction("Look Delta", throwIfNotFound: true);
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

    // MainMap
    private readonly InputActionMap m_MainMap;
    private IMainMapActions m_MainMapActionsCallbackInterface;
    private readonly InputAction m_MainMap_Movement;
    private readonly InputAction m_MainMap_LookDelta;
    public struct MainMapActions
    {
        private @Controller m_Wrapper;
        public MainMapActions(@Controller wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_MainMap_Movement;
        public InputAction @LookDelta => m_Wrapper.m_MainMap_LookDelta;
        public InputActionMap Get() { return m_Wrapper.m_MainMap; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MainMapActions set) { return set.Get(); }
        public void SetCallbacks(IMainMapActions instance)
        {
            if (m_Wrapper.m_MainMapActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_MainMapActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_MainMapActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_MainMapActionsCallbackInterface.OnMovement;
                @LookDelta.started -= m_Wrapper.m_MainMapActionsCallbackInterface.OnLookDelta;
                @LookDelta.performed -= m_Wrapper.m_MainMapActionsCallbackInterface.OnLookDelta;
                @LookDelta.canceled -= m_Wrapper.m_MainMapActionsCallbackInterface.OnLookDelta;
            }
            m_Wrapper.m_MainMapActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @LookDelta.started += instance.OnLookDelta;
                @LookDelta.performed += instance.OnLookDelta;
                @LookDelta.canceled += instance.OnLookDelta;
            }
        }
    }
    public MainMapActions @MainMap => new MainMapActions(this);
    public interface IMainMapActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnLookDelta(InputAction.CallbackContext context);
    }
}
