using Infrastructure.Event;
using UI.Event;
using UnityEngine;

namespace UI.SlotMachine
{
    public class SlotMachineRollButtonView : MonoBehaviour
    {
        #region Unity handlers

        public void _OnClicked()
        {
            EventManager.Send(SlotMachineRollRequestedEvent.Create());
        }

        #endregion
        
    }
}