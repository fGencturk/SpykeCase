using System;
using Infrastructure.Event;
using UI.Event;
using UnityEngine;
using UnityEngine.UI;

namespace UI.SlotMachine
{
    [RequireComponent(typeof(Button))]
    public class SlotMachineRollButtonView : MonoBehaviour
    {
        private Button _button;

        #region Unity event functions

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            EventManager.Register<SlotMachineRollStartedEvent>(OnRollStarted);
            EventManager.Register<SlotMachineRollEndedEvent>(OnRollEnded);
        }

        private void OnDisable()
        {
            EventManager.Unregister<SlotMachineRollStartedEvent>(OnRollStarted);
            EventManager.Unregister<SlotMachineRollEndedEvent>(OnRollEnded);
        }

        #endregion

        #region Handlers
        
        private void OnRollStarted(SlotMachineRollStartedEvent data)
        {
            _button.interactable = false;
        }

        private void OnRollEnded(SlotMachineRollEndedEvent data)
        {
            _button.interactable = true;
        }

        #endregion
        
        #region Unity handlers

        public void _OnClicked()
        {
            EventManager.Send(SlotMachineRollRequestedEvent.Create());
        }

        #endregion
        
    }
}