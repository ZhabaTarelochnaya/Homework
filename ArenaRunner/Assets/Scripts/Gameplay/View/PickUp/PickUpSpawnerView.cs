using System;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace.Gameplay.Data.PickUp;
using UnityEngine;

namespace DefaultNamespace.Gameplay.View.PickUp
{
    public class PickUpSpawnerView : MonoBehaviour
    {
        PickUpSpawnerViewModel _viewModel;
        List<PickUpView> _pickUpViews;
        public void Bind(PickUpSpawnerViewModel viewModel)
        {
            _viewModel = viewModel;
            _viewModel.Spawned += OnSpawned;
            _viewModel.Destroyed += OnDestroyed;
        }
        void OnSpawned(GameObject prefab, PickUpViewModel pickUpViewModel)
        {
            var instance = Instantiate(prefab, pickUpViewModel.Position, Quaternion.identity);
            var pickUpView = instance.GetComponent<PickUpView>();
            pickUpView.Bind(pickUpViewModel);
        }
        void OnDestroyed(int ID)
        {
            var pickUpView = _pickUpViews.FirstOrDefault(p => p.ID == ID);
            Destroy(pickUpView.gameObject);
        }
        void OnDestroy()
        {
            _viewModel.Spawned -= OnSpawned;
            _viewModel.Destroyed -= OnDestroyed;
        }
    }
}