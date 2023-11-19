namespace BlazorApp2;

// Implemented the `Linear Programming Simplex Method VI` of LabVIEW in C#
// https://gist.github.com/diluculo/0cabfed820fe6b0c7ce70f44fc95c597

public static class Optimization
{
    /// <summary>
    /// Determines the solution of a linear programming problem: Maximize C∙x, subject to M∙x >= b and x >= 0
    /// </summary>
    /// <param name="objective">C, a array describing the linear functional to maximize.</param>
    /// <param name="constraints">M, a 2D array describing the different constraints.</param>
    /// <param name="rightHandSides">B, a array describing the right sides of the constraints inequalities.</param>
    /// <returns>The tuple of optimal value of the objective function and the values of the decision variables</returns>
    public static Tuple<double, double[]> SolveLinearProgram(double[] objective, double[,] constraints, double[] rightHandSides)
    {
        try
        {
            return new Simplex(objective, constraints, rightHandSides).Maximize();
        }
        catch
        {
            return new Tuple<double, double[]>(double.NaN, new double[0]);
        }
    }
}

class Simplex
{
    public double[] C { get; set; } //objective
    public double[,] M { get; set; } // constraints
    public double[] B { get; set; } //rightHandSides

    public int ErrorCode { get; private set; }

    //private double[] Bm; // modified B
    private double[,] Tableaux;

    private int[] n_minus_m, n_minus_m_prime; // n- m
    private int[] m, m_prime; // m

    public Simplex(double[] C, double[,] M, double[] B)
    {
        this.C = C;
        this.M = M;
        this.B = B;

        int dim1 = M.GetLength(0); // number of constraints
        int dim2 = M.GetLength(1); // number of variables            

        if (dim1 != B.Length)
        {
            throw new Exception("Number of constraints in M doesn't match number in B.");
        }

        if (dim2 != C.Length)
        {
            throw new Exception("Number of variables in C doesn't match number in M.");
        }

        //
        // LP Preparation
        //

        double[] Bm = new double[dim1]; // modifed B
        Array.Copy(this.B, Bm, dim1);
        for (int i = 0; i < Bm.Length; i++)
        {
            if (Bm[i] == 0) Bm[i] = -1.0E-12;
            Bm[i] *= -1E-8 * Math.Sign(Bm[i]) + 1;
        }

        this.Tableaux = new double[dim1 + dim2, dim1];
        for (int i = 0; i < dim1; i++)
        {
            for (int j = 0; j < dim2; j++)
            {
                this.Tableaux[j, i] = M[i, j];
            }
        }

        for (int i = 0; i < Bm.Length; i++)
        {
            if (Bm[i] >= 0)
            {
                this.Tableaux[i + dim2, i] = +1.0;

                for (int j = 0; j < dim2; j++)
                {
                    this.Tableaux[j, i] = -1.0 * this.Tableaux[j, i];
                }
            }
            else
            {
                Bm[i] *= -1.0;
                this.Tableaux[i + dim2, i] = -1.0;
            }
        }

        double[] firstRow = this.Append(-1 * Bm.Sum(), Bm);
        double[] sumOfEachRow = this.SumOfRows(this.Tableaux);
        this.Tableaux = this.Transpose(this.Append(firstRow, this.Transpose(this.Append(this.Negate(sumOfEachRow), this.Transpose(this.Tableaux)))));

        this.n_minus_m = new int[dim1 + dim2];
        for (int i = 0; i < dim1 + dim2; i++)
            this.n_minus_m[i] = i + 1;

        this.m = new int[dim1];
        for (int i = 0; i < dim1; i++)
            this.m[i] = i + 1 + dim1 + dim2;
    }

    public Tuple<double, double[]> Maximize()
    {
        this.ErrorCode = 0;

        double eps;
        Tuple<double, double> maxmin;
        bool err1 = false, err2 = false, err3 = false;

        this.n_minus_m_prime = new int[this.n_minus_m.Length];
        this.m_prime = new int[this.m.Length];

        //
        // Normalized case
        //

        Array.Copy(this.n_minus_m, this.n_minus_m_prime, this.n_minus_m.Length);
        Array.Copy(this.m, this.m_prime, this.m.Length);

        while (true)
        {
            maxmin = this.MaxMin(this.Tableaux);
            eps = 1.0E-8 * Math.Max(Math.Abs(maxmin.Item1), Math.Abs(maxmin.Item2));

            if (this.MaxMinOfIndexedRow(this.Tableaux, 0).Item1 > eps)
            {
                int errCode = this.StepNormalize(eps);
                if (errCode != 0)
                {
                    this.ErrorCode = Math.Min(this.ErrorCode, errCode);
                    err1 = true;
                    break;
                }
            }
            else
                break;
        }

        maxmin = this.MaxMin(this.Tableaux);
        eps = 1E-8 * Math.Max(Math.Abs(maxmin.Item1), Math.Abs(maxmin.Item2));

        err2 = (this.MaxMinOfIndexedRow(this.Tableaux, 0).Item1 > eps);

        for (int i = 0; i < this.m_prime.Length; i++)
        {
            double val = this.m_prime[i];
            for (int j = 0; j < this.m.Length; j++)
                if (val == this.m[j])
                {
                    err3 = true;
                    break;
                }
        }

        if (err1 || err2 || err3)
        {
            this.m_prime = null;
            this.n_minus_m_prime = null;
            this.Tableaux = null;
            this.ErrorCode = err1 ? -23032 : -23031;
        }
        else
            this.ErrorCode = 0;

        int[] n_m = this.Append(0, this.n_minus_m_prime);
        double[,] table = new double[0, 0];
        this.n_minus_m_prime = new int[0];

        int loop = n_m.Length;
        for (int i = 0; i < loop; i++)
        {
            if (n_m[i] <= this.M.GetLength(0) + this.M.GetLength(1))
            {
                table = this.Append(table, this.IndexedColumn(this.Tableaux, i));
                if (i > 0)
                    this.n_minus_m_prime = this.Append(this.n_minus_m_prime, n_m[i]);
            }
        }

        this.Tableaux = this.Transpose(table);

        //
        // Middle
        //            
        double[] vector = new double[this.C.Length + 1];
        for (int i = 0; i < this.C.Length; i++)
        {
            double[] vectorInc = new double[this.C.Length + 1];

            int idx1 = -1;
            for (int j = 0; j < this.m_prime.Length; j++)
            {
                if (this.m_prime[j] == i + 1)
                    idx1 = j;
            }

            int idx2 = -1;
            for (int j = 0; j < this.n_minus_m_prime.Length; j++)
            {
                if (this.n_minus_m_prime[j] == i + 1)
                    idx2 = j;
            }

            if (idx2 != -1)
            {
                vectorInc[idx2 + 1] = 1.0;
            }
            else
            {
                vectorInc = this.IndexedRow(this.Tableaux, idx1 + 1);
            }

            for (int j = 0; j < vector.Length; j++)
            {
                vector[j] += this.C[i] * vectorInc[j];
            }

        }

        for (int i = 0; i < vector.Length; i++)
        {
            this.Tableaux[0, i] = vector[i];
        }

        //
        // Normalized Case Decode
        //

        while (true)
        {
            maxmin = this.MaxMin(this.Tableaux);
            eps = 1E-8 * Math.Max(Math.Abs(maxmin.Item1), Math.Abs(maxmin.Item2));

            if (this.MaxMinOfIndexedRow(this.Tableaux, 0).Item1 > eps)
            {
                int errCode = this.StepNormalize(eps);
                if (errCode != 0)
                {
                    this.ErrorCode = Math.Min(this.ErrorCode, errCode);
                    break;
                }
            }
            else
                break;
        }

        //
        // Finalizing
        //

        if (this.ErrorCode < 0)
        {
            return new Tuple<double, double[]>(double.NaN, new double[0]);
        }
        else
        {
            double xaximum = this.Tableaux[0, 0];
            double[] x = new double[this.C.Length];

            for (int i = 0; i < this.m_prime.Length; i++)
            {
                if (this.m_prime[i] <= this.C.Length)
                {
                    x[this.m_prime[i] - 1] = this.Tableaux[i + 1, 0];
                }
            }

            return new Tuple<double, double[]>(xaximum, x);
        }
    }

    private int StepNormalize(double eps)
    {
        int errCode = 0;

        double maxRatio = double.NegativeInfinity;
        int pivotRowIndex = 0, pivotColIndex = 0;

        for (int j = 0; j < this.Tableaux.GetLength(1); j++)
        {
            if ((this.Tableaux[0, j] >= eps) && (j > 0))
            {
                for (int i = 0; i < this.Tableaux.GetLength(0); i++)
                {
                    if ((this.Tableaux[i, j] <= -1 * eps) && (i > 0))
                    {
                        double newRatio = this.Tableaux[i, 0] / this.Tableaux[i, j];
                        if (maxRatio <= newRatio)
                        {
                            maxRatio = newRatio;
                            pivotRowIndex = i;
                            pivotColIndex = j;
                        }
                    }
                }
            }
        }

        if ((pivotRowIndex > 0) && (pivotColIndex > 0))
        {
            double[] pivotRow = this.IndexedRow(this.Tableaux, pivotRowIndex);
            double[] pivotColumn = this.IndexedColumn(this.Tableaux, pivotColIndex);
            double pivotElement = this.Tableaux[pivotRowIndex, pivotColIndex];

            this.Tableaux = this.BLAS_dger(-1.0 / pivotElement, pivotColumn, pivotRow, this.Tableaux);

            for (int i = 0; i < pivotRow.Length; i++)
                this.Tableaux[pivotRowIndex, i] = -1.0 * pivotRow[i] / pivotElement;

            for (int i = 0; i < pivotColumn.Length; i++)
                this.Tableaux[i, pivotColIndex] = pivotColumn[i] / pivotElement;

            this.Tableaux[pivotRowIndex, pivotColIndex] = 1.0 / pivotElement;

            //Swap index
            int temp1 = this.m_prime[pivotRowIndex - 1];
            int temp2 = this.n_minus_m_prime[pivotColIndex - 1];
            this.m_prime[pivotRowIndex - 1] = temp2;
            this.n_minus_m_prime[pivotColIndex - 1] = temp1;

            errCode = 0;
        }
        else
            errCode = -23032;

        return errCode;
    }

    private double[,] Transpose(double[,] A)
    {
        int N = A.GetLength(0);
        int M = A.GetLength(1);

        double[,] result = new double[M, N];

        for (int i = 0; i < M; i++)
            for (int j = 0; j < N; j++)
                result[i, j] = A[j, i];

        return result;
    }

    private T[,] Append<T>(T[] a, T[,] A)
    {
        int N = A.GetLength(0);
        int M = Math.Max(a.Length, A.GetLength(1));

        T[,] result = new T[N + 1, M];

        for (int j = 0; j < M; j++)
            result[0, j] = (j < a.Length) ? a[j] : default(T);

        for (int i = 0; i < N; i++)
            for (int j = 0; j < M; j++)
                result[i + 1, j] = (j < A.GetLength(1)) ? A[i, j] : default(T);

        return result;
    }

    private T[,] Append<T>(T[,] A, T[] a)
    {
        int N = A.GetLength(0);
        int M = Math.Max(A.GetLength(1), a.Length);

        T[,] result = new T[N + 1, M];

        for (int i = 0; i < N; i++)
            for (int j = 0; j < M; j++)
                result[i, j] = (j < A.GetLength(1)) ? A[i, j] : default(T);

        for (int j = 0; j < M; j++)
            result[N, j] = (j < a.Length) ? a[j] : default(T);

        return result;
    }

    private T[] Append<T>(T a, T[] A)
    {
        T[] result = new T[A.Length + 1];

        result[0] = a;
        for (int i = 0; i < A.Length; i++)
            result[i + 1] = A[i];

        return result;
    }

    private T[] Append<T>(T[] A, T a)
    {
        T[] result = new T[A.Length + 1];

        for (int i = 0; i < A.Length; i++)
            result[i] = A[i];

        result[A.Length] = a;

        return result;
    }

    private double[] SumOfRows(double[,] A)
    {
        double[] result = new double[A.GetLength(0)];

        for (int i = 0; i < A.GetLength(0); i++)
            for (int j = 0; j < A.GetLength(1); j++)
                result[i] += A[i, j];

        return result;
    }

    private double[] Negate(double[] A)
    {
        double[] result = new double[A.Length];

        for (int i = 0; i < A.Length; i++)
            result[i] = -1 * A[i];

        return result;
    }

    private Tuple<double, double> MaxMin(double[,] A)
    {
        double max = A[0, 0];
        double min = A[0, 0];

        for (int i = 0; i < A.GetLength(0); i++)
        {
            for (int j = 0; j < A.GetLength(1); j++)
            {
                if (max < A[i, j]) max = A[i, j];
                if (min > A[i, j]) min = A[i, j];
            }
        }

        return new Tuple<double, double>(max, min);
    }

    private Tuple<double, double> MaxMin(double[] A)
    {
        return new Tuple<double, double>(A.Max(), A.Min());
    }

    private T[] IndexedRow<T>(T[,] A, int rowIndex)
    {
        if (rowIndex > A.GetLength(0))
            throw new ArgumentOutOfRangeException("No such row in array.", "row");

        T[] result = new T[A.GetLength(1)];
        for (int i = 0; i < result.Length; i++)
        {
            result[i] = A[rowIndex, i];
        }
        return result;
    }

    private T[] IndexedColumn<T>(T[,] A, int colIndex)
    {
        if (colIndex > A.GetLength(1))
            throw new ArgumentOutOfRangeException("No such column in array.", "col");

        T[] result = new T[A.GetLength(0)];
        for (int i = 0; i < result.Length; i++)
        {
            result[i] = A[i, colIndex];
        }
        return result;
    }

    private Tuple<double, double> MaxMinOfIndexedRow(double[,] A, int rowIndex = 0)
    {
        double[] row = IndexedRow(A, rowIndex);
        row[0] = row[1];

        return MaxMin(row);
    }

    /// <summary>
    /// Performs the rank 1 operation, alpha*x*y' + A
    /// </summary>
    /// <param name="alpha">scalar</param>
    /// <param name="x">m element vector</param>
    /// <param name="y">n element vector</param>
    /// <param name="A">m by n matrix</param>
    /// <returns></returns>
    private double[,] BLAS_dger(double alpha, double[] x, double[] y, double[,] A)
    {
        //  DGER   performs the rank 1 operation
        //
        //     A := alpha*x*y' + A,
        //
        //  where alpha is a scalar, x is an m element vector, y is an n element
        //  vector and A is an m by n matrix.    
        //

        //[Case I] using DotNetNumerics
        //return (alpha * x.ToVector().OuterProduct(y.ToVector()) + A.ToMatrix()).ToArray();

        //[Case II] direct calculation
        double[,] result = new double[A.GetLength(0), A.GetLength(1)];
        for (int i = 0; i < A.GetLength(0); i++)
        {
            for (int j = 0; j < A.GetLength(1); j++)
            {
                result[i, j] = alpha * x[i] * y[j] + A[i, j];
            }
        }

        return result;
    }
}