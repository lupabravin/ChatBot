using Chat.CrossCutting.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace Chat.Bot
{
    public class Consumer : CrossCutting.Consumer
    {
        string _rabbitConnection;
        Producer _producer;
        HttpClient _httpClient;

        public Consumer(string rabbitConnection, Producer producer)
        {
            _rabbitConnection = rabbitConnection;
            _producer = producer;
            _httpClient = new HttpClient();
        }

        public void Consume(string targetQueue)
        {
            Consume<KeyValuePair<string,string>>(targetQueue, _rabbitConnection, result => {
                _producer.Produce(SearchStock(result), BotHelper.BOT_MESSAGES_QUEUE);
            });
        }

        private string SearchStock(KeyValuePair<string,string> keyValuePair)
        {
            var command = keyValuePair.Key;
            var parameter = keyValuePair.Value;

            try
            {
                var commandKeyValuePair = BotHelper.COMMANDS.FirstOrDefault(c => c.Key == command);
                var url = commandKeyValuePair.Value.Replace("#parameter", parameter);
                string response = _httpClient.GetStringAsync(url).Result;

                string[] csvRows = response.Split('\n');
                List<string> cells = csvRows[1].Split(",").ToList();
                string stock = cells[0];
                string price = cells[cells.Count - 2];

                if (price.Equals("N/D"))
                    throw new Exception("Stock price not defined");

                return $"{stock} quote is ${price} per share";
            }
            catch (Exception ex)
            {
                return $"A problem ocurred while trying to get \'{parameter}\' infos: " + ex.Message;
            }
        }
    }
}

