using System;

namespace Chat.Bot
{
    class Program
    {
        static void Main(string[] args)
        {
            var consumer = new Consumer();
            consumer.Consume();
        }
    }
}
