using System;
using NMath = System.Math;

namespace MPT.Math
{
    /// <summary>
    /// Contains static methods for common algebraic operations.
    /// </summary>
    public static class Algebra
    {       
        /// <summary>
        /// Performs the square root of the sum of the squares of the provided values.
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static double SRSS(params double[] values)
        {
            double sumOfSquares = 0;
            foreach (double value in values)
            {
                sumOfSquares += value.Squared();
            }

            return NMath.Sqrt(sumOfSquares);
        }

        /// <summary>
        /// Returns the x solution to the equation ax^2 + bx + c = 0.
        /// </summary>
        /// <param name="a">Multiplier to x^2.</param>
        /// <param name="b">Multiplier to x.</param>
        /// <param name="c">Constant.</param>
        /// <returns></returns>
        public static double[] QuadraticFormula(double a, double b, double c)
        {
            double denominator = 2 * a;
            double operand1 = -b / denominator;
            double operand2 = NMath.Sqrt(b.Squared() - 4 * a * c) / denominator;

            return operand1.PlusMinus(operand2);
        }

        /// <summary>
        /// Linearly interpolates between two values.
        /// </summary>
        /// <param name="value1">First value.</param>
        /// <param name="value2">Second value.</param>
        /// <param name="amount">Value between 0 and 1 indicating the weight of the second value.</param>
        /// <returns>Interpolated value.</returns>
        public static double InterpolationLinear(double value1,
                                                 double value2,
                                                 double amount)
        {
            return value1 + (value2 - value1) * amount;
        }

        /// <summary>
        /// Linearly interpolates between two values.
        /// </summary>
        /// <param name="point1">First point.</param>
        /// <param name="point2">Second point.</param>
        /// <param name="amount">Value between 0 and 1 indicating the weight of the second point.</param>
        /// <returns>Interpolated value.</returns>
        public static Point InterpolationLinear(Point point1,
                                                Point point2,
                                                double amount)
        {
            return point1 + (point2 - point1) * amount;
        }

        /// <summary>
        /// Interpolations the polynomial.
        /// </summary>
        /// <param name="points">The points.</param>
        /// <param name="amount">The amount.</param>
        /// <returns>System.Double.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public static double InterpolationPolynomial(Point[] points,
                                                     double amount)
        {
            // Use Lagrange polynomials:
            // https://en.wikipedia.org/wiki/Polynomial_interpolation
            // Consider making object in order to follow curve
            throw new NotImplementedException();
        }



        /// <summary>
        /// X-coordinate of a horizontal line intersecting the line described by the points provided.
        /// </summary>
        /// <param name="y">Y-coordinate of the horizontal line.</param>
        /// <param name="x1">X-coordinate of first point.</param>
        /// <param name="y1">Y-coordinate of first point.</param>
        /// <param name="x2">X-coordinate of second point.</param>
        /// <param name="y2">Y-coordinate of second point.</param>
        /// <returns></returns>
        public static double IntersectionX(double y, double x1, double y1, double x2, double y2)
        {
            return ((((y - y1) * (x2 - x1)) / (y2 - y1)) + x1);
        }

        /// <summary>
        /// X-coordinate of a horizontal line intersecting the line described by the points provided.
        /// </summary>
        /// <param name="y">Y-coordinate of the horizontal line.</param>
        /// <param name="I">First point.</param>
        /// <param name="J">Second point.</param>
        /// <returns></returns>
        public static double IntersectionX(double y, Point I, Point J)
        {
            return IntersectionX(y, I.X, I.Y, J.X, J.Y);
        }

        

    }
}
