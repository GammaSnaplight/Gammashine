using Gammashine;
using Gammashine.Automachinery;
using Gammashine.Manifolds;

using Project.Gammashine.Fold;

using System;
using System.Collections.Generic;

using UnityEngine;

namespace Gammashine.Modules.Masterminds
{
    /// <summary>
    /// 0_Supremind - RulecoreSupremind является системой, которая проигрывает другие системы внутри себя
    /// используя автоматизацию где это возможно и помогает использовать фишки архитектуры для повышения производительности
    /// </summary>
    public class RulecoreSupremind : MonoBehaviour, IMasterable<IManifoldable<IModulable>>
    {
        // IMasterable
        public RulecoreManifold Rulecorector = new();
        public IManifoldable<IModulable> Manifold { get => Rulecorector; private set => Rulecorector = (RulecoreManifold)value; }

        public void Collection()
        {
            EventfulAutomachine.Subscribe<MastermindEventful>(MastermindEventfulCallback);
        }

        public void Playback()
        {
            
        }

        public void Elimination()
        {
            
        }

        public void MastermindEventfulCallback(MastermindEventful eventful)
        {
            Rulecorector.RulecorePlayback.Masterminds.Add(eventful.Mastermind);
            Rulecorector.RulecorePlayback.Collection();
        }

        private void OnEnable()
        {

        }

        private void OnDisable()
        {
            
        }

        private void OnDestroy()
        {
            Elimination();

            EventfulAutomachine.AllUnsubscribe();
            PathfindAutomachine.Clearfull();
        }

        private void Awake()
        {
            Collection();
        }

        private void Start()
        {

        }

        private void Update()
        {
            Rulecorector.RulecorePlayback.UpdateControllable = UpdateControllable.Update;

            Rulecorector.RulecorePlayback.Playback();
        }

        private void FixedUpdate()
        {
            Rulecorector.RulecorePlayback.UpdateControllable = UpdateControllable.Fixed;

            Rulecorector.RulecorePlayback.Playback();
        }

        private void LateUpdate()
        {
            Rulecorector.RulecorePlayback.UpdateControllable = UpdateControllable.Late;

            Rulecorector.RulecorePlayback.Playback();
        }
    }
}
