using Core.Data;
using Infrastructure.Event;

namespace UI.Event
{
    public struct SlotMachineRollStartedEvent : IEvent
    {
        #region Properties
        
        public SlotCombinationConfig SlotCombinationConfig { get; }

        #endregion

        private SlotMachineRollStartedEvent(SlotCombinationConfig slotCombinationConfig)
        {
            SlotCombinationConfig = slotCombinationConfig;
        }

        public static SlotMachineRollStartedEvent Create(SlotCombinationConfig combinationConfig)
        {
            return new SlotMachineRollStartedEvent(combinationConfig);
        }
    }
}