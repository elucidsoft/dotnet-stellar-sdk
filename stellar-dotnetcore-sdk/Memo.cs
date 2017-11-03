namespace stellar_dotnetcore_sdk
{
    public abstract class Memo
    {
        /**
    * Creates new MemoNone instance.
    */
        public static MemoNone None()
        {
            return new MemoNone();
        }

        /**
         * Creates new {@link MemoText} instance.
         * @param text
         */
        public static MemoText Text(string text)
        {
            return new MemoText(text);
        }

        /**
         * Creates new {@link MemoId} instance.
         * @param id
         */
        public static MemoId Id(long id)
        {
            return new MemoId(id);
        }

        /**
         * Creates new {@link MemoHash} instance from byte array.
         * @param bytes
         */
        public static MemoHash Hash(byte[] bytes)
        {
            return new MemoHash(bytes);
        }

        /**
         * Creates new {@link MemoHash} instance from hex-encoded string
         * @param hexString
         * @throws DecoderException
         */
        public static MemoHash Hash(string hexString)
        {
            return new MemoHash(hexString);
        }

        /**
         * Creates new {@link MemoReturnHash} instance from byte array.
         * @param bytes
         */
        public static MemoReturnHash returnHash(byte[] bytes)
        {
            return new MemoReturnHash(bytes);
        }

        /**
         * Creates new {@link MemoReturnHash} instance from hex-encoded string.
         * @param hexString
         * @throws DecoderException
         */
        public static MemoReturnHash returnHash(string hexString)
        {
            return new MemoReturnHash(hexString);
        }

        public abstract xdr.Memo ToXdr();
    }
}