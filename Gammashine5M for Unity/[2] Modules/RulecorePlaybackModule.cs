using Gammashine.Folds;

using Snaplight.Folds;

using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace Gammashine.Modules
{
    [Serializable]
    public class RulecorePlaybackModule : IPlayableModulable
    {
        // Serializable
        [HideInInspector] public List<IMasterable<IManifoldable<IModulable>>> Masterminds = new();
        [HideInInspector] public UpdateControllable UpdateControllable;

        private List<ModulesManifold<IMainstreamModulable>> _modulesFold = new();
        private readonly List<IMainstreamModulable> _sortedBuffer = new();
        private static readonly Comparison<IMainstreamModulable> _comparison = CompareBySubstage;

        private int _maxFoldSize;

        public void Collection()
        {
            _modulesFold.Clear();

            //---
            ModulesManifold<IMainstreamModulable> updateModules = new()
            {
                Controllable = UpdateControllable.Update,
            };
            ModulesManifold<IMainstreamModulable> fixedModules = new()
            {
                Controllable = UpdateControllable.Fixed,
            };
            ModulesManifold<IMainstreamModulable> lateModules = new()
            {
                Controllable = UpdateControllable.Late,
            };

            //---
            foreach (IMasterable<IManifoldable<IModulable>> mind in Masterminds)
            {
                mind.Collection();
                foreach (IModulable module in mind.Manifold.Modules)
                {
                    if (module is IMainstreamModulable imm)
                    {
                        if (imm.Changeover.Updating == UpdateControllable.Update) updateModules.Modules.Add(imm);
                        else if (imm.Changeover.Updating == UpdateControllable.Fixed) fixedModules.Modules.Add(imm);
                        else if (imm.Changeover.Updating == UpdateControllable.Late) lateModules.Modules.Add(imm);
                    }
                }
            }

            //---
            _modulesFold.Add(updateModules);
            _modulesFold.Add(fixedModules);
            _modulesFold.Add(lateModules);

            //---
            _maxFoldSize = 0;
            foreach (ModulesManifold<IMainstreamModulable> fold in _modulesFold)
            {
                if (fold.Modules.Count > _maxFoldSize)
                    _maxFoldSize = fold.Modules.Count;
            }

            if (_sortedBuffer.Capacity < _maxFoldSize)
                _sortedBuffer.Capacity = _maxFoldSize;
        }

        public void Playback()
        {
            foreach (ModulesManifold<IMainstreamModulable> fold in _modulesFold)
            {
                if (fold.Controllable != UpdateControllable) continue;

                List<IMainstreamModulable> source = fold.Modules;
                int count = source.Count;

                //---
                _sortedBuffer.Clear();
                for (int i = 0; i < count; i++)
                {
                    if (i >= _sortedBuffer.Count)
                        _sortedBuffer.Add(source[i]);
                    else
                        _sortedBuffer[i] = source[i];
                }

                if (_sortedBuffer.Count > count)
                    _sortedBuffer.RemoveRange(count, _sortedBuffer.Count - count);

                _sortedBuffer.Sort(_comparison);

                //---
                for (int i = 0; i < _sortedBuffer.Count; i++)
                {
                    IMainstreamModulable module = _sortedBuffer[i];
                    switch (module.Changeover.Undertaking)
                    {
                        case ModuleUndertaking.Finishes:
                            return;
                        case ModuleUndertaking.Playback:
                            module.Playback();
                            break;
                        case ModuleUndertaking.Lightweight:
                            module.Lightback();
                            break;
                        case ModuleUndertaking.Elimination:
                            module.Elimination();
                            break;
                        case ModuleUndertaking.Shutdown:
                            module.Shutdown();
                            break;
                    }
                }
            }
        }

        private static int CompareBySubstage(IMainstreamModulable a, IMainstreamModulable b)
        {
            uint aStage = (a is ISubstagable sa) ? sa.Substage : 0;
            uint bStage = (b is ISubstagable sb) ? sb.Substage : 0;
            return aStage.CompareTo(bStage);
        }

        public void Elimination()
        {
            foreach (IMasterable<IManifoldable<IModulable>> mind in Masterminds) mind.Elimination();
        }
    }
}