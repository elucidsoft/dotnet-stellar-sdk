using System;
using System.Text;

namespace stellar_dotnet_sdk
{
    public class Network
    {
        private static readonly string PUBLIC = "Public Global Stellar Network ; September 2015";
        private static readonly string TESTNET = "Test SDF Network ; September 2015";

        private static Network _network;

        public Network(string networkPassphrase)
        {
            NetworkPassphrase = networkPassphrase ?? throw new ArgumentNullException(nameof(networkPassphrase), "networkPassphrase cannot be null");
        }

        public string NetworkPassphrase { get; }

        public byte[] NetworkId => Util.Hash(Encoding.UTF8.GetBytes(Current.NetworkPassphrase));

        public static Network Current
        {
            get
            {
                if (_network == null)
                    _network = new Network(PUBLIC);

                return _network;
            }
            private set { _network = value; }
        }

        public static void Use(Network network)
        {
            Current = network;
        }

        public static bool IsTestNetwork()
        {
            return Current.NetworkPassphrase == TESTNET;
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