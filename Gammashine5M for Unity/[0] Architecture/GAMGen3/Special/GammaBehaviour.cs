//using System.Collections.Generic;

//using UnityEngine;
//using UnityEngine.LowLevel;
//using UnityEngine.PlayerLoop;

//namespace Snaplight
//{
//    public class GammaBehaviour : IModulable
//    {
//        private static readonly HashSet<IAutomateCollectibles> _collection = new();
//        private static readonly HashSet<IAutomatePlayable> _playback = new();
//        private static readonly HashSet<IAutomateFixation> _fixation = new();
//        private static readonly HashSet<IAutomateAfterable> _aftereffect = new();

//        public void Collection()
//        {
//            PlayerLoopSystem defaultLoopSystems = PlayerLoop.GetDefaultPlayerLoop();

//            PlayerLoopSystem collection = new()
//            {
//                subSystemList = null,
//                updateDelegate = Collected,
//                type = typeof(IAutomateCollectibles),  
//            };

//            PlayerLoopSystem playback = new()
//            {
//                subSystemList = null,
//                updateDelegate = Played,
//                type = typeof(IAutomatePlayable),
//            };

//            PlayerLoopSystem fixation = new()
//            {
//                subSystemList = null,
//                updateDelegate = Fixed,
//                type = typeof(IAutomateFixation),
//            };

//            PlayerLoopSystem aftereffect = new()
//            {
//                subSystemList = null,
//                updateDelegate = After,
//                type = typeof(IAutomateAfterable),
//            };

//            PlayerLoopSystem loopCollection = CollectionSystem<Initialization>(in defaultLoopSystems, collection);
//            PlayerLoopSystem loopPlayback = CollectionSystem<Update>(in loopCollection, playback);
//            PlayerLoopSystem loopFixation = CollectionSystem<FixedUpdate>(in loopPlayback, fixation);
//            PlayerLoopSystem loopAftereffect = CollectionSystem<PostLateUpdate>(in loopFixation, aftereffect);
//            PlayerLoop.SetPlayerLoop(loopAftereffect);
//        }

//        public void Elimination()
//        {
            
//        }

//        //===
//        [RuntimeInitializeOnLoadMethod]
//        private static void Collected()
//        {
//            using HashSet<IAutomateCollectibles>.Enumerator enumerator = _collection.GetEnumerator();
//            while (enumerator.MoveNext()) enumerator.Current?.Collection();
//        }

//        [RuntimeInitializeOnLoadMethod]
//        private static void Played()
//        {
//            using HashSet<IAutomatePlayable>.Enumerator enumerator = _playback.GetEnumerator();
//            while (enumerator.MoveNext()) enumerator.Current?.Playback();
//        }

//        [RuntimeInitializeOnLoadMethod]
//        private static void Fixed()
//        {
//            using HashSet<IAutomateFixation>.Enumerator enumerator = _fixation.GetEnumerator();
//            while (enumerator.MoveNext()) enumerator.Current?.Fixation();
//        }

//        [RuntimeInitializeOnLoadMethod]
//        private static void After()
//        {
//            using HashSet<IAutomateAfterable>.Enumerator enumerator = _aftereffect.GetEnumerator();
//            while (enumerator.MoveNext()) enumerator.Current?.Aftereffect();
//        }

//        //===
//        public static PlayerLoopSystem CollectionSystem<T>(in PlayerLoopSystem loopSystem, PlayerLoopSystem systemToCollection) 
//            where T : struct
//        {
//            PlayerLoopSystem newPlayerLoop = new()
//            {
//                loopConditionFunction = loopSystem.loopConditionFunction,
//                type = loopSystem.type,
//                updateDelegate = loopSystem.updateDelegate,
//                updateFunction = loopSystem.updateFunction
//            };

//            List<PlayerLoopSystem> newSubSystemList = new();

//            foreach (PlayerLoopSystem subSystem in loopSystem.subSystemList)
//            {
//                newSubSystemList.Add(subSystem);

//                if (subSystem.type == typeof(T)) newSubSystemList.Add(systemToCollection);
//            }

//            newPlayerLoop.subSystemList = newSubSystemList.ToArray();
//            return newPlayerLoop;
//        }
//    }
//}
