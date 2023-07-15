using Infrastructure.Event;

namespace UI.Event
{
    public class SlotMachineRollRequestedEvent : IEvent
    {
        private SlotMachineRollRequestedEvent()
        {
        }

        public static SlotMachineRollRequestedEvent Create()
        {
            return new SlotMachineRollRequestedEvent();
        }
    }
}