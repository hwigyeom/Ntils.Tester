using System;
using System.Collections.Generic;

namespace Ntils.Models
{
    public class FakeMessageStore
    {
        private static DateTime _startDateTime = new DateTime(2016, 08, 28);

        public static readonly IList<Message> FakeMessages = new List<Message>()
        {
            Message.CreateMessage("First message title", "First message text", _startDateTime),
            Message.CreateMessage("Second message title", "Second message text", _startDateTime.AddDays(1)),
            Message.CreateMessage("Third message title", "Third message text", _startDateTime.AddDays(2)),
            Message.CreateMessage("Fourth message title", "Fourth message text", _startDateTime.AddDays(3)),
            Message.CreateMessage("Fifth message title", "Fifth message text", _startDateTime.AddDays(4)),
            Message.CreateMessage("Sixth message title", "Sixth message text", _startDateTime.AddDays(5)),
        };
    }
}