﻿using MediatR;

namespace Inventory_Management.Common
{
    public class ControllereParameters
    {
        public IMediator Mediator { get; set; }
        public UserState UserState { get; set; }

        public ControllereParameters(IMediator mediator, UserState userState)
        {
            Mediator = mediator;
            UserState = userState;
        }
    }
}
