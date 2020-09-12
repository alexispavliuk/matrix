using System;

namespace MatrixLibrary
{
    public class Matrix : ICloneable 
    {
        /// <summary>
        /// Number of rows.
        /// </summary>
        public int Rows { get; }

        /// <summary>
        /// Number of columns.
        /// </summary>
        public int Columns { get; }

        /// <summary>
        /// Gets an array of floating-point values that represents the elements of this Matrix.
        /// </summary>
        public double[,] Array { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Matrix"/> class.
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="columns"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public Matrix(int rows, int columns)
        {
            if (rows < 0)
            {
                throw new ArgumentOutOfRangeException("rows", "is out of range");
            }
            else if (columns < 0)
            {
                throw new ArgumentOutOfRangeException("columns", "is out of range");
            }
            this.Rows = rows;
            this.Columns = columns;
            Array = new double[rows, columns];
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Matrix"/> class with the specified elements.
        /// </summary>
        /// <param name="array">An array of floating-point values that represents the elements of this Matrix.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public Matrix(double[,] array)
        {
            if (array is null)
            {
                throw new ArgumentNullException("array", "Array is null");
            }
            this.Array = array;
            Rows = array.GetLength(0);
            Columns = array.GetLength(1);
        }

        /// <summary>
        /// Allows instances of a Matrix to be indexed just like arrays.
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <exception cref="ArgumentException"></exception>
        public double this[int row, int column]
        {
            get
            {
                if (row >= Rows || column >= Columns || row < 0 || column < 0)
                {
                    throw new ArgumentException("Wrong index.");
                }
                return Array[row, column];
            }
            set
            {
                if (row >= Rows || column >= Columns || row < 0 || column < 0)
                {
                    throw new ArgumentException("Wrong index.");
                }
                Array[row, column] = value;
            }
        }

        /// <summary>
        /// Creates a deep copy of this Matrix.
        /// </summary>
        /// <returns>A deep copy of the current object.</returns>
        public object Clone()
        {
            return this.MemberwiseClone();
        }

        /// <summary>
        /// Adds two matrices.
        /// </summary>
        /// <param name="matrix1"></param>
        /// <param name="matrix2"></param>
        /// <returns>New <see cref="Matrix"/> object which is sum of two matrices.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="MatrixException"></exception>
        public static Matrix operator +(Matrix matrix1, Matrix matrix2)
        {
            if (matrix1 is null)
            {
                throw new ArgumentNullException("matrix1", "is null");
            }
            if (matrix2 is null)
            {
                throw new ArgumentNullException("matrix2", "is null");
            }
            if (matrix1.Rows != matrix2.Rows || matrix1.Columns != matrix2.Columns)
            {
                throw new MatrixException("MatrixException");
            }
            Matrix matrix3 = new Matrix(matrix1.Rows, matrix1.Columns);
            for (int i = 0; i < matrix1.Rows; i++)
            {
                for (int j = 0; j < matrix1.Columns; j++)
                {
                    matrix3[i, j] = matrix1[i, j] + matrix2[i, j];
                }
            }
            return matrix3;
        }

        /// <summary>
        /// Subtracts two matrices.
        /// </summary>
        /// <param name="matrix1"></param>
        /// <param name="matrix2"></param>
        /// <returns>New <see cref="Matrix"/> object which is subtraction of two matrices</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="MatrixException"></exception>
        public static Matrix operator -(Matrix matrix1, Matrix matrix2)
        {
            if (matrix1 is null)
            {
                throw new ArgumentNullException("matrix1", "is null");
            }
            if (matrix2 is null)
            {
                throw new ArgumentNullException("matrix2", "is null");
            }
            if (matrix1.Rows != matrix2.Rows || matrix1.Columns != matrix2.Columns)
            {
                throw new MatrixException("MatrixException");
            }
            Matrix matrix3 = new Matrix(matrix1.Rows, matrix1.Columns);
            for (int i = 0; i < matrix1.Rows; i++)
            {
                for (int j = 0; j < matrix1.Columns; j++)
                {
                    matrix3[i, j] = matrix1[i, j] - matrix2[i, j];
                }
            }
            return matrix3;
        }

        /// <summary>
        /// Multiplies two matrices.
        /// </summary>
        /// <param name="matrix1"></param>
        /// <param name="matrix2"></param>
        /// <returns>New <see cref="Matrix"/> object which is multiplication of two matrices.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="MatrixException"></exception>
        public static Matrix operator *(Matrix matrix1, Matrix matrix2)
        {
            if (matrix1 is null)
            {
                throw new ArgumentNullException("matrix1", "is null");
            }
            if (matrix2 is null)
            {
                throw new ArgumentNullException("matrix2", "is null");
            }
            if (matrix1.Columns != matrix2.Rows)
            {
                throw new MatrixException();
            }
            Matrix result = new Matrix(matrix1.Rows, matrix2.Columns);
            for (int i = 0; i < result.Rows; i++)
            {
                for (int j = 0; j < result.Columns; j++)
                {
                    result[i, j] = 0;
                    for (int k = 0; k < matrix1.Columns; k++)
                    {
                        result[i, j] += matrix1[i, k] * matrix2[k, j];
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Adds <see cref="Matrix"/> to the current matrix.
        /// </summary>
        /// <param name="matrix"><see cref="Matrix"/> for adding.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="MatrixException"></exception>
        public Matrix Add(Matrix matrix)
        {
            if (matrix is null)
            {
                throw new ArgumentNullException("matrix", "is null");
            }
            if (matrix.Rows != this.Rows || matrix.Columns != this.Columns)
            {
                throw new MatrixException("MatrixException");
            }
            Matrix result = new Matrix(this.Rows, this.Columns);
            for (int i = 0; i < this.Rows; i++)
            {
                for (int j = 0; j < this.Columns; j++)
                {
                    result[i, j] = this.Array[i, j] + matrix[i, j];
                }
            }
            return result;
        }

        /// <summary>
        /// Subtracts <see cref="Matrix"/> from the current matrix.
        /// </summary>
        /// <param name="matrix"><see cref="Matrix"/> for subtracting.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="MatrixException"></exception>
        public Matrix Subtract(Matrix matrix)
        {
            if (matrix is null)
            {
                throw new ArgumentNullException("matrix", "is null");
            }
            if (matrix.Rows != this.Rows || matrix.Columns != this.Columns)
            {
                throw new MatrixException("MatrixException");
            }
            Matrix result = new Matrix(this.Rows, this.Columns);
            for (int i = 0; i < this.Rows; i++)
            {
                for (int j = 0; j < this.Columns; j++)
                {
                    result[i, j] = this.Array[i, j] - matrix[i, j];
                }
            }
            return result;
        }

        /// <summary>
        /// Multiplies <see cref="Matrix"/> on the current matrix.
        /// </summary>
        /// <param name="matrix"><see cref="Matrix"/> for multiplying.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="MatrixException"></exception>
        public Matrix Multiply(Matrix matrix)
        {
            if (matrix is null)
            {
                throw new ArgumentNullException("matrix", "is null");
            }
            if (this.Columns != matrix.Rows)
            {
                throw new MatrixException();
            }
            Matrix result = new Matrix(this.Rows, matrix.Columns);
            for (int i = 0; i < result.Rows; i++)
            {
                for (int j = 0; j < result.Columns; j++)
                {
                    result[i, j] = 0;
                    for (int k = 0; k < this.Columns; k++)
                    {
                        result[i, j] += this[i, k] * matrix[k, j];
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Tests if <see cref="Matrix"/> is identical to this Matrix.
        /// </summary>
        /// <param name="obj">Object to compare with. (Can be null)</param>
        /// <returns>True if matrices are equal, false if are not equal.</returns>
        /// <exception cref="InvalidCastException">Thrown when object has wrong type.</exception>
        /// <exception cref="MatrixException">Thrown when matrices are incomparable.</exception>
        public override bool Equals(object obj)
        {
            try
            {
                Matrix matrix = (Matrix)obj;
                for (int i = 0; i < Rows; i++)
                {
                    for (int j = 0; j < Columns; j++)
                    {
                        if (matrix[i, j] != Array[i, j])
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            var hashCode = 352033288;
            hashCode = hashCode * -1521134295 + Rows.GetHashCode();
            hashCode = hashCode * -1521134295 + Columns.GetHashCode();
            return hashCode;
        }
    }
}
