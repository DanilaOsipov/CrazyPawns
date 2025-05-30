﻿using CrazyPawn.Gameplay.Interactable;
using CrazyPawn.Services.Input;
using System;
using UnityEngine;
using Zenject;

namespace CrazyPawn.Services.Interaction
{
    public class InteractionService : ITickable
    {
        private readonly IInputService _inputService;
        private readonly Camera _camera;

        private IInteractable _interactableUnderMouse;

        public IInteractable InteractableUnderMouse
        {
            get => _interactableUnderMouse;
            private set
            {
                if (_interactableUnderMouse != value)
                    _interactableUnderMouse = value;
            }
        }

        public event Action<IInteractable> OnMouseOverInteractable = delegate { };

        public InteractionService(IInputService inputService, Camera camera)
        {
            _inputService = inputService;
            _camera = camera;
        }

        void ITickable.Tick()
        {
            var ray = _camera.ScreenPointToRay(_inputService.MousePosition);
            if (Physics.Raycast(ray, out var hitInfo))
            {
                var interactable = hitInfo.collider.GetComponentInParent<IInteractable>();
                if (interactable != null)
                {
                    InteractableUnderMouse = interactable;
                    OnMouseOverInteractable(interactable);
                    return;
                }
            }

            InteractableUnderMouse = null;
        }
    }
}