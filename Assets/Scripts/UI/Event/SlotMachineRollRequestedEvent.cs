using Infrastructure.Event;

namespace UI.Event
{
    public struct SlotMachineRollRequestedEvent : IEvent
    {
        public static SlotMachineRollRequestedEvent Create()
        {
            return new SlotMachineRollRequestedEvent();
        }
    }
}