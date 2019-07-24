// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections;
using System.Runtime.Serialization;

namespace System.ComponentModel.Design
{
    [Serializable]  // Exceptions should exchange successfully between the classic and Core frameworks.
    public sealed class ExceptionCollection : Exception
    {
#pragma warning disable IDE1006
        private readonly ArrayList exceptions; // Do NOT rename (binary serialization).
#pragma warning restore IDE1006

        public ExceptionCollection(ArrayList exceptions)
        {
            this.exceptions = exceptions;
        }

        private ExceptionCollection(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            exceptions = (ArrayList)info.GetValue(nameof(exceptions), typeof(ArrayList));
        }

        public ArrayList Exceptions => (ArrayList)exceptions?.Clone();

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            info.AddValue(nameof(exceptions), exceptions);
            base.GetObjectData(info, context);
        }
    }
}
