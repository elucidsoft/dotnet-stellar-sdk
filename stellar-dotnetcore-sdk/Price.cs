using System;
using System.Collections.Generic;
using sdkxdr = stellar_dotnetcore_sdk.xdr;

namespace stellar_dotnetcore_sdk
{
    public class Price
    {
        //@SerializedName("n")
        private readonly int _Numerator;
        //@SerializedName("d")
        private readonly int _Denominator;

        public int Numerator => _Numerator;
        public int Denominator => _Denominator;

        ///<summary>
        ///Create a new price. Price in Stellar is represented as a fraction.
        ///</summary>
        ///<param name="n">Numerator</param>
        ///<param name="d">Denominator</param>
        ///
        Price(int n, int d)
        {
            this._Numerator = n;
            this._Denominator = d;
        }


        ///<summary>
        /// Approximates<code> price</code> to a fraction.
        /// </summary>
        /// <param name="price">Example 1.25</param>
        public static Price FromString(String price)
        {
            if (string.IsNullOrEmpty(price))
                throw new ArgumentNullException(nameof(price), "price cannot be null");

            Decimal maxInt = new Decimal(int.MaxValue);
            Decimal number = Convert.ToDecimal(price);
            Decimal a;
            Decimal f;
            List<Decimal[]> fractions = new List<Decimal[]>();
            fractions.Add(new Decimal[] { new Decimal(0), new Decimal(1) });
            fractions.Add(new Decimal[] { new Decimal(1), new Decimal(0) });
            int i = 2;
            while (true)
            {
                if (number.CompareTo(maxInt) > 0)
                {
                    break;
                }

                a = Decimal.Floor(number);
                f = Decimal.Subtract(number, a);
                Decimal h = Decimal.Add(Decimal.Multiply(a, fractions[i - 1][0]), fractions[i - 2][0]);
                Decimal k = Decimal.Add(Decimal.Multiply(a, fractions[i - 1][1]), fractions[i - 2][1]);
                if (h.CompareTo(maxInt) > 0 || k.CompareTo(maxInt) > 0)
                {
                    break;
                }
                fractions.Add(new Decimal[] { h, k });
                if (f.CompareTo(0m) == 0)
                {
                    break;
                }
                number = Decimal.Divide(1m, f);
                i = i + 1;
            }
            Decimal n = fractions[fractions.Count - 1][0];
            Decimal d = fractions[fractions.Count - 1][1];
            return new Price(Convert.ToInt32(n), Convert.ToInt32(d));
        }

        ///<summary>
        /// Generates Price XDR object.
        /// </summary>
        public sdkxdr.Price ToXdr()
        {
            sdkxdr.Price xdr = new sdkxdr.Price();
            sdkxdr.Int32 n = new sdkxdr.Int32();
            sdkxdr.Int32 d = new sdkxdr.Int32();
            n.InnerValue = this.Numerator;
            d.InnerValue = this.Denominator;
            xdr.N = n;
            xdr.D = d;
            return xdr;
        }


        public new bool Equals(Object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (!(obj is Price))
            {
                return false;
            }

            Price price = (Price)obj;

            return this.Numerator == price.Numerator &&
                    this.Denominator == price.Denominator;

        }
    }
}
