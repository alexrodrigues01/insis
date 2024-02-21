using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace VotesBootstrap.Helpers
{
    public class EntityConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var token = JToken.Load(reader);

            if (token.Type == JTokenType.Array)
            {
                return token.ToObject<List<object>>();
            }
            else
            {
                return token.ToObject<object>();
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value is IEnumerable<object>)
            {
                JArray.FromObject(value).WriteTo(writer);
            }
            else
            {
                JToken.FromObject(value).WriteTo(writer);
            }
        }
    }
}
