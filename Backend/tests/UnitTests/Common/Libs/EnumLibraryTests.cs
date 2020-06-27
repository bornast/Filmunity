using Common.Libs;
using FluentAssertions;
using NUnit.Framework;
using System;

namespace UnitTests.Common.Libs
{
    [TestFixture]
    public class EnumLibraryTests
    {
        [Test]
        public void GetIntValuesFromEnumType_TypeIsNull_ThrowException()
        {
            Assert.That(() => EnumLibrary.GetIntValuesFromEnumType(null), Throws.Exception.TypeOf<Exception>());
        }

        [Test]
        public void GetIntValuesFromEnumType_PassEnum_ReturnListOfIntegers()
        {
            var enumType = typeof(TestEnums);

            var result = EnumLibrary.GetIntValuesFromEnumType(enumType);

            result.Should().Contain(1);
            result.Should().Contain(2);
            result.Should().HaveCount(2);
        }

        [Test]
        public void GetIdAndNameDictionaryOfEnumType_TypeIsNull_ThrowException()
        {
            Assert.That(() => EnumLibrary.GetIdAndNameDictionaryOfEnumType(null), Throws.Exception.TypeOf<Exception>());
        }

        [Test]
        public void GetIdAndNameDictionaryOfEnumType_PassEnum_ReturnDictionary()
        {
            var enumType = typeof(TestEnums);

            var result = EnumLibrary.GetIdAndNameDictionaryOfEnumType(enumType);

            result.Should().ContainKey(1);
            result.Should().ContainKey(2);
            result.Should().ContainValue("Test1");
            result.Should().ContainValue("Test2");
            result.Should().HaveCount(2);
        }

    }

    public enum TestEnums
    {
        Test1 = 1,
        Test2 = 2
    }

}
