namespace GenerateQR.Models
{
    public class ByteBuilder
    {
        private List<byte> bytes = new List<byte>();
        public void AddByte(byte value)
        {
            bytes.Add(value);
        }
        public void AddBytes(byte[] values)
        {
            bytes.AddRange(values);
        }

        public byte[] GetBytes()
        {
            return bytes.ToArray();
        }
    }
}