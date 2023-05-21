// using UnityEngine;
// using Zenject;
//
// namespace Game.Level
// {
//     public class UnitViewFactory
//     {
//         [Inject] private UnitView.Factory _unitViewFactory;
//         //[Inject] private CustomGameObjectFactory _gameObjectFactory;
//         
//         public GameObject Create(GameObjectFactoryArgs args)
//         {
//             var unitArgs = (UnitFactoryArgs)args;
//             var go = _unitViewFactory.Create(unitArgs.Settings);
//             
//             //var go = _gameObjectFactory.Create(args);
//             go.transform.SetPositionAndRotation(unitArgs.Position, unitArgs.Rotation);
//             return go.gameObject;
//         }
//     }
// }