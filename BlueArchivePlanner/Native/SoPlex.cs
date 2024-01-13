namespace BlueArchivePlanner.Native;

public sealed unsafe partial class SoPlex : IDisposable
{
    private void* handle;
    private bool disposedValue;

    public int NumRows
    {
        get
        {
            ObjectDisposedException.ThrowIf(disposedValue, this);
            return Native.SoPlex_numRows(handle);
        }
    }

    public int NumCols
    {
        get
        {
            ObjectDisposedException.ThrowIf(disposedValue, this);
            return Native.SoPlex_numCols(handle);
        }
    }

    public const double Infinity = 1e100;

    public SoPlex()
    {
        handle = Native.SoPlex_create();
    }

    public unsafe void AddColReal(ReadOnlySpan<double> entries, double objVal, double lb, double ub)
    {
        ObjectDisposedException.ThrowIf(disposedValue, this);
        int nonZeroCount = entries.Length - entries.Count(0);
        fixed (double* ptr = entries)
        {
            Native.SoPlex_addColReal(handle, ptr, entries.Length, nonZeroCount, objVal, lb, ub);
        }
    }

    public unsafe void AddRowReal(ReadOnlySpan<double> entries, double lb, double ub)
    {
        ObjectDisposedException.ThrowIf(disposedValue, this);
        int nonZeroCount = entries.Length - entries.Count(0);
        fixed (double* ptr = entries)
        {
            Native.SoPlex_addRowReal(handle, ptr, entries.Length, nonZeroCount, lb, ub);
        }
    }

    public void ClearLPReal()
    {
        ObjectDisposedException.ThrowIf(disposedValue, this);
        Native.SoPlex_clearLPReal(handle);
    }

    public unsafe void ChangeObjReal(ReadOnlySpan<double> entries)
    {
        ObjectDisposedException.ThrowIf(disposedValue, this);
        fixed (double* ptr = entries)
        {
            Native.SoPlex_changeObjReal(handle, ptr, entries.Length);
        }
    }

    public unsafe void ChangeLhsReal(ReadOnlySpan<double> entries)
    {
        ObjectDisposedException.ThrowIf(disposedValue, this);
        fixed (double* ptr = entries)
        {
            Native.SoPlex_changeLhsReal(handle, ptr, entries.Length);
        }
    }

    public unsafe void ChangeRhsReal(ReadOnlySpan<double> entries)
    {
        ObjectDisposedException.ThrowIf(disposedValue, this);
        fixed (double* ptr = entries)
        {
            Native.SoPlex_changeRhsReal(handle, ptr, entries.Length);
        }
    }

    public double[] GetPrimalReal()
    {
        ObjectDisposedException.ThrowIf(disposedValue, this);
        int numCols = NumCols;
        double[] result = new double[numCols];
        fixed (double* ptr = result)
        {
            Native.SoPlex_getPrimalReal(handle, ptr, numCols);
        }
        return result;
    }

    public double[] GetDualReal()
    {
        ObjectDisposedException.ThrowIf(disposedValue, this);
        int numRows = NumRows;
        double[] result = new double[numRows];
        fixed (double* ptr = result)
        {
            Native.SoPlex_getDualReal(handle, ptr, numRows);
        }
        return result;
    }

    public double ObjValueReal()
    {
        ObjectDisposedException.ThrowIf(disposedValue, this);
        return Native.SoPlex_objValueReal(handle);
    }

    public void Optimize()
    {
        ObjectDisposedException.ThrowIf(disposedValue, this);
        int ret = Native.SoPlex_optimize(handle);
        if (ret != 1)
        {
            throw new Exception("SoPlex_optimize failed");
        }
    }



    public void SetObjSense(ObjSense mode)
    {
        ObjectDisposedException.ThrowIf(disposedValue, this);
        Native.SoPlex_setIntParam(handle, (int)IntParam.OBJSENSE, (int)mode);
    }

    public enum ObjSense
    {
        Minimize = -1,
        Maximize = 1
    }

    public void SetVerbosity(int verbosity)
    {
        ObjectDisposedException.ThrowIf(disposedValue, this);
        Native.SoPlex_setIntParam(handle, (int)IntParam.VERBOSITY, verbosity);
    }

    private void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                // dispose managed state (managed objects)
            }

            // free unmanaged resources (unmanaged objects) and override finalizer
            Native.SoPlex_free(handle);
            // set large fields to null
            disposedValue = true;
        }
    }

    // override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
    ~SoPlex()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: false);
    }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    enum IntParam
    {
        /// objective sense
        OBJSENSE = 0,

        /// type of computational form, i.e., column or row representation
        REPRESENTATION = 1,

        /// type of algorithm, i.e., primal or dual
        ALGORITHM = 2,

        /// type of LU update
        FACTOR_UPDATE_TYPE = 3,

        /// maximum number of updates without fresh factorization
        FACTOR_UPDATE_MAX = 4,

        /// iteration limit (-1 if unlimited)
        ITERLIMIT = 5,

        /// refinement limit (-1 if unlimited)
        REFLIMIT = 6,

        /// stalling refinement limit (-1 if unlimited)
        STALLREFLIMIT = 7,

        /// display frequency
        DISPLAYFREQ = 8,

        /// verbosity level
        VERBOSITY = 9,

        /// type of simplifier
        SIMPLIFIER = 10,

        /// type of scaler
        SCALER = 11,

        /// type of starter used to create crash basis
        STARTER = 12,

        /// type of pricer
        PRICER = 13,

        /// type of ratio test
        RATIOTESTER = 14,

        /// mode for synchronizing real and rational LP
        SYNCMODE = 15,

        /// mode for reading LP files
        READMODE = 16,

        /// mode for iterative refinement strategy
        SOLVEMODE = 17,

        /// mode for a posteriori feasibility checks
        CHECKMODE = 18,

        /// type of timer
        TIMER = 19,

        /// mode for hyper sparse pricing
        HYPER_PRICING = 20,

        /// minimum number of stalling refinements since last pivot to trigger rational factorization
        RATFAC_MINSTALLS = 21,

        /// maximum number of conjugate gradient iterations in least square scaling
        LEASTSQ_MAXROUNDS = 22,

        /// mode for solution polishing
        SOLUTION_POLISHING = 23,

        /// the number of iterations before the decomposition simplex initialisation is terminated.
        DECOMP_ITERLIMIT = 24,

        /// the maximum number of rows that are added in each iteration of the decomposition based simplex
        DECOMP_MAXADDEDROWS = 25,

        /// the iteration frequency at which the decomposition solve output is displayed.
        DECOMP_DISPLAYFREQ = 26,

        /// the verbosity of the decomposition based simplex
        DECOMP_VERBOSITY = 27,

        /// print condition number during the solve
        PRINTBASISMETRIC = 28,

        /// type of timer for statistics
        STATTIMER = 29,

        /// number of integer parameters
        INTPARAM_COUNT = 30
    }
}
