namespace Infrastructure.Save
{
    public interface IPlayerData
    {
        int CurrentRollIndex { get; set; }
        int GetLastHitIndex(int slotIndex);
        void SetLastHitIndex(int slotIndex, int lastHitIndex);
        void Clear(int slotCount);
    }
}