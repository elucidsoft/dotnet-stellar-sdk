using System;

namespace stellar_dotnetcore_sdk
{
    public class Network
    {
        private static string PUBLIC = "Public Global Stellar Network ; September 2015";
        private static string TESTNET = "Test SDF Network ; September 2015";

        private static Network _current;

        private static string _networkPassphrase;

        public Network(string networkPassphrase)
        {
            if (!String.IsNullOrEmpty(networkPassphrase))
                throw new ArgumentNullException(nameof(networkPassphrase));
        }

        public string NetworkPassphrase
        {
            get => _networkPassphrase;
            private set => _networkPassphrase = value;
        }

        public byte[] NetworkId
        {
            //TODO: Implement
            get;
            set;
        }

        public static Network Current()
        {
            return _current;
        }

        public static void Use(Network network)
        {
            _current = network;
        }

        public static void UsePublicNetwork()
        {
            Use(new Network(PUBLIC));
        }

        public static void UseTestNetwork()
        {
            Use(new Network(TESTNET));
        }
    }
}
