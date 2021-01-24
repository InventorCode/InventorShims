using System;
using System.Collections.Generic;
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
        /// <param name="document">Inventor.Document</param>
        /// <param name="parameterName"></param>
        /// <param name="parameterValue"></param>
        /// <param name="units">Units as a string, e.g. in, mm, cm, etc</param>
        /// <param name="clobberFlag">If set to false, will not overwrite an existing parameter.</param>
        public static void SetParameterValue(this Document document, string parameterName, string parameterValue, string units, bool clobberFlag = true)
        {
            Parameter parameter = document.GetParameter(parameterName);

            if (!ParameterExists(parameter))
            {
                try
                {
                    Parameters parameters = document.GetParameters();
                    parameters.UserParameters.AddByExpression(parameterName, parameterValue, units);
                }
                catch { }
                return;
            }

            if (clobberFlag && ParameterIsWritable(parameter))
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
        /// <param name="document">Inventor.Document</param>
        /// <param name="parameterName"></param>
        /// <param name="parameterValue"></param>
        /// <param name="clobberFlag">If set to false, will not overwrite an existing parameter.</param>
        public static void SetParameterValue(this Document document, string parameterName, string parameterValue, bool clobberFlag = true)
        {
            Parameter parameter = document.GetParameter(parameterName);

            if (!ParameterExists(parameter))
            {
                try
                {
                    Parameters parameters = document.GetParameters();
                    parameters.UserParameters.AddByValue(parameterName, parameterValue, UnitsTypeEnum.kTextUnits);
                }
                catch { }
                return;
            }

            var unit = parameter.get_Units();

            if (clobberFlag && ParameterIsWritable(parameter) && unit.Equals("Text", StringComparison.OrdinalIgnoreCase))
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
        /// <param name="document">Inventor.Document</param>
        /// <param name="parameterName"></param>
        /// <param name="parameterValue"></param>
        /// <param name="clobberFlag">If set to false, will not overwrite an existing parameter.</param>
        public static void SetParameterValue(this Document document, string parameterName, bool parameterValue, bool clobberFlag = true)
        {
            Parameter parameter = document.GetParameter(parameterName);

            if (!ParameterExists(parameter))
            {
                try
                {
                    Parameters parameters = document.GetParameters();
                    parameters.UserParameters.AddByValue(parameterName, parameterValue, UnitsTypeEnum.kBooleanUnits);
                }
                catch { }
                return;
            }

            var unit = parameter.get_Units();


            if (clobberFlag && ParameterIsWritable(parameter) && unit.Equals("Boolean", StringComparison.OrdinalIgnoreCase))
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
        /// <param name="document">Inventor.Document</param>
        /// <param name="parameterName">Name of the parameter as a string.</param>
        /// <returns></returns>
        public static string GetParameterValue(this Inventor.Document document, string parameterName)
        {
            Parameter parameter = document.GetParameter(parameterName);

            UnitsOfMeasure uom = document.UnitsOfMeasure;
            
            if (!ParameterExists(parameter))
            {
                return String.Empty;
            }

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

        /// <summary>
        /// Removes a parameter from a Document object if it exists.
        /// </summary>
        /// <param name="document">Inventor.Document</param>
        /// <param name="parameterName"></param>
        public static void RemoveParameter(this Inventor.Document document, string parameterName)
        {
            //Parameters parameters = GetParameters(document);
            //_ = parameters ?? throw new ArgumentException("This document " + document.FullDocumentName + " does not support parameters.");
            Parameter parameter = document.GetParameter(parameterName);

            if (!ParameterExists(parameter))
            {
                return;
            }

            if (!ParameterIsWritable(parameter))
            {
                return;
            }

            if (parameter.InUse)
            {
                return;
            }

            parameter.Delete();
        }

        /// <summary>
        /// Returns a parameter object from a document object specified by name.
        /// </summary>
        /// <param name="document">Inventor.Document</param>
        /// <param name="parameterName"></param>
        /// <returns>Parameter</returns>
        public static Parameter GetParameter(this Inventor.Document document, string parameterName)
        {
            Parameters parameters = GetParameters(document);
            _ = parameters ?? throw new ArgumentException("This document " + document.FullDocumentName + " does not support parameters.");

            foreach (Parameter parameter in parameters)
            {
                if (parameter.Name == parameterName) { return parameter; }
            }

            return null;
        }

        /// <summary>
        /// Returns a boolean indicating if a Parameter exists within a Document object.
        /// </summary>
        /// <param name="parameter">Inventor.Parameter</param>
        /// <returns>Boolean</returns>
        private static bool ParameterExists(Parameter parameter)
        {
            return (parameter is null) ? false : true;
        }

        /// <summary>
        /// Returns the Parameters object in a specified Document object.
        /// </summary>
        /// <param name="document">Inventor.Document</param>
        /// <returns>Parameters</returns>
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

        /// <summary>
        /// Tests if the provided parameter is writable.  Only kModelParameters and kUserParameters return true.
        /// </summary>
        /// <param name="parameter">Parameter</param>
        /// <returns>Boolean</returns>
        public static bool ParameterIsWritable(this Parameter parameter)
        {
            if (parameter.ParameterType == ParameterTypeEnum.kModelParameter || parameter.ParameterType == ParameterTypeEnum.kUserParameter)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Tests if the parameter matching the provided parameterName is writable.  Only kModelParameters and kUserParameters return true.
        /// <code></code>VB sample:<code>Dim answer As Boolean = ParameterIsWritable(oDoc, "ThisParameterName")</code>
        /// </summary>
        /// <param name="document">Document</param>
        /// <param name="parameterName">String</param>
        /// <returns>Boolean</returns>
        public static bool ParameterIsWritable(Document document, string parameterName)
        {
            Parameter parameter = null;
            try
            {
                parameter = GetParameter(document, parameterName);

                if (parameter.ParameterType == ParameterTypeEnum.kModelParameter || parameter.ParameterType == ParameterTypeEnum.kUserParameter)
                {
                    return true;
                }
            }
            catch
            { }

            return false;
        }

        /// <summary>
        /// Return a list of parameter names within the specified document.
        /// </summary>
        /// <param name="document">Inventor.Document</param>
        /// <returns>List (of string)</returns>
        public static List<string> GetParameterNames(this Document document)
        {
            Parameters parameters;

            if (document.DocumentType == DocumentTypeEnum.kAssemblyDocumentObject)
            {
                AssemblyDocument identifiedAssemblyDocument = (AssemblyDocument)document;

                parameters = identifiedAssemblyDocument.ComponentDefinition.Parameters;
            }
            else if (document.DocumentType == DocumentTypeEnum.kPartDocumentObject)
            {
                PartDocument identifiedPartDocument = (PartDocument)document;

                parameters = identifiedPartDocument.ComponentDefinition.Parameters;
            }
            else
            {
                throw new Exception("Unknown type of document passed to GetParameterNames");
            }

            var returnList = new List<string>();

            foreach (Parameter parameter in parameters)
            {
                returnList.Add(parameter.Name);
            }

            return returnList;
        }
    }
}
