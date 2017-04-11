// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Runtime.InteropServices;

namespace Microsoft.AspNetCore.Server.Kestrel.Transport.Libuv.Internal.Networking
{
    public class UvRequest : UvMemory
    {
        private GCHandle _pin;

        protected UvRequest(ILibuvTrace logger) : base (logger)
        {
        }

        /// <summary>
        /// Called when this SafeHandle is Disposed (Request Death)  
        /// </summary>
        protected override bool ReleaseHandle()
        {
            DestroyMemory(handle);
            handle = IntPtr.Zero;
            _pin.Free();        // This is a noop if _pin was not allocated.  
            return true;
        }

        /// <summary>
        /// This does not mean pin in the .NET Sense, It just means it is references (it can't be collected).  
        /// </summary>
        public virtual void Pin()
        {
            if (!_pin.IsAllocated)
                _pin = GCHandle.Alloc(null, GCHandleType.Normal);
            _pin.Target = this;
        }

        public virtual void Unpin()
        {
            _pin.Target = null;
        }
    }
}

