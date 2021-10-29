using System;
using System.Security.Cryptography;
using System.Text;
using Isopoh.Cryptography.Argon2;
using Isopoh.Cryptography.SecureArray;

namespace Utility {
    public class StringHasher {
        public static (string hash, string salt) HashPassword(string password, string pepper, string? salt = null) {
            if (salt is null) {
                byte[] encodedSalt = new byte[16];
                (new Random()).NextBytes(encodedSalt);
                salt = Encoding.UTF8.GetString(encodedSalt);
            }

            return (Argon2.Hash(password, salt + pepper, 3, 65536, Environment.ProcessorCount * 2, Argon2Type.DataIndependentAddressing, 32), salt);
        }

        public static bool VerifyPassword(string hash, string password, string pepper, string salt) {
            return Argon2.Verify(hash, password, salt + pepper);
        }

        public static string GenerateToken(string seed, int memoryCost = 16384, int length = 32) {
            Argon2Config a2c = new Argon2Config();

            string encodedHash = Argon2.Hash(seed, null, 1, 16384, 1, Argon2Type.DataDependentAddressing, length);
            
            SecureArray<byte>? decodedHash;
            a2c.DecodeString(encodedHash, out decodedHash);

            string hash = Convert.ToBase64String(decodedHash!.Buffer);

            return hash;
        }
    }
}