using Infrastructure.Event;
using UI.Event;
using UnityEngine;

namespace Particle
{
    public class CoinThrowParticleController : MonoBehaviour
    {
        #region Inspector

        [SerializeField] private ParticleSystem _CoinParticle;

        #endregion

        #region Unity event functions

        private void OnEnable()
        {
            EventManager.Register<SlotMachineRollEndedEvent>(OnRollEnded);
        }
        
        private void OnDisable()
        {
            EventManager.Unregister<SlotMachineRollEndedEvent>(OnRollEnded);
        }

        #endregion

        #region Handlers
        
        private void OnRollEnded(SlotMachineRollEndedEvent data)
        {
            if (data.SlotCombinationConfig.ThrownCoinAmount <= 0)
            {
                return;
            }

            var emission = _CoinParticle.emission;
            var burst = emission.GetBurst(0);
            burst.minCount = data.SlotCombinationConfig.ThrownCoinAmount;
            burst.maxCount = data.SlotCombinationConfig.ThrownCoinAmount;
            emission.SetBurst(0, burst);

            _CoinParticle.Play();
        }

        #endregion
        
    }
}