using System;
using System.Collections.Generic;
using System.Text;

namespace fraction_ItI
{
    public class Fraction
    {
        public int Numerator { get; }
        public int Denominator { get; }

        public static readonly Fraction Zero = new Fraction(0, 1);

        public Fraction(int numerator, int denominator)
        {
            if (denominator == 0)
                throw new ArgumentException("zero denominator");

            if (denominator < 0)
            {
                numerator = -numerator;
                denominator = -denominator;
            }

            int gcd = Gcd(Math.Abs(numerator), Math.Abs(denominator));

            Numerator = numerator / gcd;
            Denominator = denominator / gcd;
        }

        private static int Gcd(int a, int b)
        {
            while (b != 0)
            {
                int temp = b;
                b = a % b;
                a = temp;
            }

            return a;
        }

        public override string ToString()
        {
            return $"{Numerator}/{Denominator}";
        }

        public static Fraction operator +(Fraction a, Fraction b)
        {
            return new Fraction(
                a.Numerator * b.Denominator +
                b.Numerator * a.Denominator,
                a.Denominator * b.Denominator
            );
        }

        public static Fraction operator -(Fraction a, Fraction b)
        {
            return new Fraction(
                a.Numerator * b.Denominator -
                b.Numerator * a.Denominator,
                a.Denominator * b.Denominator
            );
        }

        public static Fraction operator *(Fraction a, Fraction b)
        {
            return new Fraction(
                a.Numerator * b.Numerator,
                a.Denominator * b.Denominator
            );
        }

        public static Fraction operator /(Fraction a, Fraction b)
        {
            return new Fraction(
                a.Numerator * b.Denominator,
                a.Denominator * b.Numerator
            );
        }

        public static bool operator ==(Fraction? a, Fraction? b)
        {
            if (ReferenceEquals(a, b))
                return true;

            if (a is null || b is null)
                return false;

            return a.Numerator == b.Numerator &&
                   a.Denominator == b.Denominator;
        }

        public static bool operator !=(Fraction? a, Fraction? b)
        {
            return !(a == b);
        }

        public static bool operator <(Fraction a, Fraction b)
        {
            return a.Numerator * b.Denominator
                 < b.Numerator * a.Denominator;
        }

        public static bool operator >(Fraction a, Fraction b)
        {
            return a.Numerator * b.Denominator
                 > b.Numerator * a.Denominator;
        }
    }
}
