// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.CodeDom;
using System.Collections;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Drawing.Design;
using Xunit;

namespace System.Windows.Forms.Design.Tests.Serialization
{
    public class SerializableTypesTests
    {
        [Fact]
        public void ExceptionCollection_RoundTrip()
        {
            var exceptions = new ArrayList(1);
            exceptions.Add(new Exception("Oops!"));
            var exceptionCollection = new ExceptionCollection(exceptions);

            var blob = BinarySerialization.ToBase64String(exceptionCollection);
            var result = BinarySerialization.EnsureDeserialize(blob) as ExceptionCollection;

            Assert.NotNull(result);
            var exception = Assert.Single(result.Exceptions) as Exception;
            Assert.NotNull(exception);
            Assert.Equal("Oops!", exception.Message);
        }

        [Fact(Skip = "System.CodeDom.CodeLinePragma is not serializable")]
        public void CodeDomSerializerException_RoundTrip()
        {
            var exception = new CodeDomSerializerException(
                "Oops!", 
                new CodeLinePragma("fileName.cs", 11));

            var blob = BinarySerialization.ToBase64String(exception);
            var result = BinarySerialization.EnsureDeserialize(blob) as CodeDomSerializerException;

            Assert.NotNull(result);
            Assert.NotNull(result.LinePragma);
            var pragma = result.LinePragma;
            Assert.Equal("fileName.cs", pragma.FileName);
            Assert.Equal(11, pragma.LineNumber);
            Assert.Equal("Oops!", exception.Message);
        }

        [Fact]
        public void CodeDomSerializationStore_RoundTrip()
        {
            var service = new CodeDomComponentSerializationService();
            var store = service.CreateStore();
            var blob = BinarySerialization.ToBase64String(store);

            var result = BinarySerialization.EnsureDeserialize(blob) as SerializationStore;
            Assert.NotNull(result);
        }
    }
}
