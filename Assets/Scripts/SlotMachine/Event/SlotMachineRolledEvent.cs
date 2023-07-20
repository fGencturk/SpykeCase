using Core.Data;
using Infrastructure.Event;

namespace SlotMachine.Event
{
    public struct SlotMachineRolledEvent : IEvent
    {
        #region Properties

        public SlotCombinationConfig SelectedCombination { get; }

        #endregion

        private SlotMachineRolledEvent(SlotCombinationConfig slotCombinationConfig)
        {
            SelectedCombination = slotCombinationConfig;
        }

        public static SlotMachineRolledEvent Create(SlotCombinationConfig slotCombinationConfig)
        {
            return new SlotMachineRolledEvent(slotCombinationConfig);
        }
    }
}