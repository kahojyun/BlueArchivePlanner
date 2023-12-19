using System.Diagnostics;
using System.Runtime.InteropServices;

namespace BlueArchivePlanner.Native;

/// <summary>Defines the type of a member as it was used in the native signature.</summary>
[AttributeUsage(AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue, AllowMultiple = false, Inherited = true)]
[Conditional("DEBUG")]
internal sealed partial class NativeTypeNameAttribute : Attribute
{
    private readonly string _name;

    /// <summary>Initializes a new instance of the <see cref="NativeTypeNameAttribute" /> class.</summary>
    /// <param name="name">The name of the type that was used in the native signature.</param>
    public NativeTypeNameAttribute(string name)
    {
        _name = name;
    }

    /// <summary>Gets the name of the type that was used in the native signature.</summary>
    public string Name => _name;
}

public sealed unsafe partial class SoPlex
{
    private unsafe partial class Native
    {
        [DllImport("libsoplexshared", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void* SoPlex_create();

        [DllImport("libsoplexshared", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void SoPlex_free(void* soplex);

        [DllImport("libsoplexshared", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void SoPlex_clearLPReal(void* soplex);

        [DllImport("libsoplexshared", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int SoPlex_numRows(void* soplex);

        [DllImport("libsoplexshared", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int SoPlex_numCols(void* soplex);

        [DllImport("libsoplexshared", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void SoPlex_setRational(void* soplex);

        [DllImport("libsoplexshared", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void SoPlex_setIntParam(void* soplex, int paramcode, int paramvalue);

        [DllImport("libsoplexshared", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int SoPlex_getIntParam(void* soplex, int paramcode);

        [DllImport("libsoplexshared", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void SoPlex_addColReal(void* soplex, double* colentries, int colsize, int nnonzeros, double objval, double lb, double ub);

        [DllImport("libsoplexshared", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void SoPlex_addColRational(void* soplex, [NativeTypeName("long *")] int* colnums, [NativeTypeName("long *")] int* coldenoms, int colsize, int nnonzeros, [NativeTypeName("long")] int objvalnum, [NativeTypeName("long")] int objvaldenom, [NativeTypeName("long")] int lbnum, [NativeTypeName("long")] int lbdenom, [NativeTypeName("long")] int ubnum, [NativeTypeName("long")] int ubdenom);

        [DllImport("libsoplexshared", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void SoPlex_addRowReal(void* soplex, double* rowentries, int rowsize, int nnonzeros, double lb, double ub);

        [DllImport("libsoplexshared", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void SoPlex_addRowRational(void* soplex, [NativeTypeName("long *")] int* rownums, [NativeTypeName("long *")] int* rowdenoms, int rowsize, int nnonzeros, [NativeTypeName("long")] int lbnum, [NativeTypeName("long")] int lbdenom, [NativeTypeName("long")] int ubnum, [NativeTypeName("long")] int ubdenom);

        [DllImport("libsoplexshared", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void SoPlex_getPrimalReal(void* soplex, double* primal, int dim);

        [DllImport("libsoplexshared", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("char *")]
        public static extern sbyte* SoPlex_getPrimalRationalString(void* soplex, int dim);

        [DllImport("libsoplexshared", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void SoPlex_getDualReal(void* soplex, double* dual, int dim);

        [DllImport("libsoplexshared", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int SoPlex_optimize(void* soplex);

        [DllImport("libsoplexshared", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void SoPlex_changeObjReal(void* soplex, double* obj, int dim);

        [DllImport("libsoplexshared", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void SoPlex_changeObjRational(void* soplex, [NativeTypeName("long *")] int* objnums, [NativeTypeName("long *")] int* objdenoms, int dim);

        [DllImport("libsoplexshared", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void SoPlex_changeLhsReal(void* soplex, double* lhs, int dim);

        [DllImport("libsoplexshared", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void SoPlex_changeLhsRational(void* soplex, [NativeTypeName("long *")] int* lhsnums, [NativeTypeName("long *")] int* lhsdenoms, int dim);

        [DllImport("libsoplexshared", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void SoPlex_changeRhsReal(void* soplex, double* rhs, int dim);

        [DllImport("libsoplexshared", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void SoPlex_changeRhsRational(void* soplex, [NativeTypeName("long *")] int* rhsnums, [NativeTypeName("long *")] int* rhsdenoms, int dim);

        [DllImport("libsoplexshared", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void SoPlex_writeFileReal(void* soplex, [NativeTypeName("char *")] sbyte* filename);

        [DllImport("libsoplexshared", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern double SoPlex_objValueReal(void* soplex);

        [DllImport("libsoplexshared", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("char *")]
        public static extern sbyte* SoPlex_objValueRationalString(void* soplex);

        [DllImport("libsoplexshared", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void SoPlex_changeBoundsReal(void* soplex, double* lb, double* ub, int dim);

        [DllImport("libsoplexshared", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void SoPlex_changeVarBoundsReal(void* soplex, int colidx, double lb, double ub);

        [DllImport("libsoplexshared", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void SoPlex_changeVarBoundsRational(void* soplex, int colidx, [NativeTypeName("long")] int lbnum, [NativeTypeName("long")] int lbdenom, [NativeTypeName("long")] int ubnum, [NativeTypeName("long")] int ubdenom);

        [DllImport("libsoplexshared", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void SoPlex_changeVarUpperReal(void* soplex, int colidx, double ub);

        [DllImport("libsoplexshared", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void SoPlex_getUpperReal(void* soplex, double* ub, int dim);

        [DllImport("libsoplexshared", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int SoPlex_basisRowStatus(void* soplex, int rowidx);

        [DllImport("libsoplexshared", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int SoPlex_basisColStatus(void* soplex, int colidx);

        [DllImport("libsoplexshared", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void SoPlex_getRowVectorReal(void* soplex, int i, int* nnonzeros, [NativeTypeName("long *")] int* indices, double* coefs);

        [DllImport("libsoplexshared", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void SoPlex_getRowVectorRational(void* soplex, int i, int* nnonzeros, [NativeTypeName("long *")] int* indices, [NativeTypeName("long *")] int* coefsnum, [NativeTypeName("long *")] int* coefsdenom);

        [DllImport("libsoplexshared", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void SoPlex_getRowBoundsReal(void* soplex, int i, double* lb, double* ub);

        [DllImport("libsoplexshared", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void SoPlex_getRowBoundsRational(void* soplex, int i, [NativeTypeName("long *")] int* lbnum, [NativeTypeName("long *")] int* lbdenom, [NativeTypeName("long *")] int* ubnum, [NativeTypeName("long *")] int* ubdenom);
    }
}
