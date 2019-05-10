/*
 *  Copyright 2014 Jonathan Bradshaw. All rights reserved.
 *  Redistribution and use in source and binary forms, with or without modification, is permitted.
 */

using System;

namespace stellar_dotnet_sdk
{
    public sealed partial class EventSource
    {
        /// <summary>
        ///     Server Sent Error Event Object
        /// </summary>
        public sealed class ServerSentErrorEventArgs : EventArgs
        {
            /// <summary>
            /// Internal Exception
            /// </summary>
            public Exception Exception { get; internal set; }
        }
    }
}