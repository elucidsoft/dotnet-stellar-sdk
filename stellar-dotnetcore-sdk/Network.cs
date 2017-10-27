using System;
using System.Text;

namespace stellar_dotnetcore_sdk
{
    public class Network
    {
        private static string PUBLIC = "Public Global Stellar Network ; September 2015";
        private static string TESTNET = "Test SDF Network ; September 2015";
        private static Network _current;

        private string _networkPassphrase;

        public Network(string networkPassphrase)
        {
            _networkPassphrase = networkPassphrase ?? throw new ArgumentNullException(nameof(networkPassphrase), "networkPassphrase cannot be null");               
        }

        public string NetworkPassphrase
        {
            get => _networkPassphrase;
            private set => _networkPassphrase = value;
        }

        public byte[] NetworkId
        {
            get => Util.Hash(Encoding.UTF8.GetBytes(_current.NetworkPassphrase));
        }

        public static Network Current { get => _current; }

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
