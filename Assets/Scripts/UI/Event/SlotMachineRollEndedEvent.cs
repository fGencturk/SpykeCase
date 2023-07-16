using Core.Data;
using Infrastructure.Event;

namespace UI.Event
{
    public class SlotMachineRollEndedEvent : IEvent
    {
        #region Properties
        
        public SlotCombinationConfig SlotCombinationConfig { get; }

        #endregion

        private SlotMachineRollEndedEvent(SlotCombinationConfig slotCombinationConfig)
        {
            SlotCombinationConfig = slotCombinationConfig;
        }

        public static SlotMachineRollEndedEvent Create(SlotCombinationConfig combinationConfig)
        {
            return new SlotMachineRollEndedEvent(combinationConfig);
        }
    }
}