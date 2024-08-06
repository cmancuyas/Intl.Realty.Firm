using Intl.Realty.Firm.Service.IServices;
using System.Text;
using System.Text.Json;

namespace Intl.Realty.Firm.Service
{
    public class JsonService : IJsonService
    {
        public byte[] Write<T>(IList<T> registers)
        {
            return Encoding.UTF8.GetBytes(JsonSerializer.Serialize(registers));
        }
    }
}
