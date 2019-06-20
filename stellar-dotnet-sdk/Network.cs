using System;
using System.Text;

namespace stellar_dotnet_sdk
{
    public class Network
    {
        public static readonly string PublicPassphrase = "Public Global Stellar Network ; September 2015";
        public static readonly string TestnetPassphrase = "Test SDF Network ; September 2015";

        public Network(string networkPassphrase)
        {
            NetworkPassphrase = networkPassphrase ?? throw new ArgumentNullException(nameof(networkPassphrase), "networkPassphrase cannot be null");
        }

        public string NetworkPassphrase { get; }

        public byte[] NetworkId => Util.Hash(Encoding.UTF8.GetBytes(NetworkPassphrase));

        public static Network Current { get; private set; }

        public static Network Public()
        {
            return new Network(PublicPassphrase);
        }

        public static Network Test()
        {
            return new Network(TestnetPassphrase);
        }

        public static void Use(Network network)
        {
            Current = network;
        }

        public static bool IsPublicNetwork(Network network)
        {
            return network.NetworkPassphrase == PublicPassphrase;
        }

        public static void UsePublicNetwork()
        {
            Use(Public());
        }

        public static void UseTestNetwork()
        {
            Use(Test());
        }
    }
}