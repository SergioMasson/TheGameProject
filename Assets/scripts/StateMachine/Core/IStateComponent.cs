﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheGame.StateMachine
{
    internal interface IStateComponent
    {
        /// <summary>
        /// Called when entering the state.
        /// </summary>
        void OnStateEnter();

        /// <summary>
        /// Called when leaving the state.
        /// </summary>
        void OnStateExit();
    }
}