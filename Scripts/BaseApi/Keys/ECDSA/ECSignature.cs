using System.Collections.Generic;
using BigI;
using CustomTools.Extensions.Core;
using CustomTools.Extensions.Core.Array;
using Tools.Assert;


namespace Base.Keys.ECDSA
{
    public class ECSignature : System.IDisposable
    {
        public class Compact
        {
            public readonly bool compressed;
            public readonly byte i;
            public readonly ECSignature signature;


            public Compact(bool compressed, byte i, ECSignature signature)
            {
                this.compressed = compressed;
                this.i = i;
                this.signature = signature;
            }
        }


        private readonly BigInteger r;
        private readonly BigInteger s;


        public BigInteger R => r;

        public BigInteger S => s;

        public ECSignature(BigInteger r, BigInteger s)
        {
            this.r = r;
            this.s = s;
        }

        public void Dispose()
        {
            r.Dispose();
            s.Dispose();
        }

        // Import operations
        public static Compact ParseCompact(byte[] buffer)
        {
            Assert.Equal(buffer.Length, 65, "Invalid signature length: " + buffer.Length);
            var i = buffer[0];
            i -= 27;

            // At most 3 bits
            Assert.Equal(i, (i & 7), "Invalid signature parameter");
            var compressed = (i & 4) != 0;

            // Recovery param only
            i = (byte)(i & 3);

            var data = buffer.Slice(1, 32);
            var r = BigInteger.FromBuffer(data);
            data.Clear();
            data = buffer.Slice(33);
            var s = BigInteger.FromBuffer(data);
            data.Clear();

            return new Compact(compressed, i, new ECSignature(r, s));
        }

        public static ECSignature FromDER(byte[] buffer)
        {
            Assert.Equal(buffer[0], 0x30, "Not a DER sequence");
            Assert.Equal(buffer[1], buffer.Length - 2, "Invalid sequence length");
            var offset = 2;
            
            Assert.Equal(buffer[offset], 0x02, "Expected a DER integer");
            var rLength = buffer[offset + 1];
            Assert.Check(rLength > 0, "R length is zero");
            offset += 2;
            
            Assert.Equal(buffer[offset + rLength], 0x02, "Expected a DER integer (2)");
            var sLength = buffer[offset + rLength + 1];
            Assert.Check(sLength > 0, "S length is zero");

            var rB = buffer.Slice(offset, rLength);
            offset += rLength;
            offset += 2;
            var sB = buffer.Slice(offset, sLength);
            offset += sLength;

            if (rLength > 1 && rB[0] == 0)
            {
                Assert.Check((rB[1] & 0x80) != 0, "R value excessively padded");
            }

            if (sLength > 1 && sB[0] == 0)
            {
                Assert.Check((sB[1] & 0x80) != 0, "S value excessively padded");
            }

            Assert.Equal(offset, buffer.Length, "Invalid DER encoding");

            var r = BigInteger.FromBuffer(rB);
            rB.Clear();
            var s = BigInteger.FromBuffer(sB);
            sB.Clear();

            Assert.Check(r.Sign >= 0, "R value is negative");
            Assert.Check(s.Sign >= 0, "S value is negative");

            return new ECSignature(r, s);
        }

        public byte[] ToDER()
        {
            var rBa = r.ToDERInteger();
            var sBa = s.ToDERInteger();

            var sequence = new List<byte>();

            // INTEGER
            sequence.Add(0x02);
            sequence.Add((byte)rBa.Length);
            sequence.AddRange(rBa);
            rBa.Clear();

            // INTEGER
            sequence.Add(0x02);
            sequence.Add((byte)sBa.Length);
            sequence.AddRange(sBa);
            sBa.Clear();

            // SEQUENCE
            sequence.InsertRange(0, new byte[] { 0x30, (byte)sequence.Count });

            var result = sequence.ToArray();
            sequence.Clear();
            return result;
        }
    }
}