using System;
using Inventor;

namespace InventorShims
{
    /// <summary>
    /// A set of extension methods for the Document Parameters.
    /// </summary>
    public static class ParameterShim
    {

        /// <summary>
        /// Sets the value of a numeric parameter, or creates one if one does not exist.
        /// </summary>
        /// <param name="document"></param>
        /// <param name="parameterName"></param>
        /// <param name="parameterValue"></param>
        /// <param name="units"></param>
        /// <param name="clobberFlag"></param>
        public static void SetParameter(this Document document, string parameterName, string parameterValue, string units, bool clobberFlag = true)
        {

            Parameters parameters = GetParameters(document);
            _ = parameters ?? throw new ArgumentException("This document " + document.FullDocumentName + " does not support parameters.");


            if (!ParameterExists(parameters, parameterName))
            {
                try
                {
                    parameters.UserParameters.AddByExpression(parameterName, parameterValue, units);
                }
                catch { }
                return;
            }

            Parameter parameter = parameters[parameterName];
            if (clobberFlag && IsParameterWritable(parameter))
            {
                try
                {
                    parameter.Expression = parameterValue + " " + units;
                }
                catch { }
            }

        }

        /// <summary>
        /// Sets the value of a text parameter, or creates one if one does not exist.
        /// </summary>
        /// <param name="document"></param>
        /// <param name="parameterName"></param>
        /// <param name="parameterValue"></param>
        /// <param name="clobberFlag"></param>
        public static void SetParameter(this Document document, string parameterName, string parameterValue, bool clobberFlag = true)
        {

            Parameters parameters = GetParameters(document);
            _ = parameters ?? throw new ArgumentException("This document " + document.FullDocumentName + " does not support parameters.");


            if (!ParameterExists(parameters, parameterName))
            {
                try
                {
                    parameters.UserParameters.AddByValue(parameterName, parameterValue, "TEXT");
                }
                catch { }
                return;
            }

            Parameter parameter = parameters[parameterName];
            var unit = parameter.get_Units();


            if (clobberFlag && IsParameterWritable(parameter) && unit == "TEXT")
            {
                try
                {
                    parameter.Value = parameterValue;
                }
                catch { }
            }

        }


        /// <summary>
        /// Sets the value of a boolean parameter, or creates one if one does not exist.
        /// </summary>
        /// <param name="document"></param>
        /// <param name="parameterName"></param>
        /// <param name="parameterBool"></param>
        /// <param name="clobberFlag"></param>
        public static void SetParameter(this Document document, string parameterName, bool parameterValue, bool clobberFlag = true)
        {

            Parameters parameters = GetParameters(document);
            _ = parameters ?? throw new ArgumentException("This document " + document.FullDocumentName + " does not support parameters.");

//            string parameterValue = parameterBool ? "true" : "false";

            if (!ParameterExists(parameters, parameterName))
            {
                try
                {
                    parameters.UserParameters.AddByValue(parameterName, parameterValue, "BOOLEAN");
                }
                catch { }
                return;
            }

            Parameter parameter = parameters[parameterName];
            var unit = parameter.get_Units();


            if (clobberFlag && IsParameterWritable(parameter) && unit == "BOOLEAN")
            {
                try
                {
                    parameter.Value = parameterValue;
                }
                catch { }
            }

        }


        /// <summary>
        /// Gets the value of a numeric parameter.
        /// </summary>
        /// <param name="document"></param>
        /// <param name="parameterName"></param>
        /// <returns></returns>
        public static string GetParameter(this Inventor.Document document, string parameterName)
        {
            Parameters parameters = GetParameters(document);
            _ = parameters ?? throw new ArgumentException("This document " + document.FullDocumentName + " does not support parameters.");

            UnitsOfMeasure uom = document.UnitsOfMeasure;
            
            if (!ParameterExists(parameters, parameterName))
            {
                return String.Empty;
            }

            Parameter parameter = parameters[parameterName];
            var value = parameter.Value;
            var unit = parameter.get_Units();

            UnitsTypeEnum unitEnum;
            unitEnum = uom.GetTypeFromString(unit);

            switch (unitEnum)
            {
                case UnitsTypeEnum.kBooleanUnits:
                    return (string)parameter.Value;

                case UnitsTypeEnum.kTextUnits:
                    return (string)parameter.Value;

                default:
                    return uom.GetStringFromValue((double)value, unitEnum);

            }



        }

        public static void RemoveParameter(this Inventor.Document document, string parameterName)
        {
            Parameters parameters = GetParameters(document);
            _ = parameters ?? throw new ArgumentException("This document " + document.FullDocumentName + " does not support parameters.");

            if (!ParameterExists(parameters, parameterName))
            {
                return;
            }

            Parameter parameter = parameters[parameterName];
            if (!IsParameterWritable(parameter))
            {
                return;
            }

            if (parameter.InUse)
            {
                return;
            }

            parameter.Delete();
        }

        private static bool ParameterExists(Parameters parameters, string parameterName)
        {
            foreach (Parameter parameter in parameters)
            {
                if (parameter.Name == parameterName) { return true; }
            }

            return false;
        }

        public static Parameters GetParameters(this Document document)
        {
            switch (document)
            {
                case PartDocument _:
                    PartDocument part = (PartDocument)document;
                    return part.ComponentDefinition.Parameters;

                case AssemblyDocument _:
                    AssemblyDocument assembly = (AssemblyDocument)document;
                    return assembly.ComponentDefinition.Parameters;

                default:
                    return null;
            }
        }
        private static bool IsParameterWritable(Parameter parameter)
        {

            if (parameter.ParameterType == ParameterTypeEnum.kModelParameter || parameter.ParameterType == ParameterTypeEnum.kUserParameter)
            {
                return true;
            }

            return false;
        }


    }
}
