using Intl.Realty.Firm.Models.Auxiliary;

namespace Intl.Realty.Firm.Helper
{
    public static class Session
    {
        private static ISession _session;

        public static void Configure(ISession session)
        {
            _session = session;
        }

        public static int UserId
            => GetInt(SessionKey.UserId);

        public static void SetString(string key, string value)
            => _session.SetString(key, value);

        public static void SetInt(string key, int value)
            => _session.SetInt32(key, value);

        public static string GetString(string key)
            => _session.GetString(key);

        public static int GetInt(string key)
            => (int)_session.GetInt32(key);

        public static void Clear()
            => _session.Clear();
    }
}
