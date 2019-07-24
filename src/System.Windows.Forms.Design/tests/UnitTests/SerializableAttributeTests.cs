// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Drawing.Design;
using Xunit;

namespace System.Windows.Forms.Design.Tests.Serialization
{
    public class SerializableAttributeTests
    {
        [Fact]
        public void EnsureSerializableAttribute()
        {
            BinarySerialization.EnsureSerializableAttribute(
                typeof(ToolboxItem).Assembly, 
                new List<string>
                {
                     // Following classes are participating in resx serialization scenarios.
                    { typeof(ExceptionCollection).FullName},
                    { "System.ComponentModel.Design.Serialization.CodeDomComponentSerializationService+CodeDomSerializationStore"}, // This type is private.
                    { typeof(CodeDomSerializerException).FullName},
                    { typeof(ToolboxItem).FullName},
                    { "System.Windows.Forms.Design.Behavior.DesignerActionKeyboardBehavior+<>c"}
                });
        }
    }
}
