﻿using CM.Text.BusinessMessaging;
using FluentAssertions;

namespace CM.Text.NET6.Tests
{
    [TestClass]
    public class BusinessMessagingApiTests
    {
        [TestMethod]
        public void TestPostBody()
        {
            var guid = Guid.NewGuid();
            var message = "This is a unit test";
            var sender = "CM.com";
            var reference = "ReferenceForMeToFind";
            var number1 = "0031612345678";
            var number2 = "0031612345679";

            var data = BusinessMessagingApi.GetHttpPostBody(guid, message, sender,
                new[] {number1, number2}, reference);

            data.Should().NotBeNull();
            //Simple to check if all values survived our logic
            data.Should().Contain(guid.ToString());
            data.Should().Contain(message);
            data.Should().Contain(sender);
            data.Should().Contain(reference);
            data.Should().Contain(number1);
            data.Should().Contain(number2);
        }


        [TestMethod]
        public void TestResponseBody()
        {
            var guid = Guid.NewGuid();
            var message = "{\r\n    \"details\": \"Created 1 message(s)\",\r\n    \"errorCode\": 0,\r\n    \"messages\": [\r\n        {\r\n            \"to\": \"0031612345678\",\r\n            \"status\": \"Accepted\",\r\n            \"reference\": \"test-reference-1\",\r\n            \"parts\": 1,\r\n            \"messageDetails\": null,\r\n            \"messageErrorCode\": 0\r\n        }\r\n    ]\r\n}";

            var data = BusinessMessagingApi.GetTextApiResult(message);

            data.Should().NotBeNull();
            //Simple to check if all values survived our logic
            data.details.Should().NotBeNull();
            data.details.First().reference.Should().Be("test-reference-1");
            data.details.First().status.Should().Be("Accepted");
            data.details.First().to.Should().Be("0031612345678");
            data.details.First().parts.Should().Be(1);
            data.details.First().details.Should().BeNull();
        }
    }
}