using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Pudelko
{

    // Nie rozumiem tego i jeśli to zostaje to mi nie działają testy
    
    // [TestClass]
    // public static class InitializeCulture
    // {
    //     [AssemblyInitialize]
    //     public static void SetEnglishCultureOnAllUnitTest()
    //     {
    //         Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
    //         Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
    //     }
    // }

    //========================================

    [TestClass]
    public class UnitTestsBoxClassConstructors
    {
        private static double defaultSize = 0.1; // w metrach
        private static double accuracy = 0.001; //dokładność 3 miejsca po przecinku

        private void AssertBoxClass(Box p, double expectedA, double expectedB, double expectedC)
        {
            Assert.AreEqual(expectedA, p.A, delta: accuracy);
            Assert.AreEqual(expectedB, p.B, delta: accuracy);
            Assert.AreEqual(expectedC, p.C, delta: accuracy);
        }

        #region Constructor tests ================================

        [TestMethod, TestCategory("Constructors")]
        public void Constructor_Default()
        {
            Box p = new Box();

            Assert.AreEqual(defaultSize, p.A, delta: accuracy);
            Assert.AreEqual(defaultSize, p.B, delta: accuracy);
            Assert.AreEqual(defaultSize, p.C, delta: accuracy);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(1.0, 2.543, 3.1,
            1.0, 2.543, 3.1)]
        [DataRow(1.0001, 2.54387, 3.1005,
            1.0, 2.543, 3.1)] // dla metrów liczą się 3 miejsca po przecinku
        public void Constructor_3params_DefaultMeters(double a, double b, double c,
            double expectedA, double expectedB, double expectedC)
        {
            Box p = new Box(a, b, c);

            AssertBoxClass(p, expectedA, expectedB, expectedC);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(1.0, 2.543, 3.1,
            1.0, 2.543, 3.1)]
        [DataRow(1.0001, 2.54387, 3.1005,
            1.0, 2.543, 3.1)] // dla metrów liczą się 3 miejsca po przecinku
        public void Constructor_3params_InMeters(double a, double b, double c,
            double expectedA, double expectedB, double expectedC)
        {
            Box p = new Box(a, b, c, unitOfMeasure: UnitOfMeasure.meter);

            AssertBoxClass(p, expectedA, expectedB, expectedC);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(100.0, 25.5, 3.1,
            1.0, 0.255, 0.031)]
        [DataRow(100.0, 25.58, 3.13,
            1.0, 0.255, 0.031)] // dla centymertów liczy się tylko 1 miejsce po przecinku
        public void Constructor_3params_InCentimeters(double a, double b, double c,
            double expectedA, double expectedB, double expectedC)
        {
            Box p = new Box(a: a, b: b, c: c, unitOfMeasure: UnitOfMeasure.centimeter);

            AssertBoxClass(p, expectedA, expectedB, expectedC);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(100, 255, 3,
            0.1, 0.255, 0.003)]
        [DataRow(100.0, 25.58, 3.13,
            0.1, 0.025, 0.003)] // dla milimetrów nie liczą się miejsca po przecinku
        public void Constructor_3params_InMilimeters(double a, double b, double c,
            double expectedA, double expectedB, double expectedC)
        {
            Box p = new Box(unitOfMeasure: UnitOfMeasure.millimeter, a: a, b: b, c: c);

            AssertBoxClass(p, expectedA, expectedB, expectedC);
        }


        // ----

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(1.0, 2.5, 1.0, 2.5)]
        [DataRow(1.001, 2.599, 1.001, 2.599)]
        [DataRow(1.0019, 2.5999, 1.001, 2.599)]
        public void Constructor_2params_DefaultMeters(double a, double b, double expectedA, double expectedB)
        {
            Box p = new Box(a, b);

            AssertBoxClass(p, expectedA, expectedB, expectedC: 0.1);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(1.0, 2.5, 1.0, 2.5)]
        [DataRow(1.001, 2.599, 1.001, 2.599)]
        [DataRow(1.0019, 2.5999, 1.001, 2.599)]
        public void Constructor_2params_InMeters(double a, double b, double expectedA, double expectedB)
        {
            Box p = new Box(a: a, b: b, unitOfMeasure: UnitOfMeasure.meter);

            AssertBoxClass(p, expectedA, expectedB, expectedC: 0.1);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(11.0, 2.5, 0.11, 0.025)]
        [DataRow(100.1, 2.599, 1.001, 0.025)]
        [DataRow(2.0019, 0.25999, 0.02, 0.002)]
        public void Constructor_2params_InCentimeters(double a, double b, double expectedA, double expectedB)
        {
            Box p = new Box(unitOfMeasure: UnitOfMeasure.centimeter, a: a, b: b);

            AssertBoxClass(p, expectedA, expectedB, expectedC: 0.1);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(11, 2.0, 0.011, 0.002)]
        [DataRow(100.1, 2599, 0.1, 2.599)]
        [DataRow(200.19, 2.5999, 0.2, 0.002)]
        public void Constructor_2params_InMilimeters(double a, double b, double expectedA, double expectedB)
        {
            Box p = new Box(unitOfMeasure: UnitOfMeasure.millimeter, a: a, b: b);

            AssertBoxClass(p, expectedA, expectedB, expectedC: 0.1);
        }

        // -------

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(2.5)]
        public void Constructor_1param_DefaultMeters(double a)
        {
            Box p = new Box(a);

            Assert.AreEqual(a, p.A);
            Assert.AreEqual(0.1, p.B);
            Assert.AreEqual(0.1, p.C);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(2.5)]
        public void Constructor_1param_InMeters(double a)
        {
            Box p = new Box(a);

            Assert.AreEqual(a, p.A);
            Assert.AreEqual(0.1, p.B);
            Assert.AreEqual(0.1, p.C);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(11.0, 0.11)]
        [DataRow(100.1, 1.001)]
        [DataRow(2.0019, 0.02)]
        public void Constructor_1param_InCentimeters(double a, double expectedA)
        {
            Box p = new Box(unitOfMeasure: UnitOfMeasure.centimeter, a: a);

            AssertBoxClass(p, expectedA, expectedB: 0.1, expectedC: 0.1);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(11, 0.011)]
        [DataRow(100.1, 0.1)]
        [DataRow(200.19, 0.2)]
        public void Constructor_1param_InMilimeters(double a, double expectedA)
        {
            Box p = new Box(unitOfMeasure: UnitOfMeasure.millimeter, a: a);

            AssertBoxClass(p, expectedA, expectedB: 0.1, expectedC: 0.1);
        }

        // ---

        public static IEnumerable<object[]> DataSet1Meters_ArgumentOutOfRangeEx => new List<object[]>
        {
            new object[] {-1.0, 2.5, 3.1},
            new object[] {1.0, -2.5, 3.1},
            new object[] {1.0, 2.5, -3.1},
            new object[] {-1.0, -2.5, 3.1},
            new object[] {-1.0, 2.5, -3.1},
            new object[] {1.0, -2.5, -3.1},
            new object[] {-1.0, -2.5, -3.1},
            new object[] {0, 2.5, 3.1},
            new object[] {1.0, 0, 3.1},
            new object[] {1.0, 2.5, 0},
            new object[] {1.0, 0, 0},
            new object[] {0, 2.5, 0},
            new object[] {0, 0, 3.1},
            new object[] {0, 0, 0},
            new object[] {10.1, 2.5, 3.1},
            new object[] {10, 10.1, 3.1},
            new object[] {10, 10, 10.1},
            new object[] {10.1, 10.1, 3.1},
            new object[] {10.1, 10, 10.1},
            new object[] {10, 10.1, 10.1},
            new object[] {10.1, 10.1, 10.1}
        };

        [DataTestMethod, TestCategory("Constructors")]
        [DynamicData(nameof(DataSet1Meters_ArgumentOutOfRangeEx))]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_3params_DefaultMeters_ArgumentOutOfRangeException(double a, double b, double c)
        {
            Box p = new Box(a, b, c);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DynamicData(nameof(DataSet1Meters_ArgumentOutOfRangeEx))]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_3params_InMeters_ArgumentOutOfRangeException(double a, double b, double c)
        {
            Box p = new Box(a, b, c, unitOfMeasure: UnitOfMeasure.meter);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(-1, 1, 1)]
        [DataRow(1, -1, 1)]
        [DataRow(1, 1, -1)]
        [DataRow(-1, -1, 1)]
        [DataRow(-1, 1, -1)]
        [DataRow(1, -1, -1)]
        [DataRow(-1, -1, -1)]
        [DataRow(0, 1, 1)]
        [DataRow(1, 0, 1)]
        [DataRow(1, 1, 0)]
        [DataRow(0, 0, 1)]
        [DataRow(0, 1, 0)]
        [DataRow(1, 0, 0)]
        [DataRow(0, 0, 0)]
        [DataRow(0.01, 0.1, 1)]
        [DataRow(0.1, 0.01, 1)]
        [DataRow(0.1, 0.1, 0.01)]
        [DataRow(1001, 1, 1)]
        [DataRow(1, 1001, 1)]
        [DataRow(1, 1, 1001)]
        [DataRow(1001, 1, 1001)]
        [DataRow(1, 1001, 1001)]
        [DataRow(1001, 1001, 1)]
        [DataRow(1001, 1001, 1001)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_3params_InCentimeters_ArgumentOutOfRangeException(double a, double b, double c)
        {
            Box p = new Box(a, b, c, unitOfMeasure: UnitOfMeasure.centimeter);
        }


        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(-1, 1, 1)]
        [DataRow(1, -1, 1)]
        [DataRow(1, 1, -1)]
        [DataRow(-1, -1, 1)]
        [DataRow(-1, 1, -1)]
        [DataRow(1, -1, -1)]
        [DataRow(-1, -1, -1)]
        [DataRow(0, 1, 1)]
        [DataRow(1, 0, 1)]
        [DataRow(1, 1, 0)]
        [DataRow(0, 0, 1)]
        [DataRow(0, 1, 0)]
        [DataRow(1, 0, 0)]
        [DataRow(0, 0, 0)]
        [DataRow(0.1, 1, 1)]
        [DataRow(1, 0.1, 1)]
        [DataRow(1, 1, 0.1)]
        [DataRow(10001, 1, 1)]
        [DataRow(1, 10001, 1)]
        [DataRow(1, 1, 10001)]
        [DataRow(10001, 10001, 1)]
        [DataRow(10001, 1, 10001)]
        [DataRow(1, 10001, 10001)]
        [DataRow(10001, 10001, 10001)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_3params_InMiliimeters_ArgumentOutOfRangeException(double a, double b, double c)
        {
            Box p = new Box(a, b, c, unitOfMeasure: UnitOfMeasure.millimeter);
        }


        public static IEnumerable<object[]> DataSet2Meters_ArgumentOutOfRangeEx => new List<object[]>
        {
            new object[] {-1.0, 2.5},
            new object[] {1.0, -2.5},
            new object[] {-1.0, -2.5},
            new object[] {0, 2.5},
            new object[] {1.0, 0},
            new object[] {0, 0},
            new object[] {10.1, 10},
            new object[] {10, 10.1},
            new object[] {10.1, 10.1}
        };

        [DataTestMethod, TestCategory("Constructors")]
        [DynamicData(nameof(DataSet2Meters_ArgumentOutOfRangeEx))]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_2params_DefaultMeters_ArgumentOutOfRangeException(double a, double b)
        {
            Box p = new Box(a, b);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DynamicData(nameof(DataSet2Meters_ArgumentOutOfRangeEx))]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_2params_InMeters_ArgumentOutOfRangeException(double a, double b)
        {
            Box p = new Box(a, b, unitOfMeasure: UnitOfMeasure.meter);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(-1, 1)]
        [DataRow(1, -1)]
        [DataRow(-1, -1)]
        [DataRow(0, 1)]
        [DataRow(1, 0)]
        [DataRow(0, 0)]
        [DataRow(0.01, 1)]
        [DataRow(1, 0.01)]
        [DataRow(0.01, 0.01)]
        [DataRow(1001, 1)]
        [DataRow(1, 1001)]
        [DataRow(1001, 1001)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_2params_InCentimeters_ArgumentOutOfRangeException(double a, double b)
        {
            Box p = new Box(a, b, unitOfMeasure: UnitOfMeasure.centimeter);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(-1, 1)]
        [DataRow(1, -1)]
        [DataRow(-1, -1)]
        [DataRow(0, 1)]
        [DataRow(1, 0)]
        [DataRow(0, 0)]
        [DataRow(0.1, 1)]
        [DataRow(1, 0.1)]
        [DataRow(0.1, 0.1)]
        [DataRow(10001, 1)]
        [DataRow(1, 10001)]
        [DataRow(10001, 10001)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_2params_InMilimeters_ArgumentOutOfRangeException(double a, double b)
        {
            Box p = new Box(a, b, unitOfMeasure: UnitOfMeasure.millimeter);
        }




        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(-1.0)]
        [DataRow(0)]
        [DataRow(10.1)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_1param_DefaultMeters_ArgumentOutOfRangeException(double a)
        {
            Box p = new Box(a);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(-1.0)]
        [DataRow(0)]
        [DataRow(10.1)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_1param_InMeters_ArgumentOutOfRangeException(double a)
        {
            Box p = new Box(a, unitOfMeasure: UnitOfMeasure.meter);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(-1.0)]
        [DataRow(0)]
        [DataRow(0.01)]
        [DataRow(1001)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_1param_InCentimeters_ArgumentOutOfRangeException(double a)
        {
            Box p = new Box(a, unitOfMeasure: UnitOfMeasure.centimeter);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(-1)]
        [DataRow(0)]
        [DataRow(0.1)]
        [DataRow(10001)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_1param_InMilimeters_ArgumentOutOfRangeException(double a)
        {
            Box p = new Box(a, unitOfMeasure: UnitOfMeasure.millimeter);
        }

        #endregion

        #region ToString tests ===================================

        [TestMethod, TestCategory("String representation")]
        public void ToString_Default_Culture_EN()
        {
            var p = new Box(2.5, 9.321);
            string expectedStringEN = "2.500 m × 9.321 m × 0.100 m";

            Assert.AreEqual(expectedStringEN, p.ToString());
        }

        [DataTestMethod, TestCategory("String representation")]
        [DataRow(null, 2.5, 9.321, 0.1, "2.500 m × 9.321 m × 0.100 m")]
        [DataRow("m", 2.5, 9.321, 0.1, "2.500 m × 9.321 m × 0.100 m")]
        [DataRow("cm", 2.5, 9.321, 0.1, "250.0 cm × 932.1 cm × 10.0 cm")]
        [DataRow("mm", 2.5, 9.321, 0.1, "2500 mm × 9321 mm × 100 mm")]
        public void ToString_Formattable_Culture_EN(string format, double a, double b, double c,
            string expectedStringRepresentation)
        {
            var p = new Box(a, b, c, unitOfMeasure: UnitOfMeasure.meter);
            Assert.AreEqual(expectedStringRepresentation, p.ToString(format));
        }

        [TestMethod, TestCategory("String representation")]
        [ExpectedException(typeof(FormatException))]
        public void ToString_Formattable_WrongFormat_FormatException()
        {
            var p = new Box(1);
            var stringformatedrepreentation = p.ToString("wrong code");
        }

        #endregion

        #region Pole, Objętość ===================================

        [TestMethod]
        [DataRow(4.125, 6.92, 2)]
        [DataRow(0.1, 0.441, 0.29)]
        public void Objetosc_Values_AreRight(double a, double b, double c)
        {
            Box a1 = new Box(a, b, c);
            double compare = Math.Round(a * b * c, 9);
            Assert.AreEqual(compare, a1.Objetosc);
        }

        [TestMethod]
        [DataRow(4.125, 6.92, 2)]
        [DataRow(0.1, 0.441, 0.29)]
        public void Field_Values_AreRight(double a, double b, double c)
        {
            Box a1 = new Box(a, b, c);
            double compare = Math.Round(((a * b) * 2) + ((b * c) * 2) + ((c * a) * 2), 6);
            Assert.AreEqual(compare, a1.Pole);
        }

        #endregion

        #region Equals ===========================================

        [TestMethod]
        [DataRow(0.1, 0.25, 0.99,
            25, 99, 10, UnitOfMeasure.centimeter)]
        [DataRow(0.1, 0.25, 0.99,
            250, 100, 990, UnitOfMeasure.millimeter)]
        [DataRow(0.1, 0.25, 0.99,
            0.99, 0.1, 0.25, UnitOfMeasure.meter)]
        public void Equals_ValuesAreEqual_ReturnTrue(double a, double b, double c,
            double d, double e, double f, UnitOfMeasure unitOfMeasure)
        {
            Box newABox = new Box(a, b, c);
            Box newBBox = new Box(d, e, f, unitOfMeasure);
            Assert.IsTrue(newABox.Equals(newBBox));
            Assert.IsFalse(!newABox.Equals(newBBox));
        }

        [TestMethod]
        [DataRow(0.12, 0.25, 0.99,
            25, 990, 10, UnitOfMeasure.centimeter)]
        [DataRow(1, 0.249, 9,
            250, 100, 99, UnitOfMeasure.millimeter)]
        [DataRow(0.1, 0.275, 0.99,
            0.99, 1, 0.25, UnitOfMeasure.meter)]
        public void Equals_ValuesAreNOTEqual_ReturnTrue(double a, double b, double c,
            double d, double e, double f, UnitOfMeasure unitOfMeasure)
        {
            Box newABox = new Box(a, b, c);
            Box newBBox = new Box(d, e, f, unitOfMeasure);
            Assert.IsFalse(newABox.Equals(newBBox));
            Assert.IsTrue(!newABox.Equals(newBBox));
        }

        #endregion

        #region Operators overloading ===========================

        [TestMethod]
        [DataRow(0.1, 0.25, 0.99,
            25, 99, 10, UnitOfMeasure.centimeter)]
        [DataRow(0.1, 0.25, 0.99,
            250, 100, 990, UnitOfMeasure.millimeter)]
        [DataRow(0.1, 0.25, 0.99,
            0.99, 0.1, 0.25, UnitOfMeasure.meter)]
        public void EqualsOperator_ValuesAreEqual_ReturnTrue(double a, double b, double c,
            double d, double e, double f, UnitOfMeasure unitOfMeasure)
        {
            Box newABox = new Box(a, b, c);
            Box newBBox = new Box(d, e, f, unitOfMeasure);
            Assert.IsTrue(newABox == newBBox);
            Assert.IsFalse(newABox != newBBox);
        }

        [TestMethod]
        [DataRow(0.12, 0.25, 0.99,
            25, 990, 10, UnitOfMeasure.centimeter)]
        [DataRow(1, 0.249, 9,
            250, 100, 99, UnitOfMeasure.millimeter)]
        [DataRow(0.1, 0.275, 0.99,
            0.99, 1, 0.25, UnitOfMeasure.meter)]
        public void NotEqualsOperator_ValuesAreNOTEqual_ReturnTrue(double a, double b, double c,
            double d, double e, double f, UnitOfMeasure unitOfMeasure)
        {
            Box newABox = new Box(a, b, c);
            Box newBBox = new Box(d, e, f, unitOfMeasure);
            Assert.IsFalse(newABox == newBBox);
            Assert.IsTrue(newABox != newBBox);
        }

        [TestMethod]
        public void AddOperator_CreateSmallestPossibleBoxThatContainBoth_ReturnSmallestBox()
        {
            Box a = new Box(10, 20, 30, UnitOfMeasure.centimeter);
            Box b = new Box(70, 15, 110, UnitOfMeasure.centimeter);
            Box c = new Box(130, 15, 70, UnitOfMeasure.centimeter);
            Assert.IsTrue(a + b == c);
        }

        #endregion

        #region Conversions =====================================

        [TestMethod]
        public void ExplicitConversion_ToDoubleArray_AsMeters()
        {
            var p = new Box(1, 2.1, 3.231);
            double[] tab = (double[]) p;
            Assert.AreEqual(3, tab.Length);
            Assert.AreEqual(p.A, tab[0]);
            Assert.AreEqual(p.B, tab[1]);
            Assert.AreEqual(p.C, tab[2]);
        }

        [TestMethod]
        public void ImplicitConversion_FromValueTuple_As_BoxClass_InMilimeters()
        {
            var (a, b, c) = (2500, 9321, 100); // in millimeters, ValueTuple
            Box p = (a, b, c);
            Assert.AreEqual((int) (p.A * 1000), a);
            Assert.AreEqual((int) (p.B * 1000), b);
            Assert.AreEqual((int) (p.C * 1000), c);
        }

        #endregion

        #region Indexer, enumeration ============================

        [TestMethod]
        public void Indexer_ReadFrom()
        {
            var p = new Box(1, 2.1, 3.231);
            Assert.AreEqual(p.A, p[0]);
            Assert.AreEqual(p.B, p[1]);
            Assert.AreEqual(p.C, p[2]);
        }

        [TestMethod]
        public void ForEach_Test()
        {
            var p = new Box(1, 2.1, 3.231);
            var tab = new[] {p.A, p.B, p.C};
            int i = 0;
            foreach (double x in p)
            {
                Assert.AreEqual(x, tab[i]);
                i++;
            }
        }

        #endregion

    }
}