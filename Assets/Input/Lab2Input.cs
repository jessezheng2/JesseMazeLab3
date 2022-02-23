//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.2.0
//     from Assets/Input/Lab2Input.inputactions
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

namespace ZhengJesse.Input
{
    public partial class @Lab2Input : IInputActionCollection2, IDisposable
    {
        public InputActionAsset asset { get; }
        public @Lab2Input()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""Lab2Input"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""04907eab-4774-4454-ae68-917cea7bd531"",
            ""actions"": [
                {
                    ""name"": ""Quit"",
                    ""type"": ""Button"",
                    ""id"": ""7adac7b9-b8c3-4dd8-bd93-d29dee8f81be"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""CameraSwitch"",
                    ""type"": ""Value"",
                    ""id"": ""025795ac-0172-443d-8862-580f178d5bd1"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""RotateMaze"",
                    ""type"": ""Button"",
                    ""id"": ""42e4287d-be55-4fbd-9044-3026425d5945"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""be406919-31db-44b7-9f62-379d50cb8278"",
                    ""path"": ""*/{Menu}"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Quit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f4c6b1e1-4de6-47a5-b873-331387abbc7a"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Quit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a8657504-f6c6-4a5d-85db-e9ada01f0244"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraSwitch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d41f8fb3-1002-4ebf-8474-a91980feddfc"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraSwitch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1ee99f1a-c37a-4a84-9550-b16bb772bd6c"",
                    ""path"": ""<Keyboard>/0"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotateMaze"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
            // Player
            m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
            m_Player_Quit = m_Player.FindAction("Quit", throwIfNotFound: true);
            m_Player_CameraSwitch = m_Player.FindAction("CameraSwitch", throwIfNotFound: true);
            m_Player_RotateMaze = m_Player.FindAction("RotateMaze", throwIfNotFound: true);
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

        // Player
        private readonly InputActionMap m_Player;
        private IPlayerActions m_PlayerActionsCallbackInterface;
        private readonly InputAction m_Player_Quit;
        private readonly InputAction m_Player_CameraSwitch;
        private readonly InputAction m_Player_RotateMaze;
        public struct PlayerActions
        {
            private @Lab2Input m_Wrapper;
            public PlayerActions(@Lab2Input wrapper) { m_Wrapper = wrapper; }
            public InputAction @Quit => m_Wrapper.m_Player_Quit;
            public InputAction @CameraSwitch => m_Wrapper.m_Player_CameraSwitch;
            public InputAction @RotateMaze => m_Wrapper.m_Player_RotateMaze;
            public InputActionMap Get() { return m_Wrapper.m_Player; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
            public void SetCallbacks(IPlayerActions instance)
            {
                if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
                {
                    @Quit.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnQuit;
                    @Quit.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnQuit;
                    @Quit.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnQuit;
                    @CameraSwitch.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCameraSwitch;
                    @CameraSwitch.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCameraSwitch;
                    @CameraSwitch.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCameraSwitch;
                    @RotateMaze.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRotateMaze;
                    @RotateMaze.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRotateMaze;
                    @RotateMaze.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRotateMaze;
                }
                m_Wrapper.m_PlayerActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Quit.started += instance.OnQuit;
                    @Quit.performed += instance.OnQuit;
                    @Quit.canceled += instance.OnQuit;
                    @CameraSwitch.started += instance.OnCameraSwitch;
                    @CameraSwitch.performed += instance.OnCameraSwitch;
                    @CameraSwitch.canceled += instance.OnCameraSwitch;
                    @RotateMaze.started += instance.OnRotateMaze;
                    @RotateMaze.performed += instance.OnRotateMaze;
                    @RotateMaze.canceled += instance.OnRotateMaze;
                }
            }
        }
        public PlayerActions @Player => new PlayerActions(this);
        public interface IPlayerActions
        {
            void OnQuit(InputAction.CallbackContext context);
            void OnCameraSwitch(InputAction.CallbackContext context);
            void OnRotateMaze(InputAction.CallbackContext context);
        }
    }
}
