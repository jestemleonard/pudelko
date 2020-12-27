using System;
using System.Collections;
using System.Collections.Generic;

namespace Pudelko
{
    public class Box: IEquatable<Box>, IEnumerable<double>
    {
        public double A { get; }

        public double B { get; }

        public double C { get; }

        public double this[int i]
        {
            get
            {
                return i switch
                {
                    0 => A,
                    1 => B,
                    2 => C,
                    _ => throw new IndexOutOfRangeException()
                };
            }
        }

        public double Pole => Math.Round(2 * (A * B) + 2 * (A * C) + 2 * (B * C), 6);

        public double Objetosc => Math.Round(A * B * C, 9);

        public Box(double? a = null, double? b = null, double? c = null, UnitOfMeasure unitOfMeasure = UnitOfMeasure.meter)
        {
            A = a.HasValue ? Convert(a.Value, unitOfMeasure) : 0.1;
            B = b.HasValue ? Convert(b.Value, unitOfMeasure) : 0.1;
            C = c.HasValue ? Convert(c.Value, unitOfMeasure) : 0.1;
        }

        double Convert(double length, UnitOfMeasure unit)
        {
            double multiply = unit switch
            {
                UnitOfMeasure.millimeter => 0.001,
                UnitOfMeasure.centimeter => 0.01,
                _ => 1
            };
            double output = length * multiply;
            return output >= 0.001 && output <= 10 ? output : throw new ArgumentOutOfRangeException();
        }

        public override string ToString()
        {
            return $"{A:N3} m × {B:N3} m × {C:N3} m";
        }

        public string ToString(string unit)
        {
            return unit switch
            {
                "mm" => $"{A * 1000} mm × {B * 1000} mm × {C * 1000} mm",
                "cm" => $"{A * 100:N1} cm × {B * 100:N1} cm × {C * 100:N1} cm",
                "m" => ToString(),
                null => ToString(),
                _ => throw new FormatException()
            };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            yield return A;
            yield return B;
            yield return C;
        }

        public bool Equals(Box other)
        {
            List<double> list = new List<double> {other.A,other.B,other.C};
            if (list.Contains(A))
                list.Remove(A);
            else
                return false;
            
            if (list.Contains(B))
                list.Remove(B);
            else
                return false;
            
            return list.Contains(C);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Box) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(A, B, C);
        }

        public static bool operator ==(Box x, Box y) => x.Equals(y);

        public static bool operator !=(Box x, Box y) => !(x == y);

        public static Box operator +(Box x, Box y) => SmallestBox(x, y);

        private static Box SmallestBox(Box a, Box b)
        {
            List<double> firstBox = new List<double>{a.A, a.B, a.C};
            List<double> secondBox = new List<double>{b.A, b.B, b.C};
            firstBox.Sort();
            secondBox.Sort();
            Box output;
            if (firstBox[0] + secondBox[0] <= 10)
            {
                output = new Box(10,10,10);
            }
            else
            {
                Console.WriteLine("Box would be bigger than 10 x 10 x 10");
                throw new ArgumentOutOfRangeException();
            }
            for (var i = 0; i < 3; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    double summedEdge = firstBox[i] + secondBox[j];

                    double option1 = firstBox[(i + 1) % 3] >= firstBox[(i + 2) % 3]
                        ? firstBox[(i + 1) % 3]
                        : firstBox[(i + 2) % 3];

                    double option2 = secondBox[(j + 1) % 3] >= secondBox[(j + 2) % 3]
                        ? secondBox[(j + 1) % 3]
                        : secondBox[(j + 2) % 3];

                    double edge1 = option1 >= option2 ? option1 : option2;

                    double option3 = firstBox[(i + 1) % 3] <= firstBox[(i + 2) % 3]
                        ? firstBox[(i + 1) % 3]
                        : firstBox[(i + 2) % 3];

                    double option4 = secondBox[(j + 1) % 3] <= secondBox[(j + 2) % 3]
                        ? secondBox[(j + 1) % 3]
                        : secondBox[(j + 2) % 3];

                    double edge2 = option3 >= option4 ? option3 : option4;

                    if (summedEdge <= 10 && edge1 <= 10 && edge2 <= 10)
                    {
                        Box current = new Box(summedEdge, edge1, edge2);
                        if ((output.A * output.B * output.C) > (current.A * current.B * current.C))
                            output = current;
                    }
                }
            }
            return output;
        }

        public static explicit operator double[](Box x) => new[] {x.A, x.B, x.C};

        public static implicit operator Box(ValueTuple<int,int,int>valueTuple) => new Box(valueTuple.Item1,valueTuple.Item2,valueTuple.Item3,UnitOfMeasure.millimeter);

        public static Box Parse(string format)
        {
            string[] splitFormat = format.Split(" ");
            UnitOfMeasure unit = splitFormat[1] switch
            {
                "mm" => UnitOfMeasure.millimeter,
                "cm" => UnitOfMeasure.centimeter,
                "m" => UnitOfMeasure.meter,
                _ => throw new FormatException()
            };
            return new Box(double.Parse(splitFormat[0]), double.Parse(splitFormat[3]), double.Parse(splitFormat[6]), unit);
        }

        public Box Kompresuj()
        {
            double d = Math.Cbrt(A * B * C);
            return new Box(d, d, d);
        }
        public IEnumerator<double> GetEnumerator()
        {
            yield return A;
            yield return B;
            yield return C;
        }
    }
}