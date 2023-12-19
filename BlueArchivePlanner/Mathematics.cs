using BlueArchivePlanner.Native;

namespace BlueArchivePlanner;

public static class Optimization
{
    /// <summary>
    /// Determines the solution of a linear programming problem: Maximize C∙x, subject to M∙x >= b and x >= 0
    /// </summary>
    /// <param name="objective">C, a array describing the linear functional to maximize.</param>
    /// <param name="constraints">M, a 2D array describing the different constraints.</param>
    /// <param name="rightHandSides">B, a array describing the right sides of the constraints inequalities.</param>
    /// <returns>The tuple of optimal value of the objective function and the values of the decision variables</returns>
    public static (double, double[]) SolveLinearProgram(double[] objective, double[,] constraints, double[] rightHandSides)
    {
        try
        {
            using SoPlex soPlex = new SoPlex();
            soPlex.SetVerbosity(0);
            soPlex.SetObjSense(SoPlex.ObjSense.Maximize);
            for (int i = 0; i < objective.Length; i++)
            {
                double[] col = new double[constraints.GetLength(0)];
                for (int j = 0; j < constraints.GetLength(0); j++)
                {
                    col[j] = constraints[j, i];
                }
                soPlex.AddColReal(col, objective[i], 0, SoPlex.Infinity);
            }
            soPlex.ChangeLhsReal(rightHandSides);
            soPlex.Optimize();
            double[] primal = soPlex.GetPrimalReal();
            double objVal = soPlex.ObjValueReal();
            return (objVal, primal);
        }
        catch
        {
            return (double.NaN, []);
        }
    }
}
