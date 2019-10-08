/*
 *  Copyright 2014 Jonathan Bradshaw. All rights reserved.
 *  Redistribution and use in source and binary forms, with or without modification, is permitted.
 */

namespace stellar_dotnet_sdk
{
    public sealed partial class EventSource
    {
        /// <summary>
        ///     The possible values of the readyState property.
        /// </summary>
        public enum EventSourceState
        {
            Connecting = 0,
            Open = 1,
            Closed = 2,
            Shutdown = 3,
            Raw = 4
        }
    }
}