using System.Runtime.InteropServices;
using CustomTools.Extensions.Core.Array;
using UnityEngine;


namespace ED25519REF10
{
    public class ED25519 : MonoBehaviour
    {
        private const int SUCCESSFUL_RESULT = 1;
        private const int FAILURE_RESULT = 0;
        private const int PUBKEY_SIZE = 32;
        private const int PRIVKEY_SIZE = 32;
        private const int SIGNATURE_SIZE = 64;


#if UNITY_EDITOR || UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN

        [DllImport("ed25519")] private static extern int ed25519_create_keypair(byte[] sk, byte[] pk);
        [DllImport("ed25519")] private static extern void ed25519_derive_public_key(byte[] sk, byte[] pk);
        [DllImport("ed25519")] private static extern void ed25519_sign(byte[] sig, byte[] msg, long msglen, byte[] pk, byte[] sk);
        [DllImport("ed25519")] private static extern int ed25519_verify(byte[] sig, byte[] msg, long msglen, byte[] pk);

#elif UNITY_IOS

        [DllImport("__Internal")] private static extern int ed25519_create_keypair(byte[] sk, byte[] pk);
        [DllImport("__Internal")] private static extern void ed25519_derive_public_key(byte[] sk, byte[] pk);
        [DllImport("__Internal")] private static extern void ed25519_sign(byte[] sig, byte[] msg, long msglen, byte[] pk, byte[] sk);
        [DllImport("__Internal")] private static extern int ed25519_verify(byte[] sig, byte[] msg, long msglen, byte[] pk);

#elif UNITY_ANDROID

        [DllImport("ed25519")] private static extern int ed25519_create_keypair(byte[] sk, byte[] pk);
        [DllImport("ed25519")] private static extern void ed25519_derive_public_key(byte[] sk, byte[] pk);
        [DllImport("ed25519")] private static extern void ed25519_sign(byte[] sig, byte[] msg, long msglen, byte[] pk, byte[] sk);
        [DllImport("ed25519")] private static extern int ed25519_verify(byte[] sig, byte[] msg, long msglen, byte[] pk);

#else

        private static int ed25519_create_keypair(byte[] sk, byte[] pk) { return 0; }
        private static void ed25519_derive_public_key(byte[] sk, byte[] pk) { }
        private static void ed25519_sign(byte[] sig, byte[] msg, long msglen, byte[] pk, byte[] sk) { }
        private static int ed25519_verify(byte[] sig, byte[] msg, long msglen, byte[] pk) { return 0; }

#endif


        public static bool CreateKeypair(out byte[] publicKey, out byte[] privateKey)
        {
            publicKey = new byte[PUBKEY_SIZE].Fill((byte)0);
            privateKey = new byte[PRIVKEY_SIZE].Fill((byte)0);
            return FAILURE_RESULT != ed25519_create_keypair(privateKey, publicKey);
        }

        public static byte[] DerivePublicKey(byte[] privateKey)
        {
            var publicKey = new byte[PUBKEY_SIZE].Fill((byte)0);
            ed25519_derive_public_key(privateKey, publicKey);
            return publicKey;
        }

        public static byte[] Sign(byte[] message, byte[] publicKey, byte[] privateKey)
        {
            message = message ?? new byte[0];
            var signature = new byte[SIGNATURE_SIZE].Fill((byte)0);
            ed25519_sign(signature, message, message.LongLength, publicKey, privateKey);
            return signature;
        }

        public static bool Verify(byte[] signature, byte[] message, byte[] publicKey)
        {
            message = message ?? new byte[0];
            return SUCCESSFUL_RESULT == ed25519_verify(signature, message, message.LongLength, publicKey);
        }
    }
}